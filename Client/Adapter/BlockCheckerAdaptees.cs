using Client.Decorator;
using Client.Observer;
using Client.PictureBoxBuilder;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.Adapter
{
    class BlockCheckerAdaptees
    {
        ServerObserver ServerObserver = new();

        public bool check_if_block_exists_specific(int side, int x, int y, FormsEditor editor, Map.MapBase map, HubConnection connection, Character player, MapBuilder mapBuilder, string room)
        {
            var loc = new Point(x, y);
            var box = MapBuilder.GetPictureBox(loc);
            var block = MapBuilder.GetBlock(loc, map);
            int[] neededEffectScore = { 5, 10, 15, 20, 25, 30 };

            switch (side)
            {
                case 0:
                    int mineDeep = 1, mineStrongerDeep = 1;
                    PowerUpCount(ref mineDeep, ref mineStrongerDeep, player, "mineDeep");

                    var defaultLoc = new Point(x, y + editor.pictureBox1.Height);

                    for (int i = 1; i <= mineDeep; i++)
                    {
                        box = GetBox(x, y + editor.pictureBox1.Height * i);
                        block = MapBuilder.GetBlock(loc, map);

                        if (block.GetBlockType() == "unbreakable" || box == null)
                            return false;

                        block.SetHealth((int.Parse(block.GetHealth()) - (25 * mineStrongerDeep)).ToString());
                        block.SetImage("");

                        box.ImageLocation = block.GetImage();
                        _ = SendMinedBoxSkinAsync(box.Location.X, box.Location.Y, connection, block.GetImage(), room);
                        ServerObserver.ReceiveMinedBoxSkin();

                        if (int.Parse(block.GetHealth()) <= 0)
                        {
                            if (box.Enabled)
                            {
                                box.Hide();
                                box.Enabled = false;
                                _ = SendMinedBoxCoordinatesAsync(box.Location.X, box.Location.Y, connection, room);
                                ServerObserver.ReceiveMinedBoxCoordinates(mapBuilder, map, editor);
                                editor.addScore();

                                if (neededEffectScore.Contains(editor.getScore())) editor.setEffectIsGranted(false);

                                return loc == defaultLoc;
                            }
                        }
                    }
                    return false;
                case 1:
                    box = GetBox(x, y - editor.pictureBox1.Height);
                    return CheckBox(box);
                case 2:
                    box = GetBox(x - editor.pictureBox1.Width, y);
                    if (box == null) return false;
                    return mapBuilder.startX <= box.Location.X && CheckBox(box);
                case 3:
                    box = GetBox(x + editor.pictureBox1.Width, y);
                    //is loc.X i box.location.x paziet ar veikia
                    if (box == null) return false;
                    return (mapBuilder.endX - mapBuilder.boxWidth) >= box.Location.X && CheckBox(box);
                case 4:
                    box = GetBox(x - editor.pictureBox1.Width, y - editor.pictureBox1.Height);
                    return CheckBox(box);
                case 5:
                    box = GetBox(x + editor.pictureBox1.Width, y - editor.pictureBox1.Height);
                    return CheckBox(box);
                case 6:

                    box = GetBox(x, y + editor.pictureBox1.Height);
                    return !CheckBox(box);
                case 7:
                    int mineWideLeft = 1, mineStrongerLeft = 1;
                    PowerUpCount(ref mineWideLeft, ref mineStrongerLeft, player, "mineWide");

                    var defaultLocLeft = new Point(x - editor.pictureBox1.Width, y);

                    for (int i = 1; i <= mineWideLeft; i++)
                    {
                        loc = new Point(x - editor.pictureBox1.Width * i, y);
                        box = MapBuilder.GetPictureBox(loc);
                        block = MapBuilder.GetBlock(loc, map);

                        if (block.GetBlockType() == "unbreakable" || box == null)
                            return false;

                        block.SetHealth((int.Parse(block.GetHealth()) - (25 * mineStrongerLeft)).ToString());

                        block.SetImage("");
                        box.ImageLocation = block.GetImage();
                        _ = SendMinedBoxSkinAsync(box.Location.X, box.Location.Y, connection, block.GetImage(), room);
                        ServerObserver.ReceiveMinedBoxSkin();

                        if (int.Parse(block.GetHealth()) <= 0 && box.Enabled)
                        {
                            box.Hide();
                            box.Enabled = false;
                            _ = SendMinedBoxCoordinatesAsync(box.Location.X, box.Location.Y, connection, room);
                            mapBuilder.BlocksFall(map, editor, box.Location.X, box.Location.Y);
                            ServerObserver.ReceiveMinedBoxCoordinates(mapBuilder, map, editor);
                            editor.addScore();

                            if (neededEffectScore.Contains(editor.getScore())) editor.setEffectIsGranted(false);

                            return loc == defaultLocLeft;
                        }
                    }

                    return false;
                case 8:
                    int mineWideRight = 1, mineStrongerRight = 1;
                    PowerUpCount(ref mineWideRight, ref mineStrongerRight, player, "mineWide");

                    var defaultLocRight = new Point(x + editor.pictureBox1.Width, y);

                    for (int i = 1; i <= mineWideRight; i++)
                    {
                        loc = new Point(x + editor.pictureBox1.Width * i, y);
                        box = MapBuilder.GetPictureBox(loc);
                        block = MapBuilder.GetBlock(loc, map);

                        if (block.GetBlockType() == "unbreakable" || box == null)
                            return false;

                        block.SetHealth((int.Parse(block.GetHealth()) - (25 * mineStrongerRight)).ToString());

                        block.SetImage("");
                        box.ImageLocation = block.GetImage();
                        _ = SendMinedBoxSkinAsync(box.Location.X, box.Location.Y, connection, block.GetImage(), room);
                        ServerObserver.ReceiveMinedBoxSkin();

                        if (int.Parse(block.GetHealth()) <= 0 && box.Enabled)
                        {
                            box.Hide();
                            box.Enabled = false;
                            _ = SendMinedBoxCoordinatesAsync(box.Location.X, box.Location.Y, connection, room);
                            mapBuilder.BlocksFall(map, editor, box.Location.X, box.Location.Y);
                            ServerObserver.ReceiveMinedBoxCoordinates(mapBuilder, map, editor);
                            editor.addScore();

                            if (neededEffectScore.Contains(editor.getScore())) editor.setEffectIsGranted(false);

                            return loc == defaultLocRight;
                        }
                    }
                    return false;
                default:
                    return false;
            }
        }
        private async Task SendMinedBoxCoordinatesAsync(int x, int y, HubConnection connection, string room)
        {
            await connection.InvokeAsync("SendMinedBoxCoordinates",
                    x.ToString(), y.ToString(), room);
        }

        private async Task SendMinedBoxSkinAsync(int x, int y, HubConnection connection, string path, string room)
        {
            await connection.InvokeAsync("SendMinedBoxSkin",
                    x.ToString(), y.ToString(), path, room);
        }

        private bool CheckBox(PictureBox? box)
        {
            if (box == null)
                return true;
            if (box.Enabled)
                return false;
            return true;
        }

        private PictureBox GetBox(int x, int y)
        {
            var loc = new Point(x, y);
            return MapBuilder.GetPictureBox(loc);
        }

        private void PowerUpCount(ref int mineWide, ref int mineStronger, Character player, string firstAbility)
        {
            var playerPowerUpsRight = player.Mine("").Split(';');

            foreach (string playerPowerUp in playerPowerUpsRight)
            {
                if (playerPowerUp == firstAbility)
                {
                    mineWide++;
                }
                if (playerPowerUp == "mineStronger")
                {
                    mineStronger++;
                }
            }
        }
    }
}
