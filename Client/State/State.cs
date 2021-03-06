using Microsoft.AspNetCore.SignalR.Client;

namespace Client.State
{
    internal abstract class State
    {
        protected StateContext _context;
        protected readonly HubConnection connection = SingletonConnection.GetInstance().GetConnection();

        public void SetContext(StateContext context)
        {
            _context = context;
        }

        public abstract void Handle1();

        public abstract string Handle2();

        public abstract void SendState(string roomName);
    }
}
