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
    public class BlockCheckerAdaptees
    {
        ServerObserver ServerObserver = new();

        public bool check_if_block_exists_specific(int side, int x, int y, FormsEditor editor, Map.MapBase map, HubConnection connection, Character player, MapBuilder mapBuilder, string room)
        {
            var loc = new Point(x, y);
            PictureBox box;
            Client.Map.Block block;
            int[] neededEffectScore = { 5, 10, 15, 20, 25, 30 };

            switch (side)
            {
                case 0:
                    int mineDeep = 1, mineStrongerDeep = 1;
                    PowerUpCount(ref mineDeep, ref mineStrongerDeep, player, "mineDeep");

                    var defaultLoc = new Point(x, y + editor.pictureBox1.Height);

                    for (int i = 1; i <= mineDeep; i++)
                    {
                        loc = new Point(x, y + editor.pictureBox1.Height * i);
                        box = MapBuilder.GetPictureBox(loc);
                        if (box == null) return false;
                        block = MapBuilder.GetBlock(loc, map);

                        if (block.GetBlockType() == "unbreakable")
                            return false;

                        block.SetHealth((int.Parse(block.GetHealth()) - (25 * mineStrongerDeep)).ToString());
                        block.SetImage("");

                        box.ImageLocation = block.GetImage();
                        _ = SendMinedBoxSkinAsync(box.Location.X, box.Location.Y, connection, block.GetImage(), room);
                        ServerObserver.ReceiveMinedBoxSkin();

                        if (int.Parse(block.GetHealth()) <= 0 && box.Enabled)
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

                    return false;
                case 1:
                    loc = new Point(x, y - editor.pictureBox1.Height);
                    box = MapBuilder.GetPictureBox(loc);
                    return CheckBox(box);
                case 2:
                    loc = new Point(x - editor.pictureBox1.Width, y);
                    box = MapBuilder.GetPictureBox(loc);
                    return mapBuilder.startX <= loc.X && CheckBox(box);
                case 3:
                    loc = new Point(x + editor.pictureBox1.Width, y);
                    box = MapBuilder.GetPictureBox(loc);
                    return (mapBuilder.endX - mapBuilder.boxWidth) >= loc.X && CheckBox(box);
                case 4:
                    loc = new Point(x - editor.pictureBox1.Width, y - editor.pictureBox1.Height);
                    box = MapBuilder.GetPictureBox(loc);
                    return CheckBox(box);
                case 5:
                    loc = new Point(x + editor.pictureBox1.Width, y - editor.pictureBox1.Height);
                    box = MapBuilder.GetPictureBox(loc);
                    return CheckBox(box);
                case 6:
                    loc = new Point(x, y + editor.pictureBox1.Height);
                    box = MapBuilder.GetPictureBox(loc);
                    return !CheckBox(box);
                case 7:
                    int mineWideLeft = 1, mineStrongerLeft = 1;
                    PowerUpCount(ref mineWideLeft, ref mineStrongerLeft, player, "mineWide");

                    var defaultLocLeft = new Point(x - editor.pictureBox1.Width, y);

                    for (int i = 1; i <= mineWideLeft; i++)
                    {
                        loc = new Point(x - editor.pictureBox1.Width * i, y);
                        box = MapBuilder.GetPictureBox(loc);
                        if (box == null) return false;
                        block = MapBuilder.GetBlock(loc, map);

                        if (block.GetBlockType() == "unbreakable")
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
                        if (box == null) return false;
                        block = MapBuilder.GetBlock(loc, map);

                        if (block.GetBlockType() == "unbreakable")
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

        public bool CheckBox(PictureBox? box)
        {
            if (box == null)
                return true;
            if (box.Enabled)
                return false;
            return true;
        }

        public void PowerUpCount(ref int mineWide, ref int mineStronger, Character player, string firstAbility)
        {
            var playerPowerUps = player.Mine("").Split(';');

            foreach (string playerPowerUp in playerPowerUps)
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
