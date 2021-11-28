using Client.PictureBoxBuilder;
using Client.Strategy;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.Observer
{
    class ServerObserver : IServerObserver
    {
        private readonly HubConnection connection;
        readonly MapBuilder MapBuilder;
        private Map.MapBase tempMap;

        public ServerObserver()
        {
            connection = SingletonConnection.GetInstance().GetConnection();
            MapBuilder = new MapBuilder();
        }

        public void ReceiveCoordinates(PictureBox enemy, Movement movement)
        {
            connection.On<string, string>("ReceiveCoordinates", (x, y) =>
            {
                var prevLoc = enemy.Location;
                enemy.Location = new Point(int.Parse(x), int.Parse(y));
                movement.FlipImage(enemy, prevLoc, true);
            });
        }
        public void ReceiveMinedBoxCoordinates(MapBuilder tempMapBuilder, Map.MapBase map, FormsEditor editor)
        {
            connection.On<string, string>("ReceiveMinedBoxCoordinates", (x, y) =>
            {
                MapBuilder.EditMinedBox(Int32.Parse(x), Int32.Parse(y));
                
            });
        }

        public void ReceiveMinedBoxSkin()
        {
            connection.On<string, string, string>("ReceiveMinedBoxSkin", (x, y, path) =>
            {
                MapBuilder.EditMinedBoxSkin(Int32.Parse(x), Int32.Parse(y), path);
            });
        }

        public MapBuilder ReceiveMap(Map.MapBase map, PictureBox pictureBox1, PictureBox pictureBox2, Button button1, ImageList imageList1, Control.ControlCollection control, Size size)
        {
            connection.On<string>("ReceiveMap", (jsonString) =>
            {
                map = JsonConvert.DeserializeObject<Map.MapBase>(jsonString);
                map.DeserializeBlocks();
                tempMap = map;
                MapBuilder.AddPictureBoxes(pictureBox1, pictureBox2, control, size);
                MapBuilder.CreateMap(imageList1, map);
                button1.Hide();
            });
            return MapBuilder;
        }

        public void ReceiveMessage(TextBox textBox)
        {
            connection.On<string>("ReceiveMessage", (x) =>
            {
                textBox.AppendText("Enemy: " + x + "\r\n");
            });
        }

        public void ReceiveUndoMessage(TextBox textBox)
        {
            connection.On<string>("ReceiveUndoMessage", (x) =>
            {
                var index = textBox.Text.LastIndexOf("Enemy: ");
                var endIndex = textBox.Text.IndexOf("\r\n", index);
                if (index >= 0)
                    textBox.Text = textBox.Text.Remove(index, endIndex - index + 1);
            });
        }
        public Map.MapBase GetMap() { return tempMap; }
        public MapBuilder GetBuilder() { return MapBuilder; }

        public async Task SendMap(Map.MapBase map, string room)
        {
            map.SerializeBlocks();
            string jsonString = JsonConvert.SerializeObject(map);
            await connection.InvokeAsync("SendMap", jsonString, room);
        }

        public void SendMessage(string message,string room)
        {
            connection.InvokeAsync("SendMessage", message,room);
        }

        public void UndoMessage(string room)
        {
            connection.InvokeAsync("UndoMessage", "", room);
        }

        public void AddPlayer(string room)
        {
            connection.InvokeAsync("AddPlayer", room);
        }

        public void RemovePlayer(string room)
        {
            connection.InvokeAsync("RemovePlayer", room);
        }

        public void SendRoom(string name, string password)
        {
            connection.InvokeAsync("SendRoom", name, password);
        }
    }
}
