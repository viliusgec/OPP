using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.AspNetCore.SignalR.Client;
using System.Text.Json;
using Newtonsoft.Json;
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

            if (e.KeyCode == Keys.D) x += 10;
            else if (e.KeyCode == Keys.A) x -= 10;
            else if (e.KeyCode == Keys.W) y -= 10;
            else if (e.KeyCode == Keys.S) y += 10;
            pictureBox1.Location = new Point(x, y);
            _ = SendGetCoordinatesAsync(x, y);
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
    }
}
