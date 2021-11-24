using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Composite
{
    public class ChatRoom : Room
    {
        static int roomLimit = 5;
        public ChatRoom(string name, string password) : base (name, password)
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
    }
}
