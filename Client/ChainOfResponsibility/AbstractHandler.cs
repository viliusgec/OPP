using Client.Adapter;
using Client.Decorator;
using Client.PictureBoxBuilder;
using Client.Strategy;
using Microsoft.AspNetCore.SignalR.Client;
using System.Drawing;

namespace Client.ChainOfResponsibility
{
    public abstract class AbstractHandler : IHandler
    {
        private IHandler _nextHandler;

        public IHandler SetNext(IHandler handler)
        {
            _nextHandler = handler;
            return handler;
        }

        public virtual IAlgorithm Handle(string key, int x, int y, FormsEditor editor, Map.MapBase map, Character player, MapBuilder mapBuilder, HubConnection connection, string room)
        {
            if (_nextHandler != null)
            {
                return _nextHandler.Handle(key, x, y, editor, map, player, mapBuilder, connection, room);
            }
            else
            {
                return null;
            }
        }

        public static bool Check_if_block_exists(int side, int x, int y, FormsEditor editor, Map.MapBase map, Character player, MapBuilder mapBuilder, HubConnection connection, string room)
        {
            BlockChecker blockchecker = new BlockCheckerAdapter(side, x, y, editor, map, connection, player, mapBuilder, room);

            return blockchecker.Check_if_block_exists();
        }
    }
}
