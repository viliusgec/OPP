using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Composite
{
    public abstract class Room
    {
        string name;
        string password;
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

        public abstract bool IsComposite();

        public abstract void JoinRoom(HubConnection connection);
        public abstract void LeaveRoom(HubConnection connection);
    }
}
