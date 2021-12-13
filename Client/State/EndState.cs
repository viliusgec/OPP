using Microsoft.AspNetCore.SignalR.Client;

namespace Client.State
{
    internal class EndState : State
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
