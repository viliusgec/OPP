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
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace Client
{
    public partial class GameForm : Form
    {
        public static int boxWidth;
        public static int boxHeight;
        int width;
        int height;
        bool boxesAdded = false;
        int mapx = 10;
        int mapy = 10;
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
                map = JsonConvert.DeserializeObject<Map.MapBase>(jsonString);
                map.DeserializeBlocks();
                if (!boxesAdded)
                {
                    AddPictureBoxes();
                    boxesAdded = true;
                }
                CreateMap();
                button1.Hide();
            });
        }


        private void AddPictureBoxes()
        {
            boxes = new PictureBox[mapx, mapy];
            var formSize = this.Size;
            int width = formSize.Width;
            int height = formSize.Height;
            int startX = width / 5;
            int startY = height / 7;
            int endX = width / 5 * 4;
            int endY = height / 7 * 6;
            boxWidth = (endX - startX) / mapx;
            boxHeight = boxWidth;
            pictureBox1.Width = boxWidth;
            pictureBox1.Size = new Size(boxWidth, boxHeight);
            pictureBox2.Size = new Size(boxWidth, boxHeight);
            pictureBox1.Location = new Point(startX + ((mapx / 2 - 1) * boxWidth), startY - boxHeight);
            pictureBox2.Location = new Point(startX + (mapx / 2 * boxWidth), startY - boxHeight);
            for (int i = 0; i < mapx; i++)
            {
                for (int j = 0; j < mapy; j++)
                {
                    boxes[i, j] = new PictureBox();
                    boxes[i, j].Size  = new Size(boxWidth, boxHeight);
                    boxes[i, j].Location = new Point(startX + boxWidth * (i),startY + boxHeight * (j));
                    this.Controls.Add(boxes[i, j]);
                }
            }
            
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
            var options = new JsonSerializerOptions { WriteIndented = true };
            //string jsonString = JsonSerializer.Serialize(map, options);
            map.SerializeBlocks();
            string jsonString = JsonConvert.SerializeObject(map);
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
            if (!boxesAdded)
            {
                AddPictureBoxes();
                boxesAdded = true;
            }
                
            CreateMap();
            //_ = sendMap(sender);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
    }
}
