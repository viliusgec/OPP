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
        private static HubConnection connection;
        ServerObserver ServerObserver = new();
        MapBuilder MapBuilder = new();
        Movement movement;
        private Map.MapBase map;

        public GameForm()
        {
            InitializeComponent();
            label1.Text = "Controls:\nW/Space - jump\n A D - left, right\n Q - Jump up left \n E - jump up right\n SHIFT - dig down\n J - dig left\n K - dig right";
            connection = SingletonConnection.GetInstance().GetConnection();
            movement = new Movement(connection);

            KeyPreview = true;
            KeyDown += SendBoxCoordinates;


            ServerObserver.ReceiveCoordinates(pictureBox2);
            MapBuilder = ServerObserver.ReceiveMap(map, pictureBox1, pictureBox2, button1, imageList1, Controls, Size);
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = PlayerBoxClass.Image;
        }

        private void SendBoxCoordinates(object sender, KeyEventArgs e)
        {
            int[] temp;

            temp = movement.SendBoxCoordinates(sender, e, pictureBox1);

            if (temp[0] == 0 && temp[1] == 0)
                return;

            pictureBox1.Location = new Point(temp[0], temp[1]);
            _ = SendGetCoordinatesAsync(temp[0], temp[1]);

            if (e.KeyCode == Keys.W || e.KeyCode == Keys.Space || e.KeyCode == Keys.Q || e.KeyCode == Keys.E) 
                Thread.Sleep(25);

            movement.fall_down(temp, pictureBox1);
        }

        private async Task SendGetCoordinatesAsync(int x, int y)
        {
            await connection.InvokeAsync("SendCoordinates",
                    x.ToString(), y.ToString());
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            bool boxesAdded = false;
            int mapx = 10;
            int mapy = 10;

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
            button1.Enabled = false;
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
    }
}
