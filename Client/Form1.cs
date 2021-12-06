using Client.Composite;
using Client.Proxy;
using Client.Strategy;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Client
{
    public partial class Form1 : Form
    {
#pragma warning disable CS0169 // The field 'Form1.gameForm' is never used
        Form gameForm;
#pragma warning restore CS0169 // The field 'Form1.gameForm' is never used
#pragma warning disable CS0169 // The field 'Form1.strategy' is never used
        Algorithm strategy;
#pragma warning restore CS0169 // The field 'Form1.strategy' is never used
        private HubConnection connection;
        private RoomHub roomHub;
        private Room selectedRoom;
        private AuthenticatorProxy authenticatorProxy;

        public Form1()
        {
            InitializeComponent();
            roomHub = new RoomHub("RoomHub");
            authenticatorProxy = new AuthenticatorProxy();

            //cia sitie jei chato kambarius darysim
            //roomHub.AddRoom(new RoomHub("Chat Rooms"));
            //roomHub.AddRoom(new RoomHub("Game Rooms"));
            SingletonConnection temp_connection = SingletonConnection.GetInstance();
            connection = temp_connection.GetConnection();
            roomListBox.Hide();
            roomNameBox.Hide();
            roomPassBox.Hide();
            joinRoomButton.Hide();
            createRoomButton.Hide();
            roomNameLabel.Hide();
            roomPasswordLabel.Hide();
            isChatCheckBox.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        //connection button
        private async void button1_Click(object sender, EventArgs e)
        {
            roomListBox.Show();
            roomNameBox.Show();
            roomPassBox.Show();
            joinRoomButton.Show();
            createRoomButton.Show();
            roomNameLabel.Show();
            roomPasswordLabel.Show();
            isChatCheckBox.Show();
            try
            {
                await connection.StartAsync();

                label1.Text = "Connection started";
                //this.Hide();
                //gameForm.ShowDialog();
                //this.Show();
            }
            catch
            {
                label1.Text = "You can't connect second time.";//ex.ToString();
            }

            _ = connection.InvokeAsync("RequestRooms", "");

            connection.On<string>("ReceiveRequestRooms", (x) =>
            {
                if (roomHub.GetRooms().Count > 0)
                {
                    foreach (var room in roomHub.GetRooms())
                    {
                        connection.InvokeAsync("SendRoom", room.GetName(), room.GetPassword(), room.GetPlayers());
                    }
                }
            });
            connection.On<string>("ReceiveAddPlayer", (x) =>
            {
                roomHub.GetRoom(x).players++;
            });
            connection.On<string>("ReceiveRemovePlayer", (x) =>
            {
                roomHub.GetRoom(x).players--;
                if (roomHub.GetRoom(x).players <= 0)
                {
                    roomHub.RemoveRoom(roomHub.GetRoom(x));
                    roomListBox.Items.Clear();
                    roomListBox.Items.AddRange(roomHub.GetRooms().Select(x => x.GetName()).ToArray());
                }

            });
            connection.On<string, string, int>("ReceiveRoom", (name, password, players) =>
            {
                if (roomHub.GetRoom(name) == null)
                {
                    var room = new GameRoom(name, password);
                    room.SetPlayers(players);

                    roomHub.AddRoom(room);

                    roomListBox.Items.Clear();
                    roomListBox.Items.AddRange(roomHub.GetRooms().Select(x => x.GetName()).ToArray());
                }
            });
            connection.On<string>("ReceiveRemoveRoom", (name) =>
            {
                if (roomHub.GetRoom(name) == null)
                {
                    roomHub.RemoveRoom(roomHub.GetRoom(name));
                    roomListBox.Items.Clear();
                    roomListBox.Items.AddRange(roomHub.GetRooms().Select(x => x.GetName()).ToArray());
                }

            });

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (roomHub.GetRoom(roomNameBox.Text) == null)
            {
                Room room;
                if (isChatCheckBox.Checked)
                {
                    room = new ChatRoom(roomNameBox.Text, roomPassBox.Text);
                    //Ateity implementuot chato atskira kambari, maybe, kaip chebra pasakys.
                    //gameForm = new ChatForm(room);
                }
                room = new GameRoom(roomNameBox.Text, roomPassBox.Text);
                roomHub.AddRoom(room);
                connection.InvokeAsync("SendRoom", room.GetName(), room.GetPassword(), room.GetPlayers());
                /*
                room.JoinRoom(connection);
                gameForm = new Facade(room);


                this.Hide();
                gameForm.ShowDialog();
                room.LeaveRoom(connection);
                if (room.players <= 0)
                    roomHub.RemoveRoom(room);
                this.Show();
                */
                roomListBox.Items.Clear();
                roomListBox.Items.AddRange(roomHub.GetRooms().Select(x => x.GetName()).ToArray());
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (roomListBox.SelectedItem != null)
            {
                var selected = roomListBox.SelectedItem.ToString();

                selectedRoom = roomHub.GetRoom(selected);
            }

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void joinRoomButton_Click(object sender, EventArgs e)
        {
            authenticatorProxy.AuthenticatePlayerCount(selectedRoom, this, roomHub, roomListBox, connection);
        }
    }
}