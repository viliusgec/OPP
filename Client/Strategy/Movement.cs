using Client.Adapter;
using Client.ChainOfResponsibility;
using Client.Decorator;
using Client.Observer;
using Client.PictureBoxBuilder;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.Strategy
{
    public class Movement
    {
        private readonly ServerObserver ServerObserver = new();
        private readonly IAlgorithm strategy;
        private readonly HubConnection connection;
        private bool playerLookingRight = true;
        private bool enemyLookingRight = true;
        private readonly string room;
        private readonly JumpHandler handler;

        public Movement(HubConnection connection, string room)
        {

            this.connection = connection;
            this.room = room;
            handler = new JumpHandler();
            MineHandler handler2 = new();
            MineLeftHandler handler3 = new();
            MineRightHandler handler4 = new();
            MoveLeftHandler handler5 = new();
            MoveRightHandler handler6 = new();
            MoveUpLeftHandler handler7 = new();
            MoveUpRightHandler handler8 = new();
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

        public int[] SendBoxCoordinates(object sender, FormsEditor editor, Map.MapBase map, Character player, MapBuilder mapBuilder, KeyEventArgs? e = null, string? convertedExpression = null)
        {
            int x = editor.playerPictureBox.Location.X;
            int y = editor.playerPictureBox.Location.Y;
            int[] temp = { 0, 0 };

            string key = "";

            if (e != null)
            {
                key = e.KeyCode.ToString();
            }
            else
            {
                key = convertedExpression;
            }

            Console.WriteLine(key);
            IAlgorithm strategy = handler.Handle(key, x, y, editor, map, player, mapBuilder, connection, room);

            if (e != null)
            {
                switch (e.KeyCode)
                {
                    case (Keys.B):
                        editor.BuyMenu(); /// <<< šitaip accessinsim buy menu. Returninsim reikšmes ir editinsim žaidėjo stats.
                        return temp;
                    case (Keys.Escape):
                        editor.CloseBuyMmenu();
                        editor.CloseMoveMenu();
                        return temp;
                    case (Keys.M):
                        editor.MoveMenu(player);
                        return temp;
                    default:
                        break;
                }
            }

            if (strategy != null)
            {
                temp = strategy.Behave(x, y, editor.playerPictureBox.Height, editor.playerPictureBox.Width);
            }

            return temp;
        }
        /**
         * side meanings: 0 - down, 1 - up, 2 - left, 3 - right, 4 - up left, 5 - up right,
         * 6 - just to check if block exists, no action taken, 7 - Mine Left, 8 - Mine Right
         */
        private bool Check_if_block_exists(int side, int x, int y, FormsEditor editor, Map.MapBase map, Character player, MapBuilder mapBuilder)
        {
            Point loc = new(x, y);
            PictureBox box = MapBuilder.GetPictureBox(loc);

            BlockChecker blockchecker = new BlockCheckerAdapter(side, x, y, editor, map, connection, player, mapBuilder, room);

            return blockchecker.Check_if_block_exists();
        }


        public void Fall_down(int[] coords, FormsEditor editor, Map.MapBase map, Character player)
        {
            while (Check_if_block_exists(6, coords[0], coords[1], editor, map, player, new MapBuilder()) == false)
            {
                editor.playerPictureBox.Location = new Point(coords[0], coords[1] + editor.playerPictureBox.Height);
                _ = SendGetCoordinatesAsync(coords[0], coords[1] + editor.playerPictureBox.Height);
                coords[1] += editor.playerPictureBox.Height;
                Thread.Sleep(10);
                if (coords[1] > (editor.playerPictureBox.Height) * 15)
                {
                    break;
                }
            }
        }

        private async Task SendGetCoordinatesAsync(int x, int y)
        {
            await connection.InvokeAsync("SendCoordinates",
                    x.ToString(), y.ToString(), room);
        }


    }
}
