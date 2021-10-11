using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

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