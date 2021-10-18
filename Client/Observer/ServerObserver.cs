using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System.Drawing;
using System.Windows.Forms;

namespace Client.Observer
{
    public class ServerObserver
    {
        private readonly HubConnection connection;
        bool boxesAdded;
        public static int boxWidth;
        public static int boxHeight;
        int mapx = 10;
        int mapy = 10;
        PictureBox[,] boxes;

        public ServerObserver()
        {
            connection = SingletonConnection.GetInstance().GetConnection();
            boxesAdded = false;
        }

        public void ReceiveCoordinates(PictureBox pictureBox)
        {
            connection.On<string, string>("ReceiveCoordinates", (x, y) =>
            {
                pictureBox.Location = new Point(int.Parse(x), int.Parse(y));
            });
        }

        public void ReceiveMap(Map.MapBase map, PictureBox pictureBox1, PictureBox pictureBox2, Button button1, ImageList imageList1, Control.ControlCollection control, Size size)
        {
            connection.On<string>("ReceiveMap", (jsonString) =>
            {
                map = JsonConvert.DeserializeObject<Map.MapBase>(jsonString);
                map.DeserializeBlocks();
                if (!boxesAdded)
                {
                    AddPictureBoxes(pictureBox1, pictureBox2, control, size);
                    boxesAdded = true;
                }
                CreateMap(imageList1, map);
                button1.Hide();
            });
        }

        private void AddPictureBoxes(PictureBox pictureBox1, PictureBox pictureBox2, Control.ControlCollection controls, Size size)
        {
            boxes = new PictureBox[mapx, mapy];
            var formSize = size;
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
                    boxes[i, j].Size = new Size(boxWidth, boxHeight);
                    boxes[i, j].Location = new Point(startX + boxWidth * (i), startY + boxHeight * (j));
                    controls.Add(boxes[i, j]);
                }
            }
        }

        private void CreateMap(ImageList imageList1, Map.MapBase map)
        {
            Map.Block[,] blocks = map.getBlocks();
            var l = blocks.Length;
            imageList1.ImageSize = new Size(40, 40);
            for (int i = 0; i < mapx; i++)
            {
                for (int j = 0; j < mapy; j++)
                {
                    string img = blocks[i, j].GetImage();
                    Image temp = Image.FromFile(img);
                    boxes[i, j].Image = temp;
                    imageList1.Images.Add(temp);
                }
            }
        }
        /*   
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
                  });*/
    }
}
