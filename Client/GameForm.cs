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
        Algorithm strategy;

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

        /**
         * side meanings: 0 - down, 1 - up, 2 - left, 3 - right, 4 - up left, 5 - up right, 6 - just to check if block exists, no action taken;
         */
        private bool check_if_block_exists(int side, int x, int y)
        {
            // Gali pagriebt picturer boxà pagal locationà, pasiþiûrët ar geras,
            // o po to pasiimt blockà jei reikia
            var loc = new Point(x, y);
            var box = MapBuilder.GetPictureBox(loc);

            switch (side) 
            {
                case 0:
                    loc = new Point(x, y + pictureBox1.Height);
                    box = MapBuilder.GetPictureBox(loc);
                    if (box == null)
                        return false;
                    if (box.Enabled)
                    {
                        box.Hide();
                        box.Enabled = false;
                        return true;
                    }
                    return false;
                case 1:

                    break;
                case 2:
                    loc = new Point(x - pictureBox1.Width, y);
                    box = MapBuilder.GetPictureBox(loc);
                    if (box == null)
                        return true;
                    if (box.Enabled)
                        return false;
                    return true;
                case 3:
                    loc = new Point(x + pictureBox1.Width, y);
                    box = MapBuilder.GetPictureBox(loc);
                    if (box == null)
                        return true;
                    if (box.Enabled)
                        return false;
                    return true;
                    break;
                case 4:
                    loc = new Point(x - pictureBox1.Width, y - pictureBox1.Height);
                    box = MapBuilder.GetPictureBox(loc);
                    if (box == null)
                        return true;
                    if (box.Enabled)
                        return false;
                    return true;
                    break;
                case 5:
                    loc = new Point(x + pictureBox1.Width, y - pictureBox1.Height);
                    box = MapBuilder.GetPictureBox(loc);
                    if (box == null)
                        return true;
                    if (box.Enabled)
                        return false;
                    return true;
                    break;
                case 6:
                    loc = new Point(x, y + pictureBox1.Height);
                    box = MapBuilder.GetPictureBox(loc);
                    if (box == null)
                        return false;
                    if (box.Enabled)
                        return true;
                    return false;
                case 7:
                    loc = new Point(x - pictureBox1.Width, y);
                    box = MapBuilder.GetPictureBox(loc);
                    if (box == null)
                        return false;
                    if (box.Enabled)
                    {
                        box.Hide();
                        box.Enabled = false;
                        return true;
                    }
                    return false;
                case 8:
                    loc = new Point(x + pictureBox1.Width, y);
                    box = MapBuilder.GetPictureBox(loc);
                    if (box == null)
                        return false;
                    if (box.Enabled)
                    {
                        box.Hide();
                        box.Enabled = false;
                        return true;
                    }
                    return false;
                default:
                    return false;
            }
            return true;
        }
        private void SendBoxCoordinates(object sender, KeyEventArgs e)
        {
            int x = pictureBox1.Location.X;
            int y = pictureBox1.Location.Y;
            int[] temp;
            
            switch(e.KeyCode)
            {
                case (Keys.Q):
                    if (check_if_block_exists(4, x, y))
                        strategy = new MoveUpLeft(x, y, pictureBox1.Height, pictureBox1.Width);
                    else
                        return;
                    break;
                case (Keys.E):
                    if (check_if_block_exists(5, x, y))
                        strategy = new MoveUpRight(x, y, pictureBox1.Height, pictureBox1.Width);
                    else
                        return;
                    break;
                case (Keys.A):
                    if (check_if_block_exists(2, x, y))
                        strategy = new MoveLeft(x, y, pictureBox1.Height, pictureBox1.Width);
                    else
                        return;
                    break;
                case (Keys.D):
                    if (check_if_block_exists(3, x, y))
                        strategy = new MoveRight(x, y, pictureBox1.Height, pictureBox1.Width);
                    else
                        return;
                    break;
                case (Keys.Space):
                case (Keys.W):
                    strategy = new Jump(x, y, pictureBox1.Height, pictureBox1.Width);
                    break;
                case (Keys.ShiftKey):
                    if (check_if_block_exists(0, x, y))
                        strategy = new Mine(x, y, pictureBox1.Height, pictureBox1.Width);
                    else
                        return;
                    break;
                case (Keys.J):
                    if (check_if_block_exists(7, x, y))
                        strategy = new MineLeft(x, y, pictureBox1.Height, pictureBox1.Width);
                    else
                        return;
                    break;
                case (Keys.K):
                    if (check_if_block_exists(8, x, y))
                        strategy = new MineRight(x, y, pictureBox1.Height, pictureBox1.Width);
                    else
                        return;
                    break;
                default:
                    return;
            }

            temp = strategy.Behave(x, y, pictureBox1.Height, pictureBox1.Width);

            pictureBox1.Location = new Point(temp[0], temp[1]);
            _ = SendGetCoordinatesAsync(temp[0], temp[1]);

            if (e.KeyCode == Keys.W || e.KeyCode == Keys.Space)
            {
                Thread.Sleep(100);
            }

            while (check_if_block_exists(6, temp[0], temp[1]) == false)
            {
                pictureBox1.Location = new Point(temp[0], temp[1] + pictureBox1.Height);
                _ = SendGetCoordinatesAsync(temp[0], temp[1]);
                temp[1] += pictureBox1.Height;
                Thread.Sleep(50);
                if (temp[1] > (pictureBox1.Height)*15)
                        break;
            }

            Console.WriteLine("aa");
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
    }
}
