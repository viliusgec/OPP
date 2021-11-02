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

        public bool check_if_block_exists_specific(int side, int x, int y, FormsEditor editor, Map.MapBase map, HubConnection connection)
        {
            var loc = new Point(x, y);
            var box = MapBuilder.GetPictureBox(loc);

            switch (side)
            {
                case 0:
                    loc = new Point(x, y + editor.pictureBox1.Height);
                    box = MapBuilder.GetPictureBox(loc);
                    var blockDown = MapBuilder.GetBlock(loc, map);
                 
                    blockDown.SetHealth((Int32.Parse(blockDown.GetHealth()) - 25).ToString());

                    if (box == null)
                        return false;

                    blockDown.SetImage("");

                    box.ImageLocation = blockDown.GetImage();
                    _ = SendMinedBoxSkinAsync(box.Location.X, box.Location.Y, connection, blockDown.GetImage());
                    ServerObserver.ReceiveMinedBoxSkin();

                    if (Int32.Parse(blockDown.GetHealth()) <= 0)
                    {

                        if (box.Enabled)
                        {
                            box.Hide();
                            box.Enabled = false;
                            _ = SendMinedBoxCoordinatesAsync(box.Location.X, box.Location.Y, connection);
                            ServerObserver.ReceiveMinedBoxCoordinates();
                            editor.addScore();
                            return true;
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
                    loc = new Point(x - editor.pictureBox1.Width, y);
                    box = MapBuilder.GetPictureBox(loc);
                    var blockLeft = MapBuilder.GetBlock(loc, map);
                    blockLeft.SetHealth((Int32.Parse(blockLeft.GetHealth()) - 25).ToString());

                    if (box == null)
                        return false;

                    blockLeft.SetImage("");
                    box.ImageLocation = blockLeft.GetImage();
                    _ = SendMinedBoxSkinAsync(box.Location.X, box.Location.Y, connection, blockLeft.GetImage());
                    ServerObserver.ReceiveMinedBoxSkin();

                    if (Int32.Parse(blockLeft.GetHealth()) <= 0)
                    {

                        if (box.Enabled)
                        {
                            box.Hide();
                            box.Enabled = false;
                            _ = SendMinedBoxCoordinatesAsync(box.Location.X, box.Location.Y, connection);
                            ServerObserver.ReceiveMinedBoxCoordinates();
                            editor.addScore();
                            return true;
                        }
                    }

                    return false;
                case 8:
                    loc = new Point(x + editor.pictureBox1.Width, y);
                    box = MapBuilder.GetPictureBox(loc);
                    var blockRight = MapBuilder.GetBlock(loc, map);
                    blockRight.SetHealth((Int32.Parse(blockRight.GetHealth()) - 25).ToString());

                    if (box == null)
                        return false;

                    blockRight.SetImage("");
                    box.ImageLocation = blockRight.GetImage();
                    _ = SendMinedBoxSkinAsync(box.Location.X, box.Location.Y, connection, blockRight.GetImage());
                    ServerObserver.ReceiveMinedBoxSkin();

                    if (Int32.Parse(blockRight.GetHealth()) <= 0)
                    {

                        if (box.Enabled)
                        {
                            box.Hide();
                            box.Enabled = false;
                            _ = SendMinedBoxCoordinatesAsync(box.Location.X, box.Location.Y, connection);
                            ServerObserver.ReceiveMinedBoxCoordinates();
                            editor.addScore();
                            return true;
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
