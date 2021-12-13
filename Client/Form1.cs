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
        private readonly Form gameForm;
        private readonly IAlgorithm strategy;
        private readonly HubConnection connection;
        private readonly RoomHub roomHub;
        private Room selectedRoom;
        private readonly AuthenticatorProxy authenticatorProxy;

        public Form1()
        {
            InitializeComponent();
            roomHub = new RoomHub("RoomHub");
            authenticatorProxy = new AuthenticatorProxy();

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
            }
            catch
            {
                label1.Text = "You can't connect second time.";
            }

            _ = connection.InvokeAsync("RequestRooms", "");

            connection.On<string>("ReceiveRequestRooms", (x) =>
            {
                if (roomHub.GetRooms().Count > 0)
                {
                    foreach (Room room in roomHub.GetRooms())
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
                    GameRoom room = new(name, password);
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
                string selected = roomListBox.SelectedItem.ToString();

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