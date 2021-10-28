using Client.PictureBoxBuilder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.Adapter
{
    class BlockCheckerAdaptees
    {
        public bool check_if_block_exists_specific(int side, int x, int y, PictureBox pictureBox1, Map.MapBase map)
        {
            var loc = new Point(x, y);
            var box = MapBuilder.GetPictureBox(loc);

            switch (side)
            {
                case 0:
                    loc = new Point(x, y + pictureBox1.Height);
                    box = MapBuilder.GetPictureBox(loc);
                    var blockDown = MapBuilder.GetBlock(loc, map);
                    blockDown.SetHealth((Int32.Parse(blockDown.GetHealth()) - 25).ToString());

                    if (box == null)

                        return false;

                    if (Int32.Parse(blockDown.GetHealth()) <= 0)
                    {

                        if (box.Enabled)
                        {
                            box.Hide();
                            box.Enabled = false;

                            return true;
                        }
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
                    var blockLeft = MapBuilder.GetBlock(loc, map);
                    blockLeft.SetHealth((Int32.Parse(blockLeft.GetHealth()) - 25).ToString());

                    if (box == null)
                        return false;

                    if (Int32.Parse(blockLeft.GetHealth()) <= 0)
                    {

                        if (box.Enabled)
                        {
                            box.Hide();
                            box.Enabled = false;
                            return true;
                        }
                    }

                    return false;
                case 8:
                    loc = new Point(x + pictureBox1.Width, y);
                    box = MapBuilder.GetPictureBox(loc);
                    var blockRight = MapBuilder.GetBlock(loc, map);
                    blockRight.SetHealth((Int32.Parse(blockRight.GetHealth()) - 25).ToString());

                    if (box == null)
                        return false;

                    if (Int32.Parse(blockRight.GetHealth()) <= 0)
                    {

                        if (box.Enabled)
                        {
                            box.Hide();
                            box.Enabled = false;
                            return true;
                        }
                    }

                    return false;
                default:
                    return false;
            }
        }
    }
}
