using Client.Decorator;
using Client.Observer;
using Client.PictureBoxBuilder;
using Microsoft.AspNetCore.SignalR.Client;
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
        ServerObserver ServerObserver = new();

        public bool check_if_block_exists_specific(int side, int x, int y, FormsEditor editor, Map.MapBase map, HubConnection connection, Character player)
        {
            var loc = new Point(x, y);
            var box = MapBuilder.GetPictureBox(loc);
            var block = MapBuilder.GetBlock(loc, map);

            switch (side)
            {
                case 0:
                    var playerPowerUpsDeep = player.Mine("").Split(';');

                    int mineDeep = 1;
                    int mineStrongerDeep = 1;
                    foreach (string playerPowerUp in playerPowerUpsDeep)
                    {
                        if (playerPowerUp == "mineDeep")
                        {
                            mineDeep++;
                        }
                        if (playerPowerUp == "mineStronger")
                        {
                            mineStrongerDeep++;
                        }
                    }

                    var defaultLoc = new Point(x, y + editor.pictureBox1.Height);

                    for (int i = 1; i <= mineDeep; i++)
                    {
                        loc = new Point(x, y + editor.pictureBox1.Height * i);
                        box = MapBuilder.GetPictureBox(loc);

                        if (box == null)
                            return false;

                        block = MapBuilder.GetBlock(loc, map);

                        if (block.GetBlockType() == "unbreakable")
                            return false;

                        block.SetHealth((Int32.Parse(block.GetHealth()) - (25 * mineStrongerDeep)).ToString());
                        block.SetImage("");

                        box.ImageLocation = block.GetImage();
                        _ = SendMinedBoxSkinAsync(box.Location.X, box.Location.Y, connection, block.GetImage());
                        ServerObserver.ReceiveMinedBoxSkin();

                        if (Int32.Parse(block.GetHealth()) <= 0)
                        {

                            if (box.Enabled)
                            {
                                box.Hide();
                                box.Enabled = false;
                                _ = SendMinedBoxCoordinatesAsync(box.Location.X, box.Location.Y, connection);
                                ServerObserver.ReceiveMinedBoxCoordinates();
                                editor.addScore();

                                if (editor.getScore() == 5 || editor.getScore() == 10 || editor.getScore() == 15 || editor.getScore() == 20 || editor.getScore() == 25 || editor.getScore() == 30)
                                {
                                    editor.setEffectIsGranted(false);
                                }

                                if (loc == defaultLoc)
                                {
                                    return true;
                                } else
                                {
                                    return false;
                                }
                            }
                        }
                    }

                    return false;
                case 1:
                    loc = new Point(x, y - editor.pictureBox1.Height);
                    box = MapBuilder.GetPictureBox(loc);
                    if (box == null)
                        return true;
                    if (box.Enabled)
                        return false;
                    return true;
                case 2:
                    loc = new Point(x - editor.pictureBox1.Width, y);
                    box = MapBuilder.GetPictureBox(loc);
                    if (box == null)
                        return true;
                    if (box.Enabled)
                        return false;
                    return true;
                case 3:
                    loc = new Point(x + editor.pictureBox1.Width, y);
                    box = MapBuilder.GetPictureBox(loc);
                    if (box == null)
                        return true;
                    if (box.Enabled)
                        return false;
                    return true;
                case 4:
                    loc = new Point(x - editor.pictureBox1.Width, y - editor.pictureBox1.Height);
                    box = MapBuilder.GetPictureBox(loc);
                    if (box == null)
                        return true;
                    if (box.Enabled)
                        return false;
                    return true;
                case 5:
                    loc = new Point(x + editor.pictureBox1.Width, y - editor.pictureBox1.Height);
                    box = MapBuilder.GetPictureBox(loc);
                    if (box == null)
                        return true;
                    if (box.Enabled)
                        return false;
                    return true;
                case 6:
                    loc = new Point(x, y + editor.pictureBox1.Height);
                    box = MapBuilder.GetPictureBox(loc);
                    if (box == null)
                        return false;
                    if (box.Enabled)
                        return true;
                    return false;
                case 7:
                    var playerPowerUpsLeft = player.Mine("").Split(';');

                    int mineWideLeft = 1;
                    int mineStrongerLeft = 1;
                    foreach (string playerPowerUp in playerPowerUpsLeft)
                    {
                        if (playerPowerUp == "mineWide")
                        {
                            mineWideLeft++;
                        }
                        if (playerPowerUp == "mineStronger")
                        {
                            mineStrongerLeft++;
                        }
                    }

                    var defaultLocLeft = new Point(x - editor.pictureBox1.Width, y);

                    for (int i = 1; i <= mineWideLeft; i++)
                    {
                        loc = new Point(x - editor.pictureBox1.Width * i, y);
                        box = MapBuilder.GetPictureBox(loc);

                        if (box == null)
                            return false;

                        block = MapBuilder.GetBlock(loc, map);

                        if (block.GetBlockType() == "unbreakable")
                            return false;

                        block.SetHealth((Int32.Parse(block.GetHealth()) - (25 * mineStrongerLeft)).ToString());

                        block.SetImage("");
                        box.ImageLocation = block.GetImage();
                        _ = SendMinedBoxSkinAsync(box.Location.X, box.Location.Y, connection, block.GetImage());
                        ServerObserver.ReceiveMinedBoxSkin();

                        if (Int32.Parse(block.GetHealth()) <= 0)
                        {

                            if (box.Enabled)
                            {
                                box.Hide();
                                box.Enabled = false;
                                _ = SendMinedBoxCoordinatesAsync(box.Location.X, box.Location.Y, connection);
                                ServerObserver.ReceiveMinedBoxCoordinates();
                                editor.addScore();

                                if (editor.getScore() == 5 || editor.getScore() == 10 || editor.getScore() == 15 || editor.getScore() == 20 || editor.getScore() == 25 || editor.getScore() == 30)
                                {
                                    editor.setEffectIsGranted(false);
                                }

                                if (loc == defaultLocLeft)
                                {
                                    return true;
                                } else
                                {
                                    return false;
                                }
                            }
                        }
                    }

                    return false;
                case 8:
                    var playerPowerUpsRight = player.Mine("").Split(';');

                    int mineWideRight = 1;
                    int mineStrongerRight = 1;
                    foreach (string playerPowerUp in playerPowerUpsRight)
                    {
                        if (playerPowerUp == "mineWide")
                        {
                            mineWideRight++;
                        }
                        if (playerPowerUp == "mineStronger")
                        {
                            mineStrongerRight++;
                        }
                    }

                    var defaultLocRight = new Point(x + editor.pictureBox1.Width, y);

                    for (int i = 1; i <= mineWideRight; i++)
                    {
                        loc = new Point(x + editor.pictureBox1.Width * i, y);
                        box = MapBuilder.GetPictureBox(loc);

                        if (box == null)
                            return false;

                        block = MapBuilder.GetBlock(loc, map);

                        if (block.GetBlockType() == "unbreakable")
                            return false;

                        block.SetHealth((Int32.Parse(block.GetHealth()) - (25 * mineStrongerRight)).ToString());

                        block.SetImage("");
                        box.ImageLocation = block.GetImage();
                        _ = SendMinedBoxSkinAsync(box.Location.X, box.Location.Y, connection, block.GetImage());
                        ServerObserver.ReceiveMinedBoxSkin();

                        if (Int32.Parse(block.GetHealth()) <= 0)
                        {

                            if (box.Enabled)
                            {
                                box.Hide();
                                box.Enabled = false;
                                _ = SendMinedBoxCoordinatesAsync(box.Location.X, box.Location.Y, connection);
                                ServerObserver.ReceiveMinedBoxCoordinates();
                                editor.addScore();

                                if (editor.getScore() == 5 || editor.getScore() == 10 || editor.getScore() == 15 || editor.getScore() == 20 || editor.getScore() == 25 || editor.getScore() == 30)
                                {
                                    editor.setEffectIsGranted(false);
                                }

                                if (loc == defaultLocRight)
                                {
                                    return true;
                                } else
                                {
                                    return false;
                                }
                            }
                        }
                    }

                    return false;
                default:
                    return false;
            }
        }
        private async Task SendMinedBoxCoordinatesAsync(int x, int y, HubConnection connection)
        {
            await connection.InvokeAsync("SendMinedBoxCoordinates",
                    x.ToString(), y.ToString());
        }

        private async Task SendMinedBoxSkinAsync(int x, int y, HubConnection connection, string path)
        {
            await connection.InvokeAsync("SendMinedBoxSkin",
                    x.ToString(), y.ToString(), path);
        }
    }
}
