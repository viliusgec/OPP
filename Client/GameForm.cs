using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.AspNetCore.SignalR.Client;
using System.Text.Json;
using Newtonsoft.Json;
using System.Xml.Serialization;
using Client.Strategy;
using System.Threading;
using Client.Observer;
using Client.PictureBoxBuilder;

namespace Client
{
    public partial class GameForm : Form
    {
        public int boxWidth;
        public int boxHeight;
        bool boxesAdded = false;
        readonly int mapx = 10;
        readonly int mapy = 10;
        private readonly HubConnection connection;
        private Map.MapBase map;
        ServerObserver ServerObserver = new();
        MapBuilder MapBuilder = new();

        public GameForm()
        {
            InitializeComponent();

            connection = SingletonConnection.GetInstance().GetConnection();
            
            KeyPreview = true;
            KeyDown += SendBoxCoordinates;

            ServerObserver.ReceiveCoordinates(pictureBox2);
            ServerObserver.ReceiveMap(map, pictureBox1, pictureBox2, button1, imageList1, Controls, Size);
        }

        private void GameForm_Load(object sender, EventArgs e)
        {

        }

        private void SendBoxCoordinates(object sender, KeyEventArgs e)
        {
            int x = pictureBox1.Location.X;
            int y = pictureBox1.Location.Y;
            int[] temp;

            switch (e.KeyCode)
            {
                case Keys.A:
                    strategy = new MoveLeft(x, y);
                    break;
                case Keys.D:
                    strategy = new MoveRight(x, y);
                    break;
                //case Keys.Space:
                case Keys.W:
                    strategy = new Jump(x, y);
                    break;
                case Keys.LShiftKey:
                    strategy = new Mine(x, y);
                    break;
                default:
                    strategy = new MoveLeft(x, y);
                    break;
            }

            temp = strategy.Behave(x, y);
            // TODO: mine
            pictureBox1.Location = new Point(temp[0], temp[1]);
            _ = SendGetCoordinatesAsync(temp[0], temp[1]);

            if (e.KeyCode == Keys.W || e.KeyCode == Keys.Space)
                JumpController(temp);
        }

        private void JumpController(int[] coords)
        {
            Thread.Sleep(100);
            pictureBox1.Location = new Point(coords[0], coords[1]+42);
        }

        private async Task SendGetCoordinatesAsync(int x, int y)
        {
            await connection.InvokeAsync("SendCoordinates",
                    x.ToString(), y.ToString());
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            map = new Map.MapBase(mapx, mapy);
            map.setFactory(1);
            map.CreateMap();
            if (!boxesAdded)
            {
                MapBuilder.AddPictureBoxes(pictureBox1, pictureBox2, Controls, Size);
                boxesAdded = true;
            }

            MapBuilder.CreateMap(imageList1, map);
            _ = ServerObserver.SendMap(map);
            button1.Hide();
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
    }
}
