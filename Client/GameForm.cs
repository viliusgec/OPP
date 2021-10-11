using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.AspNetCore.SignalR.Client;
using System.Text.Json;
using System.IO;

namespace Client
{
    public partial class GameForm : Form
    {
        int mapx = 3;
        int mapy = 3;
        private HubConnection connection;
        private Map.MapBase map;
        PictureBox[,] boxes;
        public GameForm()
        {
            InitializeComponent();

            connection = SingletonConnection.GetInstance().GetConnection();
            
            this.KeyPreview = true;
            this.KeyDown += sendBoxCoordinates;
            
            connection.On<string, string>("ReceiveCoordinates", (x, y) =>
            {
                pictureBox2.Location = new Point(int.Parse(x), int.Parse(y));

            });
            connection.On<string>("ReceiveMap", (jsonString) =>
            {
                map = JsonSerializer.Deserialize<Map.MapBase>(jsonString);
                AddPictureBoxes();
                CreateMap();
                button1.Hide();
            });

        }


        private void AddPictureBoxes()
        {
            boxes = new PictureBox[mapx, mapy];
            boxes[0, 0] = pictureBox3;
            boxes[0, 1] = pictureBox4;
            boxes[0, 2] = pictureBox5;
            boxes[1, 0] = pictureBox6;
            boxes[1, 1] = pictureBox7;
            boxes[1, 2] = pictureBox8;
            boxes[2, 0] = pictureBox9;
            boxes[2, 1] = pictureBox10;
            boxes[2, 2] = pictureBox11;
        }

        

        private void CreateMap()
        {
            Map.Block[,] blocks = map.getBlocks();
            var l = blocks.Length;
            this.imageList1.ImageSize = new Size(40, 40);
            for (int i = 0; i < mapx; i++)
            {
                for(int j = 0; j < mapy; j++)
                {
                    string img = blocks[i, j].GetImage();
                    Image temp = Image.FromFile(img);
                    boxes[i, j].Image = temp;
                    imageList1.Images.Add(temp);
                }
            }
            
        }

        private void GameForm_Load(object sender, EventArgs e)
        {

        }
        private async Task sendMap(object sender)
        {
            string jsonString = JsonSerializer.Serialize(map);
            await connection.InvokeAsync("SendMap", jsonString);

        }
        private void sendBoxCoordinates(object sender, KeyEventArgs e)
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
            AddPictureBoxes();
            CreateMap();
            _ = sendMap(sender);
        }
    }
}
