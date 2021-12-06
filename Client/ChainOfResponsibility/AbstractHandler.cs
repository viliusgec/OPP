using Client.Adapter;
using Client.Decorator;
using Client.PictureBoxBuilder;
using Client.Strategy;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ChainOfResponsibility
{
    public abstract class AbstractHandler : IHandler
    {
        private IHandler _nextHandler;

        public IHandler SetNext(IHandler handler)
        {
            this._nextHandler = handler;
            return handler;
        }

        public virtual Algorithm Handle(string key, int x, int y, FormsEditor editor, Map.MapBase map, Character player, MapBuilder mapBuilder, HubConnection connection, string room)
        {
            if (this._nextHandler != null)
            {
                return this._nextHandler.Handle(key, x, y, editor, map, player, mapBuilder, connection, room);
            }
            else
            {
                return null;
            }
        }

        public bool check_if_block_exists(int side, int x, int y, FormsEditor editor, Map.MapBase map, Character player, MapBuilder mapBuilder, HubConnection connection, string room)
        {
            var loc = new Point(x, y);
            var box = MapBuilder.GetPictureBox(loc);

            BlockChecker blockchecker = new BlockCheckerAdapter(side, x, y, editor, map, connection, player, mapBuilder, room);

            return blockchecker.check_if_block_exists();
        }
    }
}
