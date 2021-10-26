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
        private readonly HubConnection connection;
        ServerObserver ServerObserver = new();
        MapBuilder MapBuilder = new();
        Algorithm strategy;
        private Map.MapBase map;

        public GameForm()
        {
            InitializeComponent();
            label1.Text = "Controls:\nW/Space - jump\n A D - left, right\n Q - Jump up left \n E - jump up right\n SHIFT - dig down\n J - dig left\n K - dig right";
            connection = SingletonConnection.GetInstance().GetConnection();
            
            KeyPreview = true;
            KeyDown += SendBoxCoordinates;

            ServerObserver.ReceiveCoordinates(pictureBox2);
            MapBuilder = ServerObserver.ReceiveMap(map, pictureBox1, pictureBox2, button1, imageList1, Controls, Size);
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = PlayerBoxClass.Image;
        }

        /**
         * side meanings: 0 - down, 1 - up, 2 - left, 3 - right, 4 - up left, 5 - up right,
         * 6 - just to check if block exists, no action taken, 7 - Mine Left, 8 - Mine Right
         */
        private bool check_if_block_exists(int side, int x, int y)
        {
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
                    loc = new Point(x, y - pictureBox1.Height);
                    box = MapBuilder.GetPictureBox(loc);
                    if (box == null)
                        return true;
                    if (box.Enabled)
                        return false;
                    return true;
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
                case 4:
                    loc = new Point(x - pictureBox1.Width, y - pictureBox1.Height);
                    box = MapBuilder.GetPictureBox(loc);
                    if (box == null)
                        return true;
                    if (box.Enabled)
                        return false;
                    return true;
                case 5:
                    loc = new Point(x + pictureBox1.Width, y - pictureBox1.Height);
                    box = MapBuilder.GetPictureBox(loc);
                    if (box == null)
                        return true;
                    if (box.Enabled)
                        return false;
                    return true;
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
                    if (check_if_block_exists(1, x, y))
                        strategy = new Jump(x, y, pictureBox1.Height, pictureBox1.Width);
                    else
                        return;
                    break;
                case (Keys.ShiftKey):
                    if (check_if_block_exists(0, x, y))
                        strategy = new Mine(x, y, pictureBox1.Height, pictureBox1.Width);
                    else
                        return;
                    break;
                case (Keys.J):
                    //Cia padaryt, kad bloga pagal koordinates gauna ir jeigu health.
                 //   MapBuilder.GetBlock
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

            if (e.KeyCode == Keys.W || e.KeyCode == Keys.Space || e.KeyCode == Keys.Q || e.KeyCode == Keys.E)
                Thread.Sleep(25);

            while (check_if_block_exists(6, temp[0], temp[1]) == false)
            {
                pictureBox1.Location = new Point(temp[0], temp[1] + pictureBox1.Height);
                _ = SendGetCoordinatesAsync(temp[0], temp[1]);
                temp[1] += pictureBox1.Height;
                Thread.Sleep(25);
                if (temp[1] > (pictureBox1.Height)*15)
                        break;
            }
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
