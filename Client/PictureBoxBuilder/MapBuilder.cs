using Client.Map;
using System.Drawing;
using System.Windows.Forms;

namespace Client.PictureBoxBuilder
{
    public class MapBuilder
    {
        public int boxWidth;
        public int boxHeight;
        readonly static int mapx = 11;
        readonly static int mapy = 12;
        static PictureBox[,] boxes;
        public bool boxesAdded = false;
        public int startX;
        public int startY;
        public int endX;
        public int endY;

        public MapBuilder()
        {

        }

        public void BlocksFall(Map.MapBase map, FormsEditor editor, int x, int y)
        {
            var loc = new Point(x, y - boxHeight);
            if (GetPictureBox(loc) != null && GetPictureBox(loc).Enabled == true)
            {
                var block = GetBlock(loc, map);
                if (block.GetBlockType() == "falling") 
                {
                    while(GetPictureBox(loc).Enabled != false && block != null && block.GetBlockType() == "falling")
                    {
                        var oldBlock = GetBlock(loc, map);
                        var newBlock = GetBlock(new Point(x, y), map);
                        var tempBlock = (Map.Block)newBlock.Clone();

                        if (oldBlock != null && oldBlock.GetBlockType() == "falling")
                        {
                            newBlock.SetBlockType(oldBlock.GetBlockType());
                            newBlock.SetEffect(oldBlock.GetEffect());
                            newBlock.SetName(oldBlock.GetName());
                            newBlock.SetHealth(oldBlock.GetHealth());
                            newBlock.SetImage(oldBlock.GetImage());

                            oldBlock.SetBlockType(tempBlock.GetBlockType());
                            oldBlock.SetEffect(tempBlock.GetEffect());
                            oldBlock.SetName(tempBlock.GetName());
                            oldBlock.SetHealth(tempBlock.GetHealth());
                            oldBlock.SetImage(tempBlock.GetImage());

                            GetPictureBox(new Point(x, y)).Enabled = true;
                            GetPictureBox(loc).Enabled = false;
                            GetPictureBox(new Point(x, y)).Show();
                            GetPictureBox(loc).Hide();

                            GetPictureBox(new Point(x, y)).ImageLocation = newBlock.GetImage();
                            GetPictureBox(new Point(x, y)).Refresh();
                            y -= boxHeight;
                            loc = new Point(x, y - boxHeight);
                            block = GetBlock(loc, map);
                            if (block == null)
                                break;
                            
                        }
                        else
                            break;
                        
                    }
                }
            }
        }

        public static PictureBox GetPictureBox(Point loc)
        {
            for (int i = 0; i < mapx; i++)
            {
                for (int j = 0; j < mapy+1; j++)
                {
                    if(boxes[i,j].Location == loc)
                    {
                        return boxes[i, j];
                    }
                }
            }
            return null;
        }

        public static Map.Block GetBlock(Point loc, Map.MapBase map)
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
        public static void SetPictureBox(Point loc, PictureBox box)
        {
            for (int i = 0; i < mapx; i++)
            {
                for (int j = 0; j < mapy + 1; j++)
                {
                    if (boxes[i, j].Location == loc)
                    {
                        boxes[i, j] = box;
                    }
                }
            }
        }

        public static void SetBlock(Point loc, Map.MapBase map, Map.Block block)
        {
            for (int i = 0; i < mapx; i++)
            {
                for (int j = 0; j < mapy; j++)
                {
                    if (boxes[i, j].Location == loc)
                    {
                        var blox = map.getBlocks();
                        blox[i, j] = block;
                        map.setBlocks(blox);
                    }
                }
            }
        }

        public void AddPictureBoxes(PictureBox pictureBox1, PictureBox pictureBox2, Control.ControlCollection controls, Size size)
        {
            boxes = new PictureBox[mapx, mapy+1];
            var formSize = size;
            int width = formSize.Width;
            int height = formSize.Height;
            startX = width / 5;
            startY = height / 5;
            endX = width / 5 * 4;
            endY = height / 7 * 6;
            boxWidth = (endX - startX) / mapx;
            boxHeight = boxWidth;
            pictureBox1.Width = boxWidth;
            pictureBox1.Size = new Size(boxWidth, boxHeight);
            pictureBox2.Size = new Size(boxWidth, boxHeight);
            pictureBox1.Location = new Point(startX + ((mapx / 2 - 1) * boxWidth), startY - boxHeight);
            pictureBox2.Location = new Point(startX + ((mapx / 2 + 1) * boxWidth), startY - boxHeight);
            //boxes[mapx, 0] = new PictureBox();
            //boxes[mapx, 0].Size = new Size(boxWidth, boxHeight);
            //boxes[mapx, 0].Location = new Point(startX + boxWidth * (i), startY + boxHeight * (j));
            //boxes[mapx, 0].SizeMode = PictureBoxSizeMode.StretchImage;
            for (int i = 0; i < mapx; i++)
            {
                for (int j = 0; j < mapy+1; j++)
                {
                    boxes[i, j] = new PictureBox();
                    boxes[i, j].BackColor = System.Drawing.Color.Transparent;
                    boxes[i, j].Size = new Size(boxWidth, boxHeight);
                    boxes[i, j].Location = new Point(startX + boxWidth * (i), startY + boxHeight * (j));
                    boxes[i, j].SizeMode = PictureBoxSizeMode.StretchImage;
                    
                    if(j == mapy && i < 2)
                    {
                        boxes[i, j] = new PictureBox();
                        boxes[i, j].Size = new Size(boxWidth, boxHeight);
                        boxes[i, j].Location = new Point(startX + ((mapx / 2) * boxWidth), startY - boxHeight * (i+1));
                        boxes[i, j].SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                    else if(j == mapy && i >= 2)
                    {
                        boxes[i, j] = new PictureBox();
                        boxes[i, j].Size = new Size(0, 0);
                        boxes[i, j].Location = new Point(0,0);
                        boxes[i, j].SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                    controls.Add(boxes[i, j]);
                }
            }
        }

        public void EditMinedBox(int x, int y)
        {
            for (int i = 0; i < mapx; i++)
            {
                for (int j = 0; j < mapy; j++)
                {
                    if (boxes[i, j].Location == new Point(x, y))
                    {
                        boxes[i, j].Hide();
                        boxes[i, j].Enabled = false;
                    }
                }
            }
        }

        public void EditMinedBoxSkin(int x, int y, string path)
        {
            for (int i = 0; i < mapx; i++)
            {
                for (int j = 0; j < mapy; j++)
                {
                    if (boxes[i, j].Location == new Point(x, y))
                    {
                        boxes[i, j].ImageLocation = path;
                    }
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
            AbstractFactory factory = new L1Factory();
            boxes[0, mapy].Image = Image.FromFile(factory.GetUnbreakable().GetImage());
            boxes[1, mapy].Image = Image.FromFile(factory.GetUnbreakable().GetImage());
            imageList1.Images.Add(boxes[0, mapy].Image);
            imageList1.Images.Add(boxes[1, mapy].Image);
        }
    }
}
