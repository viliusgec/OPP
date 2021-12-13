using Microsoft.AspNetCore.SignalR.Client;

namespace Client.State
{
    internal class PauseState : State
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
