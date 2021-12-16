using Microsoft.AspNetCore.SignalR.Client;

namespace Client.Composite
{
    public class ChatRoom : Room
    {
        private static readonly int roomLimit = 5;
        public ChatRoom(string name, string password) : base(name, password)
        {

        }

        public override bool IsComposite()
        {
            return false;
        }

        public override Iterator.Iterator CreateIterator()
        {
            return null;
        }

        public override async void JoinRoom(HubConnection connection)
        {
            if (players >= roomLimit)
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

            if (!(obj is ChatRoom))
            {
                return false;
            }

            return ((GetName() == ((ChatRoom)obj).GetName()) && (GetPassword() == ((GameRoom)obj).GetPassword()));
        }
    }
}
