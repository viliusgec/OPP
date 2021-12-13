using Client.Decorator;
using Client.Map;
using Client.PictureBoxBuilder;
using Client.Strategy;
using Microsoft.AspNetCore.SignalR.Client;
using System.Diagnostics;


namespace Client.ChainOfResponsibility
{
    internal class MoveUpRightHandler : AbstractHandler
    {
        public override IAlgorithm Handle(string key, int x, int y, FormsEditor editor, MapBase map, Character player, MapBuilder mapBuilder, HubConnection connection, string room)
        {
            if (key == "E")
            {
                Debug.WriteLine("The key was " + key + ": Handled by MoveUpRight Handler");
                if (Check_if_block_exists(5, x, y, editor, map, player, mapBuilder, connection, room))
                {
                    return new MoveUpRight(x, y, editor.playerPictureBox.Height, editor.playerPictureBox.Width);
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
