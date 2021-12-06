using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.State
{
    class ResumeState : State
    {
        public override void Handle1()
        {
            _context.TransitionTo(new StartState());
        }

        public override string Handle2()
        {
            return "Game resumed";
        }

        public override void SendState(string roomName)
        {
            connection.InvokeAsync("SendState", "ResumeState", roomName);
        }
    }
}
