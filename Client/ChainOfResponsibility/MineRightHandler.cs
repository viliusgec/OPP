using Client.Decorator;
using Client.Map;
using Client.PictureBoxBuilder;
using Client.Strategy;
using Microsoft.AspNetCore.SignalR.Client;
using System.Diagnostics;


namespace Client.ChainOfResponsibility
{
    class MineRightHandler : AbstractHandler
    {
        public override Algorithm Handle(string key, int x, int y, FormsEditor editor, MapBase map, Character player, MapBuilder mapBuilder, HubConnection connection, string room)
        {
            if (key == "K")
            {
                Debug.WriteLine("The key was " + key + ": Handled by MineRight Handler");
                if (this.check_if_block_exists(8, x, y, editor, map, player, mapBuilder, connection, room))
                {
                    return new MineRight(x, y, editor.playerPictureBox.Height, editor.playerPictureBox.Width);
                }
                Debug.WriteLine("But, the block didn't exist.");
                return null;
            }
            else
            {
                return base.Handle(key, x, y, editor, map, player, mapBuilder, connection, room);
            }
        }
    }
}
