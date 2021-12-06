using Microsoft.AspNetCore.SignalR.Client;

namespace Client.Composite
{
#pragma warning disable CS0659 // 'ChatRoom' overrides Object.Equals(object o) but does not override Object.GetHashCode()
    public class ChatRoom : Room
#pragma warning restore CS0659 // 'ChatRoom' overrides Object.Equals(object o) but does not override Object.GetHashCode()
    {
        static int roomLimit = 5;
        public ChatRoom(string name, string password) : base(name, password)
        {

        }

        public override bool IsComposite()
        {
            return false;
        }

        public override async void JoinRoom(HubConnection connection)
        {
            if (players >= roomLimit)
                await connection.InvokeAsync("JoinRoom",
                        this.GetName());
            players++;
        }

        public override async void LeaveRoom(HubConnection connection)
        {
            await connection.InvokeAsync("LeaveRoom",
                    this.GetName());
            players--;
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is ChatRoom))
                return false;
            return ((this.GetName() == ((ChatRoom)obj).GetName()) && (this.GetPassword() == ((GameRoom)obj).GetPassword()));
        }
    }
}
