using Client.Decorator;
using Client.Map;
using Client.PictureBoxBuilder;
using Client.Strategy;
using Microsoft.AspNetCore.SignalR.Client;
using System.Diagnostics;


namespace Client.ChainOfResponsibility
{
    class MoveUpLeftHandler : AbstractHandler
    {
        public override Algorithm Handle(string key, int x, int y, FormsEditor editor, MapBase map, Character player, MapBuilder mapBuilder, HubConnection connection, string room)
        {
            if (key == "Q")
            {
                Debug.WriteLine("The key was " + key + ": Handled by MoveUpLeft Handler");
                if (this.check_if_block_exists(4, x, y, editor, map, player, mapBuilder, connection, room))
                {
                    return new MoveUpLeft(x, y, editor.playerPictureBox.Height, editor.playerPictureBox.Width);
                }
                Debug.WriteLine("But, there's a block blocking my path.");
                return null;
            }
            else
            {
                return base.Handle(key, x, y, editor, map, player, mapBuilder, connection, room);
            }
        }
    }
}
