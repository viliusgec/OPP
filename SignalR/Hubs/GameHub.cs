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
    }
}