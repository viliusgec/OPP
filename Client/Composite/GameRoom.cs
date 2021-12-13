using Microsoft.AspNetCore.SignalR.Client;

namespace Client.Composite
{
    public class GameRoom : Room
    {
        private static readonly int roomLimit = 2;
        public GameRoom(string name, string password) : base(name, password)
        {

        }

        public override bool IsComposite()
        {
            return false;
        }

        public override async void JoinRoom(HubConnection connection)
        {
            if (players <= roomLimit)
            {
                await connection.InvokeAsync("JoinRoom",
                        GetName());
            }

            players++;
        }

        public override async void LeaveRoom(HubConnection connection)
        {
            await connection.InvokeAsync("LeaveRoom",
                    GetName());
            players--;
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (!(obj is GameRoom))
            {
                return false;
            }

            return ((GetName() == ((GameRoom)obj).GetName()) && (GetPassword() == ((GameRoom)obj).GetPassword()));
        }
    }
}
