using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace SignalRChat.Hubs
{
    public class GameHub : Hub
    {
        public async Task SendCoordinates(string x, string y)
        {
            await Clients.Others.SendAsync("ReceiveCoordinates", x, y);
        }
        public async Task SendMap(string x)
        {
            await Clients.Others.SendAsync("ReceiveMap", x);
        }

        public async Task SendMinedBoxCoordinates(string x, string y)
        {
            await Clients.Others.SendAsync("ReceiveMinedBoxCoordinates", x, y);
        }

        public async Task SendMinedBoxSkin(string x, string y, string path)
        {
            await Clients.Others.SendAsync("ReceiveMinedBoxSkin", x, y, path);
        }

        public async Task SendMessage(string x)
        {
            await Clients.Others.SendAsync("ReceiveMessage", x);
        }
        public async Task UndoMessage(string x)
        {
            await Clients.Others.SendAsync("ReceiveUndoMessage", x);

        }
    }
}