using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.State
{
    class EndState : State
    {
        public override void Handle1()
        {
            _context.TransitionTo(new StartState());
        }

        public override string Handle2()
        {
            return "Game ended";
        }

        public override void SendState(string roomName)
        {
            connection.InvokeAsync("SendState", "EndState", roomName);
        }
    }
}
