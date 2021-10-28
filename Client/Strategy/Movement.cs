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
using Client.Adapter;

namespace Client.Strategy
{
    class Movement
    {
        ServerObserver ServerObserver = new();
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

            BlockChecker blockchecker = new BlockCheckerAdapter(side, x, y, pictureBox1, map, connection);

            return blockchecker.check_if_block_exists();
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
