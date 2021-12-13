using Client.Command;
using Client.Composite;
using Client.Decorator;
using Client.Interpreter;
using Client.Mediator;
using Client.Memento;
using Client.Observer;
using Client.PictureBoxBuilder;
using Client.State;
using Client.Strategy;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Facade : Form
    {
        private static HubConnection connection;
        private readonly ServerObserver ServerObserver = new();
        private MapBuilder MapBuilder = new();
        private readonly Movement movement;
        private ConcreteMediator mediator;
        private Map.MapBase map;
        private Command.SendMessage message;
        private Command.SendEmote emote;
        private FormsEditor editor;
        private Character player;
        private readonly List<ICommand> ChatCommands = new();
        private readonly Room room;
        private bool generator = false;
        private readonly StateContext stateContext = new(new StartState());
        private readonly Originator originator = new(new StartState());
        private readonly MementoController mementoController;


        public Facade(Room gameRoom)
        {
            mementoController = new(originator);
            room = gameRoom;

            connection = SingletonConnection.GetInstance().GetConnection();
            ElementsSet();
            movement = new Movement(connection, room.GetName());

            CommandPattern();

            KeyDown += SendBoxCoordinates;

            ServerObserver.ReceiveCoordinates(enemyPictureBox, movement);
            MapBuilder = ServerObserver.ReceiveMap(map, playerPictureBox, enemyPictureBox, button1, imageList1, Controls, Size);
            ConnectionsHandler();
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
        }

        private void CommandPattern()
        {
            message = new Command.SendMessage(textBox2);
            emote = new Command.SendEmote(textBox2);
            message.ReceiveUndoMessage();
            message.RecieveMessage();
        }

        private void ConnectionsHandler()
        {
            connection.On<string>("ReceiveMap", (jsonString) =>
            {
                if (!generator)
                {
                    editor.ScoreZero();
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
                    button4.Hide();
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
                    MapBuilder.CreateMap(new ImageList(), map);

                    //State
                    stateContext.TransitionTo(new StartState());
                    mementoController.Backup();
                    button5.Show();
                    button5.Enabled = true;
                }
            });

            connection.On<string, string, string>("ReceiveMinedBoxSkin", (x, y, path) =>
            {
                MapBuilder.EditMinedBoxSkin(int.Parse(x), int.Parse(y), path);
            });

            connection.On<string, string>("ReceiveCoordinates", (x, y) =>
            {
                Point prevLoc = enemyPictureBox.Location;
                enemyPictureBox.Location = new Point(int.Parse(x), int.Parse(y));
                movement.FlipImage(enemyPictureBox, prevLoc, true);
            });

            connection.On<string, string>("ReceiveMinedBoxCoordinates", (x, y) =>
            {
                MapBuilder.EditMinedBox(int.Parse(x), int.Parse(y));
                MapBuilder.BlocksFall(map, editor, int.Parse(x), int.Parse(y));
            });

            connection.On<string>("ReceiveState", (state) =>
            {
                if (state == "StartState")
                {
                    stateContext.TransitionTo(new StartState());
                    originator.SetState(new StartState());
                    mementoController.Backup();
                    gameStateLabel.Text = string.Empty;
                    button5.Enabled = true;
                    button5.Show();
                }
                else if (state == "PauseState")
                {
                    stateContext.TransitionTo(new PauseState());
                    originator.SetState(new PauseState());
                    mementoController.Backup();
                    gameStateLabel.Text = stateContext.ShowText();
                    button5.Text = "Resume";
                }
                else if (state == "ResumeState")
                {
                    stateContext.TransitionTo(new ResumeState());
                    originator.SetState(new ResumeState());
                    mementoController.Backup();
                    gameStateLabel.Text = stateContext.ShowText();
                    button5.Text = "Pause";
                }
                else if (state == "EndState")
                {
                    stateContext.TransitionTo(new EndState());
                    originator.SetState(new EndState());
                    mementoController.Backup();
                    gameStateLabel.Text = stateContext.ShowText();
                    gameStateLabel.Text += "\r\nYou lost!";
                    button5.Enabled = false;
                    button5.Hide();
                }
            });
        }

        private void ElementsSet()
        {
            InitializeComponent();
            gameStateLabel.Location = new Point((Width / 2) - 80, (Height / 2));
            player = new Player();
            MovementLabel.Text = "Controls:\nW/Space - jump\n A D - Left, Right\n Q - Jump Up Left \n E - Jump Up Right\n SHIFT - Dig Down\n J - Dig Left\n K - Dig Right\n B - Buy Menu\n Move Menu";
            Size = new Size(800, 800);

            FormsEditor tempEdit = new(playerPictureBox, enemyPictureBox, ScoreLabel, moneyLabel, buyMenu, buyMenuButton, buyMenuButtonMoney, moveMenu, moveMenuButton, imageList1, player, Controls, Size, MovementLabel2, room.GetName(), connection);

            editor = tempEdit;
        }

        private void SendBoxCoordinates(object sender, KeyEventArgs e)
        {
            if (stateContext.GetState().GetType().Name != "StartState")
            {
                return;
            }

            if (moveMenu.Enabled && e.KeyCode != Keys.Escape)
            {
                return;
            }

            int[] temp;
            Point prevLoc = playerPictureBox.Location;
            temp = movement.SendBoxCoordinates(sender, editor, map, player, MapBuilder, e);
            if (temp[0] == 0 && temp[1] == 0)
            {
                return;
            }

            //Strategy
            playerPictureBox.Location = new Point(temp[0], temp[1]);
            movement.FlipImage(playerPictureBox, prevLoc, false);


            _ = SendGetCoordinatesAsync(temp[0], temp[1]);

            if (e.KeyCode == Keys.W || e.KeyCode == Keys.Space || e.KeyCode == Keys.Q || e.KeyCode == Keys.E)
            {
                Thread.Sleep(25);
            }

            movement.Fall_down(temp, editor, map, player);
            Check_if_win();
        }

        public void Check_if_win()
        {
            if (playerPictureBox.Location.Y >= playerPictureBox.Height * 15)
            {
                //State
                stateContext.TransitionTo(new EndState());
                originator.SetState(new EndState());
                mementoController.Backup();
                gameStateLabel.Text = stateContext.ShowText();
                gameStateLabel.Text += "\r\nYou won!";
                button5.Enabled = false;
                button5.Hide();
                stateContext.SendState(room.GetName());
            }
        }

        private async Task SendGetCoordinatesAsync(int x, int y)
        {
            await connection.InvokeAsync("SendCoordinates",
                    x.ToString(), y.ToString(), room.GetName());
        }

        //Game start
        private void button1_Click_1(object sender, EventArgs e)
        {
            generator = true;
            int mapx = 11;
            int mapy = 12;

            //AbstractFactory, Factory, Bridge
            map = new Map.MapBase(mapx, mapy);
            ConcreteMediator temp_mediator = new(map, editor, MapBuilder, ServerObserver, room);
            mediator = temp_mediator;
            map.SetFactory(1); // pirmas mediator component 

            playerPictureBox.Show();
            enemyPictureBox.Show();
            editor.ScoreZero();
            button1.Hide();
            button2.Hide();
            button3.Hide();
            button4.Hide();
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
            //State
            button5.Enabled = true;
            button5.Show();
            stateContext.TransitionTo(new StartState());
            originator.SetState(new StartState());
            mementoController.Backup();
            stateContext.SendState(room.GetName());
            stateContext.ShowText();
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
            if (textBox1.Text != string.Empty)
            {
                message.Send(textBox1.Text, room.GetName());
                textBox1.Text = "";
                ChatCommands.Add(message);
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
            int index = ChatCommands.Count - 1;
            ICommand cmd = ChatCommands[index];
            ChatCommands.RemoveAt(index);
            cmd.Undo(room.GetName());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            emote.Send(textBox1.Text, room.GetName());
            textBox1.Text = "";
            ChatCommands.Add(emote);
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void gameStateButton_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (stateContext.GetState().GetType().Name == "PauseState")
            {
                stateContext.ChangeToNextState();
                button5.Text = "Pause";
                stateContext.SendState(room.GetName());
                gameStateLabel.Text = stateContext.ShowText();
                Thread.Sleep(3000);
                gameStateLabel.Text = string.Empty;
                mementoController.Undo();
                mementoController.Undo();
                stateContext.TransitionTo(mementoController._originator._state);
                stateContext.SendState(room.GetName());
                return;
            }

            stateContext.TransitionTo(new PauseState());
            originator.SetState(new PauseState());
            mementoController.Backup();
            gameStateLabel.Text = stateContext.ShowText();
            button5.Text = "Resume";
            stateContext.SendState(room.GetName());
        }

        private void moveMenuButton_Click(object sender, EventArgs e)
        {
            string enteredExpression = moveMenu.Text;
            if (enteredExpression != null)
            {
                ExpressionExecutor executive = new(enteredExpression, stateContext, moveMenu, playerPictureBox, movement, sender, editor, map, player, MapBuilder, connection, room, gameStateLabel, button5);
                moveMenu.ResetText();
            }
            //Tada cia sitoj vietos turi callint ta shit is facade
        }

        private void textBox3_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
