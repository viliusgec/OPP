using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace SignalRChat.Hubs
{
    public class GameHub : Hub
    {
        public async Task JoinRoom(string room)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, room);
        }

        public async Task SendRoom(string room, string password, int players)
        {
            await Clients.Others.SendAsync("ReceiveRoom", room, password, players).ConfigureAwait(true);
        }

        public async Task SendRemoveRoom(string room)
        {
            await Clients.Others.SendAsync("ReceiveRemoveRoom", room).ConfigureAwait(true);
        }

        public Task LeaveRoom(string room)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, room);
        }
        public async Task SendState(string state, string room)
        {
            await Clients.OthersInGroup(room).SendAsync("ReceiveState", state).ConfigureAwait(true);
        }
        public async Task SendCoordinates(string x, string y, string room)
        {
            await Clients.OthersInGroup(room).SendAsync("ReceiveCoordinates", x, y).ConfigureAwait(true);
        }
        public async Task SendMap(string x,string room)
        {
            await Clients.OthersInGroup(room).SendAsync("ReceiveMap", x).ConfigureAwait(true);
        }

        public async Task SendMinedBoxCoordinates(string x, string y, string room)
        {
            await Clients.OthersInGroup(room).SendAsync("ReceiveMinedBoxCoordinates", x, y).ConfigureAwait(true);
        }

        public async Task SendMinedBoxSkin(string x, string y, string path, string room)
        {
            await Clients.OthersInGroup(room).SendAsync("ReceiveMinedBoxSkin", x, y, path).ConfigureAwait(true);
        }

        public async Task SendMessage(string x,string room)
        {
            await Clients.OthersInGroup(room).SendAsync("ReceiveMessage", x).ConfigureAwait(true);
        }
        public async Task UndoMessage(string x,string room)
        {
            await Clients.OthersInGroup(room).SendAsync("ReceiveUndoMessage", x).ConfigureAwait(true);

        }
        public async Task AddPlayer(string room)
        {
            await Clients.Others.SendAsync("ReceiveAddPlayer", room).ConfigureAwait(true);
        }
        public async Task RemovePlayer(string room)
        {
            await Clients.Others.SendAsync("ReceiveRemovePlayer", room).ConfigureAwait(true);
        }
        public async Task RequestRooms(string x)
        {
            await Clients.Others.SendAsync("ReceiveRequestRooms", x).ConfigureAwait(true);
        }
    }
}