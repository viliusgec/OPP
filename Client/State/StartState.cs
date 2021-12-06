using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.State
{
    class StartState : State
    {
        public override void Handle1()
        {
            _context.TransitionTo(new PauseState());
        }

        public override string Handle2()
        {
            return "Game started";
        }

        public override void SendState(string roomName)
        {
            connection.InvokeAsync("SendState", "StartState", roomName);
        }
    }
}
