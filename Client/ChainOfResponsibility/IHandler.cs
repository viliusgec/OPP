using Client.Decorator;
using Client.Map;
using Client.PictureBoxBuilder;
using Client.Strategy;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ChainOfResponsibility
{
    public interface IHandler
    {
        IHandler SetNext(IHandler handler);

        Algorithm Handle(string key, int x, int y, FormsEditor editor, Map.MapBase map, Character player, MapBuilder mapBuilder, HubConnection connection, string room);
        //Algorithm Handle(string key, int x, int y, FormsEditor editor, MapBase map, Character player, MapBuilder mapBuilder, HubConnection connection, string room);
    }
}
