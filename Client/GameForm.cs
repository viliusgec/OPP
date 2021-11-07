using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.AspNetCore.SignalR.Client;
using Client.Strategy;
using System.Threading;
using Client.Observer;
using Client.Command;
using Client.PictureBoxBuilder;
using Client.Decorator;

namespace Client
{
    public partial class GameForm : Form
    {
        private static HubConnection connection;
        ServerObserver ServerObserver = new();
        MapBuilder MapBuilder = new();
        Movement movement;
        private Map.MapBase map;
        Command.Message message;
        FormsEditor editor;
        Character player;



        public GameForm()
        {
            InitializeComponent();
            MovementLabel.Text = "Controls:\nW/Space - jump\n A D - left, right\n Q - Jump up left \n E - jump up right\n SHIFT - dig down\n J - dig left\n K - dig right";
            FormsEditor tempEdit = new FormsEditor(playerPictureBox, enemyPictureBox, ScoreLabel);
            editor = tempEdit;
            connection = SingletonConnection.GetInstance().GetConnection();
            movement = new Movement(connection);

            message = new Command.Message(textBox2);
            message.ReceiveUndoMessage();
            message.RecieveMessage();
            player = new Player();
       

            playerPictureBox.Hide();
            enemyPictureBox.Hide();
            playerPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            enemyPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;


            this.Size = new Size(800, 800);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            KeyPreview = false;
            KeyDown += SendBoxCoordinates;

            ServerObserver.ReceiveCoordinates(enemyPictureBox, movement);
            MapBuilder = ServerObserver.ReceiveMap(map, playerPictureBox, enemyPictureBox, button1, imageList1, Controls, Size);
            connection.On<string>("ReceiveMap", (jsonString) =>
            {
                playerPictureBox.Show();
                enemyPictureBox.Show();
                map = ServerObserver.GetMap();
                MapBuilder = ServerObserver.GetBuilder();
                Point tempPoint = enemyPictureBox.Location;
                enemyPictureBox.Location = playerPictureBox.Location;
                playerPictureBox.Location = tempPoint;
                button1.Hide();
                button2.Hide();
                button3.Hide();
                textBox1.Hide();
                textBox2.Hide();
                label2.Hide();
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                label2.Enabled = false;
                KeyPreview = true;
            });


            connection.On<string, string, string>("ReceiveMinedBoxSkin", (x, y, path) =>
            {
                MapBuilder.EditMinedBoxSkin(Int32.Parse(x), Int32.Parse(y), path);
            });

            connection.On<string, string>("ReceiveMinedBoxCoordinates", (x, y) =>
            {
                MapBuilder.EditMinedBox(Int32.Parse(x), Int32.Parse(y));
            });

            
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
        }

        private void SendBoxCoordinates(object sender, KeyEventArgs e)
        {
            int[] temp;
            Point prevLoc = playerPictureBox.Location;
            temp = movement.SendBoxCoordinates(sender, e, editor, map, player);
            if (temp[0] == 0 && temp[1] == 0)
                return;

            //Jeigu dalinas is 5 be liekanos score
            if (editor.getScore() % 5 == 0 && !editor.getEffectIsGranted())
            {
                Random rnd = new Random();
                int randomNr = rnd.Next(1, 4);
                switch (randomNr)
                {
                    case 1:
                        player = new MineDeep(player);
                        editor.setEffectIsGranted(true);
                        break;
                    case 2:
                        player = new MineStronger(player);
                        editor.setEffectIsGranted(true);
                        break;
                   case 3:
                        player = new MineWide(player);
                        editor.setEffectIsGranted(true);
                        break;
                }
            }

            playerPictureBox.Location = new Point(temp[0], temp[1]);
            movement.FlipImage(playerPictureBox, prevLoc, false);
            

            _ = SendGetCoordinatesAsync(temp[0], temp[1]);

            if (e.KeyCode == Keys.W || e.KeyCode == Keys.Space || e.KeyCode == Keys.Q || e.KeyCode == Keys.E) 
                Thread.Sleep(25);

            movement.fall_down(temp, editor, map, player);
        }

        private async Task SendGetCoordinatesAsync(int x, int y)
        {
            await connection.InvokeAsync("SendCoordinates",
                    x.ToString(), y.ToString());
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            int mapx = 11;
            int mapy = 12;

            map = new Map.MapBase(mapx, mapy);
            map.setFactory(1);
            map.CreateMap();

            MapBuilder.AddPictureBoxes(playerPictureBox, enemyPictureBox, Controls, Size);

            MapBuilder.CreateMap(imageList1, map);
            _ = ServerObserver.SendMap(map);
            playerPictureBox.Show();
            enemyPictureBox.Show();
            editor.scoreZero();
            button1.Hide();
            button2.Hide();
            button3.Hide();
            textBox1.Hide();
            textBox2.Hide();
            label2.Hide();
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            label2.Enabled = false;
            KeyPreview = true;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox1.Text != string.Empty)
            {
                message.SendMessage(textBox1.Text);
                textBox1.Text = "";
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            message.UndoMessage();
        }
    }
}
