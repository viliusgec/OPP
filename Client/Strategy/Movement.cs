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

namespace Client.Strategy
{
    class Movement
    {
        Algorithm strategy;
        HubConnection connection;

        public Movement(HubConnection connection)
        {
            this.connection = connection;
        }

        public int[] SendBoxCoordinates(object sender, KeyEventArgs e, PictureBox pictureBox1, Map.MapBase map)
        {
            int x = pictureBox1.Location.X;
            int y = pictureBox1.Location.Y;
            int[] temp = { 0, 0 };

            switch (e.KeyCode)
            {
                case (Keys.Q):
                    if (check_if_block_exists(4, x, y, pictureBox1, map))
                        strategy = new MoveUpLeft(x, y, pictureBox1.Height, pictureBox1.Width);
                    else
                        return temp;
                    break;
                case (Keys.E):
                    if (check_if_block_exists(5, x, y, pictureBox1, map))
                        strategy = new MoveUpRight(x, y, pictureBox1.Height, pictureBox1.Width);
                    else
                        return temp;
                    break;
                case (Keys.A):
                    if (check_if_block_exists(2, x, y, pictureBox1, map))
                        strategy = new MoveLeft(x, y, pictureBox1.Height, pictureBox1.Width);
                    else
                        return temp;
                    break;
                case (Keys.D):
                    if (check_if_block_exists(3, x, y, pictureBox1, map))
                        strategy = new MoveRight(x, y, pictureBox1.Height, pictureBox1.Width);
                    else
                        return temp;
                    break;
                case (Keys.Space):
                case (Keys.W):
                    if (check_if_block_exists(1, x, y, pictureBox1, map))
                        strategy = new Jump(x, y, pictureBox1.Height, pictureBox1.Width);
                    else
                        return temp;
                    break;
                case (Keys.ShiftKey):
                    if (check_if_block_exists(0, x, y, pictureBox1, map))
                        strategy = new Mine(x, y, pictureBox1.Height, pictureBox1.Width);
                    else
                        return temp;
                    break;
                case (Keys.J):
                    if (check_if_block_exists(7, x, y, pictureBox1, map))
                        strategy = new MineLeft(x, y, pictureBox1.Height, pictureBox1.Width);
                    else
                        return temp;
                    break;
                case (Keys.K):
                    if (check_if_block_exists(8, x, y, pictureBox1, map))
                        strategy = new MineRight(x, y, pictureBox1.Height, pictureBox1.Width);
                    else
                        return temp;
                    break;
                default:
                    return temp;
            }

            temp = strategy.Behave(x, y, pictureBox1.Height, pictureBox1.Width);
            return temp;
        }
        /**
 * side meanings: 0 - down, 1 - up, 2 - left, 3 - right, 4 - up left, 5 - up right,
 * 6 - just to check if block exists, no action taken, 7 - Mine Left, 8 - Mine Right
 */
        private bool check_if_block_exists(int side, int x, int y, PictureBox pictureBox1, Map.MapBase map)
        {
            var loc = new Point(x, y);
            var box = MapBuilder.GetPictureBox(loc);
           
           
           // Console.WriteLine(block.GetHealth());

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

        public void fall_down(int[] coords, PictureBox pictureBox1, Map.MapBase map)
        {
            while (check_if_block_exists(6, coords[0], coords[1], pictureBox1, map) == false)
            {
                pictureBox1.Location = new Point(coords[0], coords[1] + pictureBox1.Height);
                _ = SendGetCoordinatesAsync(coords[0], coords[1] + pictureBox1.Height);
                coords[1] += pictureBox1.Height;
                Thread.Sleep(25);
                if (coords[1] > (pictureBox1.Height) * 15)
                    break;
            }
        }

        private async Task SendGetCoordinatesAsync(int x, int y)
        {
            await connection.InvokeAsync("SendCoordinates",
                    x.ToString(), y.ToString());
        }
    }
}
