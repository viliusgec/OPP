using Client.Decorator;
using Client.Observer;
using Client.PictureBoxBuilder;
using Microsoft.AspNetCore.SignalR.Client;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.Adapter
{
    public class BlockCheckerAdaptees
    {
        private readonly ServerObserver ServerObserver = new();

        public bool Check_if_block_exists_specific(int side, int x, int y, FormsEditor editor, Map.MapBase map, HubConnection connection, Character player, MapBuilder mapBuilder, string room)
        {
            Point loc;
            PictureBox box;
            Client.Map.Block block;

            switch (side)
            {
                case 0:
                    Point defaultLoc = new(x, y + editor.playerPictureBox.Height);
                    loc = new Point(x, y + editor.playerPictureBox.Height);
                    box = MapBuilder.GetPictureBox(loc);
                    if (box == null)
                    {
                        return false;
                    }

                    block = MapBuilder.GetBlock(loc, map);
                    if (block == null)
                    {
                        return false;
                    }

                    if (block.GetBlockType() == "unbreakable")
                    {
                        return false;
                    }

                    block.SetHealth((int.Parse(block.GetHealth()) - (player.GetStr())).ToString()); ;
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
                        editor.AddPlayerScore();
                        editor.AddPlayerMoney(block.GetPoints());

                        return loc == defaultLoc;
                    }


                    return false;
                case 1:
                    loc = new Point(x, y - editor.playerPictureBox.Height);
                    box = MapBuilder.GetPictureBox(loc);
                    return CheckBox(box);
                case 2:
                    loc = new Point(x - editor.playerPictureBox.Width, y);
                    box = MapBuilder.GetPictureBox(loc);
                    return mapBuilder.startX <= loc.X && CheckBox(box);
                case 3:
                    loc = new Point(x + editor.playerPictureBox.Width, y);
                    box = MapBuilder.GetPictureBox(loc);
                    return (mapBuilder.endX - mapBuilder.boxWidth) >= loc.X && CheckBox(box);
                case 4:
                    loc = new Point(x - editor.playerPictureBox.Width, y - editor.playerPictureBox.Height);
                    box = MapBuilder.GetPictureBox(loc);
                    return mapBuilder.startX <= loc.X && CheckBox(box);
                case 5:
                    loc = new Point(x + editor.playerPictureBox.Width, y - editor.playerPictureBox.Height);
                    box = MapBuilder.GetPictureBox(loc);
                    return (mapBuilder.endX - mapBuilder.boxWidth) >= loc.X && CheckBox(box);
                case 6:
                    loc = new Point(x, y + editor.playerPictureBox.Height);
                    box = MapBuilder.GetPictureBox(loc);
                    return !CheckBox(box);
                case 7:
                    Point defaultLocLeft = new(x - editor.playerPictureBox.Width, y);


                    loc = new Point(x - editor.playerPictureBox.Width, y);
                    box = MapBuilder.GetPictureBox(loc);
                    if (box == null)
                    {
                        return false;
                    }

                    block = MapBuilder.GetBlock(loc, map);
                    if (block == null)
                    {
                        return false;
                    }

                    if (block.GetBlockType() == "unbreakable")
                    {
                        return false;
                    }

                    block.SetHealth((int.Parse(block.GetHealth()) - (player.GetStr())).ToString());

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
                        editor.AddPlayerScore();
                        editor.AddPlayerMoney(block.GetPoints());

                        return loc == defaultLocLeft;
                    }

                    return false;
                case 8:

                    Point defaultLocRight = new(x + editor.playerPictureBox.Width, y);

                    loc = new Point(x + editor.playerPictureBox.Width, y);
                    box = MapBuilder.GetPictureBox(loc);
                    if (box == null)
                    {
                        return false;
                    }

                    block = MapBuilder.GetBlock(loc, map);

                    if (block == null)
                    {
                        return false;
                    }

                    if (block.GetBlockType() == "unbreakable")
                    {
                        return false;
                    }

                    block.SetHealth((int.Parse(block.GetHealth()) - (player.GetStr())).ToString());

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
                        editor.AddPlayerScore();
                        editor.AddPlayerMoney(block.GetPoints());

                        return loc == defaultLocRight;
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
            {
                return true;
            }

            if (box.Enabled)
            {
                return false;
            }

            return true;
        }

        public void PowerUpCount(ref int mineWide, ref int mineStronger, Character player, string firstAbility)
        {
            string[] playerPowerUps = player.Mine("").Split(';');

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
