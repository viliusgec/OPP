using Microsoft.AspNetCore.SignalR.Client;

namespace Client.State
{
    internal class StartState : State
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
