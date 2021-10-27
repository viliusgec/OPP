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

        public GameForm()
        {
            InitializeComponent();
            label1.Text = "Controls:\nW/Space - jump\n A D - left, right\n Q - Jump up left \n E - jump up right\n SHIFT - dig down\n J - dig left\n K - dig right";
            connection = SingletonConnection.GetInstance().GetConnection();
            movement = new Movement(connection);

            message = new Command.Message(textBox2);
            message.ReceiveUndoMessage();
            message.RecieveMessage();
            

            KeyPreview = false;
            KeyDown += SendBoxCoordinates;

            ServerObserver.ReceiveCoordinates(pictureBox2);
            MapBuilder = ServerObserver.ReceiveMap(map, pictureBox1, pictureBox2, button1, imageList1, Controls, Size);
            connection.On<string>("ReceiveMap", (jsonString) =>
            {
                map = ServerObserver.GetMap();
                MapBuilder = ServerObserver.GetBuilder();
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

            temp = movement.SendBoxCoordinates(sender, e, pictureBox1, map);

            if (temp[0] == 0 && temp[1] == 0)
                return;

            pictureBox1.Location = new Point(temp[0], temp[1]);
            _ = SendGetCoordinatesAsync(temp[0], temp[1]);

            if (e.KeyCode == Keys.W || e.KeyCode == Keys.Space || e.KeyCode == Keys.Q || e.KeyCode == Keys.E) 
                Thread.Sleep(25);

            movement.fall_down(temp, pictureBox1, map);
        }

        private async Task SendGetCoordinatesAsync(int x, int y)
        {
            await connection.InvokeAsync("SendCoordinates",
                    x.ToString(), y.ToString());
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            int mapx = 10;
            int mapy = 10;

            map = new Map.MapBase(mapx, mapy);
            map.setFactory(1);
            map.CreateMap();
            MapBuilder.AddPictureBoxes(pictureBox1, pictureBox2, Controls, Size);

            MapBuilder.CreateMap(imageList1, map);
            _ = ServerObserver.SendMap(map);
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
