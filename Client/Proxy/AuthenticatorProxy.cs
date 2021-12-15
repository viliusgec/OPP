using Client.Composite;
using Microsoft.AspNetCore.SignalR.Client;
using System.Windows.Forms;

namespace Client.Proxy
{
    public class AuthenticatorProxy : IAuthenticator
    {
        private Authenticator authenticator = new();

        public bool CheckAccess(Room selectedRoom,
            Client.Form1 form,
            RoomHub roomHub,
            ListBox roomListBox,
            HubConnection connection
            )
        {
            if (selectedRoom != null && selectedRoom.players < 2)
            {
                return true;
            }

            return false;
        }

        public void AuthenticatePlayerCount(Room selectedRoom,
             Client.Form1 form,
             RoomHub roomHub,
             ListBox roomListBox,
             HubConnection connection
             )
        {
            if (this.CheckAccess(selectedRoom, form, roomHub, roomListBox, connection))
            {
                authenticator.AuthenticatePlayerCount(selectedRoom, form, roomHub, roomListBox, connection);
            }
        }
    }
}
