using System.Drawing;
using System.Windows.Forms;

namespace Client.PictureBoxBuilder
{
    public class MapBuilder
    {
        public int boxWidth;
        public int boxHeight;
        readonly static int mapx = 10;
        readonly static int mapy = 10;
        static PictureBox[,] boxes;
        public bool boxesAdded = false;

        public MapBuilder()
        {

        }

        public static PictureBox GetPictureBox(Point loc)
        {
            for (int i = 0; i < mapx; i++)
            {
                for (int j = 0; j < mapy; j++)
                {
                    if(boxes[i,j].Location == loc)
                    {
                        return boxes[i, j];
                    }
                }
            }
            return null;
        }

        public Map.Block GetBlock(Point loc, Map.MapBase map)
        {
            for (int i = 0; i < mapx; i++)
            {
                for (int j = 0; j < mapy; j++)
                {
                    if (boxes[i, j].Location == loc)
                    {
                        return map.getBlocks()[i, j];
                    }
                }
            }
            return null;
        }

        public void AddPictureBoxes(PictureBox pictureBox1, PictureBox pictureBox2, Control.ControlCollection controls, Size size)
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
            //getPickaxesResized
    
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

        public void CreateMap(ImageList imageList1, Map.MapBase map)
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
    }
}
