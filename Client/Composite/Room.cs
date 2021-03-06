using Microsoft.AspNetCore.SignalR.Client;

namespace Client.Composite
{
    public abstract class Room
    {
        private readonly string name;
        private readonly string password;
        public int players;

        public Room(string _name, string _password)
        {
            name = _name;
            password = _password;
            players = 0;
        }

        public string GetName()
        {
            return name;
        }

        public string GetPassword()
        {
            return password;
        }

        public int GetPlayers()
        {
            return players;
        }

        public void SetPlayers(int playerCount)
        {
            players = playerCount;
        }


        public abstract bool IsComposite();


        public abstract void JoinRoom(HubConnection connection);
        public abstract void LeaveRoom(HubConnection connection);

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (!(obj is Room))
            {
                return false;
            }

            return (name == ((Room)obj).GetName());
        }
    }
}
