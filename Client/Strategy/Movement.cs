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
using Client.Decorator;
using Client.ChainOfResponsibility;
using Microsoft.Extensions.Logging;

namespace Client.Strategy
{
    public class Movement
    {
        ServerObserver ServerObserver = new();
        Algorithm strategy;
        HubConnection connection;
        bool playerLookingRight = true;
        bool enemyLookingRight = true;
        string room;
        JumpHandler handler;

        public Movement(HubConnection connection, string room)
        {
            
            this.connection = connection;
            this.room = room;
            handler = new JumpHandler();
            var handler2 = new MineHandler();
            var handler3 = new MineLeftHandler();
            var handler4 = new MineRightHandler();
            var handler5 = new MoveLeftHandler();
            var handler6 = new MoveRightHandler();
            var handler7 = new MoveUpLeftHandler();
            var handler8 = new MoveUpRightHandler();
            handler.SetNext(handler2).SetNext(handler3).SetNext(handler4).SetNext(handler5).SetNext(handler6).SetNext(handler7).SetNext(handler8);
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

        public int[] SendBoxCoordinates(object sender, KeyEventArgs e, FormsEditor editor, Map.MapBase map, Character player, MapBuilder mapBuilder)
        {
            int x = editor.playerPictureBox.Location.X;
            int y = editor.playerPictureBox.Location.Y;
            int[] temp = { 0, 0 };

            var key = e.KeyCode.ToString();
            Console.WriteLine(key);
            var strategy = handler.Handle(key, x, y, editor, map, player, mapBuilder, connection, room);
            
            switch (e.KeyCode)
            {/*
                case (Keys.Q):
                    if (check_if_block_exists(4, x, y, editor, map, player, mapBuilder))
                        strategy = new MoveUpLeft(x, y, editor.playerPictureBox.Height, editor.playerPictureBox.Width);
                    else
                        return temp;
                    break;
                case (Keys.E):
                    if (check_if_block_exists(5, x, y, editor, map, player, mapBuilder))
                        strategy = new MoveUpRight(x, y, editor.playerPictureBox.Height, editor.playerPictureBox.Width);
                    else
                        return temp;
                    break;
                case (Keys.A):
                    if (check_if_block_exists(2, x, y, editor, map, player, mapBuilder))
                        strategy = new MoveLeft(x, y, editor.playerPictureBox.Height, editor.playerPictureBox.Width);
                    else
                        return temp;
                    break;
                case (Keys.D):
                    if (check_if_block_exists(3, x, y, editor, map, player, mapBuilder))
                        strategy = new MoveRight(x, y, editor.playerPictureBox.Height, editor.playerPictureBox.Width);
                    else
                        return temp;
                    break;
                case (Keys.Space):
                case (Keys.W):
                    if (check_if_block_exists(1, x, y, editor, map, player, mapBuilder))
                        strategy = new Jump(x, y, editor.playerPictureBox.Height, editor.playerPictureBox.Width);
                    else
                        return temp;
                    break;
                case (Keys.ShiftKey):
                    if (check_if_block_exists(0, x, y, editor, map, player, mapBuilder))
                        strategy = new Mine(x, y, editor.playerPictureBox.Height, editor.playerPictureBox.Width);
                    else
                        return temp;
                    break;
                case (Keys.J):
                    if (check_if_block_exists(7, x, y, editor, map, player, mapBuilder))
                        strategy = new MineLeft(x, y, editor.playerPictureBox.Height, editor.playerPictureBox.Width);
                    else
                        return temp;
                    break;
                case (Keys.K):
                    if (check_if_block_exists(8, x, y, editor, map, player, mapBuilder))
                        strategy = new MineRight(x, y, editor.playerPictureBox.Height, editor.playerPictureBox.Width);
                    else
                        return temp;
                    break;*/
                case (Keys.B):
                    editor.buyMenu(player); /// <<< šitaip accessinsim buy menu. Returninsim reikšmes ir editinsim žaidėjo stats.
                    return temp;
                case (Keys.Escape):
                    editor.closeBuyMmenu();
                    return temp;
                default:
                    break;
            }
            if(strategy != null)
                temp = strategy.Behave(x, y, editor.playerPictureBox.Height, editor.playerPictureBox.Width);
            return temp;
        }
        /**
         * side meanings: 0 - down, 1 - up, 2 - left, 3 - right, 4 - up left, 5 - up right,
         * 6 - just to check if block exists, no action taken, 7 - Mine Left, 8 - Mine Right
         */
        private bool check_if_block_exists(int side, int x, int y, FormsEditor editor, Map.MapBase map, Character player, MapBuilder mapBuilder)
        {
            var loc = new Point(x, y);
            var box = MapBuilder.GetPictureBox(loc);

            BlockChecker blockchecker = new BlockCheckerAdapter(side, x, y, editor, map, connection, player, mapBuilder, room);

            return blockchecker.check_if_block_exists();
        }


        public void fall_down(int[] coords, FormsEditor editor, Map.MapBase map, Character player)
        {
            while (check_if_block_exists(6, coords[0], coords[1], editor, map, player,new MapBuilder()) == false)
            {
                editor.playerPictureBox.Location = new Point(coords[0], coords[1] + editor.playerPictureBox.Height);
                _ = SendGetCoordinatesAsync(coords[0], coords[1] + editor.playerPictureBox.Height);
                coords[1] += editor.playerPictureBox.Height;
                Thread.Sleep(10);
                if (coords[1] > (editor.playerPictureBox.Height) * 15)
                    break;
            }
        }

        private async Task SendGetCoordinatesAsync(int x, int y)
        {
            await connection.InvokeAsync("SendCoordinates",
                    x.ToString(), y.ToString(), room);
        }

      
    }
}
