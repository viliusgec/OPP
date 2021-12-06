using Client.Composite;
using Microsoft.AspNetCore.SignalR.Client;
using System.Linq;
using System.Windows.Forms;

namespace Client.Proxy
{
    public class Authenticator : IAuthenticator
    {
        private Form gameForm;
        public void AuthenticatePlayerCount(Room selectedRoom,
            Client.Form1 form,
            RoomHub roomHub,
            ListBox roomListBox,
            HubConnection connection
            )
        {
            if (selectedRoom != null && selectedRoom.players < 2)
            {
                connection.InvokeAsync("AddPlayer", selectedRoom.GetName());
                selectedRoom.JoinRoom(connection);
                gameForm = new Facade(selectedRoom);


                form.Hide();
                gameForm.ShowDialog();
                selectedRoom.LeaveRoom(connection);
                roomHub.GetRoom(selectedRoom.GetName()).players--;
                if (selectedRoom.players <= 0)
                {
                    roomHub.RemoveRoom(selectedRoom);
                    connection.InvokeAsync("SendRemoveRoom", selectedRoom.GetName());
                    roomListBox.Items.Clear();
                    roomListBox.Items.AddRange(roomHub.GetRooms().Select(x => x.GetName()).ToArray());
                }
                connection.InvokeAsync("RemovePlayer", selectedRoom.GetName());
                form.Show();
            }

        }
    }

}
