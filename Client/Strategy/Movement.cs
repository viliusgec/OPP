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
        bool playerLookingRight = true;
        bool enemyLookingRight = true;

        public Movement(HubConnection connection)
        {
            this.connection = connection;
        }

        public void FlipImage(PictureBox pictureBox, Point prevLoc, bool enemy)
        {
            bool lookingRight = playerLookingRight;
            if (enemy)
            {
                lookingRight = enemyLookingRight;
            }
            if (prevLoc.X > pictureBox.Location.X && lookingRight)
            {
                pictureBox.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                lookingRight = false;
            }
            else if (prevLoc.X < pictureBox.Location.X && !lookingRight)
            {
                pictureBox.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                lookingRight = true;
            }
            if (enemy)
            {
                enemyLookingRight = lookingRight;
            }
            else
            {
                playerLookingRight = lookingRight;
            }
            pictureBox.Refresh();
        }

        public int[] SendBoxCoordinates(object sender, KeyEventArgs e, FormsEditor editor, Map.MapBase map)
        {
            int x = editor.pictureBox1.Location.X;
            int y = editor.pictureBox1.Location.Y;
            int[] temp = { 0, 0 };



            switch (e.KeyCode)
            {
                case (Keys.Q):
                    if (check_if_block_exists(4, x, y, editor, map))
                        strategy = new MoveUpLeft(x, y, editor.pictureBox1.Height, editor.pictureBox1.Width);
                    else
                        return temp;
                    break;
                case (Keys.E):
                    if (check_if_block_exists(5, x, y, editor, map))
                        strategy = new MoveUpRight(x, y, editor.pictureBox1.Height, editor.pictureBox1.Width);
                    else
                        return temp;
                    break;
                case (Keys.A):
                    if (check_if_block_exists(2, x, y, editor, map))
                        strategy = new MoveLeft(x, y, editor.pictureBox1.Height, editor.pictureBox1.Width);
                    else
                        return temp;
                    break;
                case (Keys.D):
                    if (check_if_block_exists(3, x, y, editor, map))
                        strategy = new MoveRight(x, y, editor.pictureBox1.Height, editor.pictureBox1.Width);
                    else
                        return temp;
                    break;
                case (Keys.Space):
                case (Keys.W):
                    if (check_if_block_exists(1, x, y, editor, map))
                        strategy = new Jump(x, y, editor.pictureBox1.Height, editor.pictureBox1.Width);
                    else
                        return temp;
                    break;
                case (Keys.ShiftKey):
                    if (check_if_block_exists(0, x, y, editor, map))
                        strategy = new Mine(x, y, editor.pictureBox1.Height, editor.pictureBox1.Width);
                    else
                        return temp;
                    break;
                case (Keys.J):
                    if (check_if_block_exists(7, x, y, editor, map))
                        strategy = new MineLeft(x, y, editor.pictureBox1.Height, editor.pictureBox1.Width);
                    else
                        return temp;
                    break;
                case (Keys.K):
                    if (check_if_block_exists(8, x, y, editor, map))
                        strategy = new MineRight(x, y, editor.pictureBox1.Height, editor.pictureBox1.Width);
                    else
                        return temp;
                    break;
                case (Keys.B):
                    editor.buyMenu(); /// <<< šitaip accessinsim buy menu. Returninsim reikšmes ir editinsim žaidėjo stats.
                    return temp;
                default:
                    return temp;
            }

            temp = strategy.Behave(x, y, editor.pictureBox1.Height, editor.pictureBox1.Width);
            return temp;
        }
        /**
 * side meanings: 0 - down, 1 - up, 2 - left, 3 - right, 4 - up left, 5 - up right,
 * 6 - just to check if block exists, no action taken, 7 - Mine Left, 8 - Mine Right
 */
        private bool check_if_block_exists(int side, int x, int y, FormsEditor editor, Map.MapBase map)
        {
            var loc = new Point(x, y);
            var box = MapBuilder.GetPictureBox(loc);

            BlockChecker blockchecker = new BlockCheckerAdapter(side, x, y, editor, map, connection);

            return blockchecker.check_if_block_exists();
        }


        public void fall_down(int[] coords, FormsEditor editor, Map.MapBase map)
        {
            while (check_if_block_exists(6, coords[0], coords[1], editor, map) == false)
            {
                editor.pictureBox1.Location = new Point(coords[0], coords[1] + editor.pictureBox1.Height);
                _ = SendGetCoordinatesAsync(coords[0], coords[1] + editor.pictureBox1.Height);
                coords[1] += editor.pictureBox1.Height;
                Thread.Sleep(25);
                if (coords[1] > (editor.pictureBox1.Height) * 15)
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
