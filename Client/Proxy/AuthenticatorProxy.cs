using Client.Composite;
using Microsoft.AspNetCore.SignalR.Client;
using System.Windows.Forms;

namespace Client.Proxy
{
    public class AuthenticatorProxy : IAuthenticator
    {
        public void AuthenticatePlayerCount(Room selectedRoom,
             Client.Form1 form,
             RoomHub roomHub,
             ListBox roomListBox,
             HubConnection connection
             )
        {
            Authenticator authenticator = new();

            authenticator.AuthenticatePlayerCount(selectedRoom, form, roomHub, roomListBox, connection);
        }
    }
}
