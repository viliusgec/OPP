using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.State
{
    class PauseState : State
    {
        public override void Handle1()
        {
            _context.TransitionTo(new ResumeState());
        }

        public override string Handle2()
        {
            return "Game paused";
        }

        public override void SendState(string roomName)
        {
            connection.InvokeAsync("SendState", "PauseState", roomName);
        }
    }
}
