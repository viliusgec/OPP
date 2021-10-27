using Client.PictureBoxBuilder;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System.Drawing;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.Observer
{
    public class ServerObserver : IServerObserver
    {
        private readonly HubConnection connection;
        readonly MapBuilder MapBuilder;
        private Map.MapBase tempMap;

        public ServerObserver()
        {
            connection = SingletonConnection.GetInstance().GetConnection();
            MapBuilder = new MapBuilder();
        }

        public void ReceiveCoordinates(PictureBox enemy)
        {
            connection.On<string, string>("ReceiveCoordinates", (x, y) =>
            {
                enemy.Location = new Point(int.Parse(x), int.Parse(y));
            });
        }

        public MapBuilder ReceiveMap(Map.MapBase map, PictureBox pictureBox1, PictureBox pictureBox2, Button button1, ImageList imageList1, Control.ControlCollection control, Size size)
        {
            connection.On<string>("ReceiveMap", (jsonString) =>
            {
                map = JsonConvert.DeserializeObject<Map.MapBase>(jsonString);
                map.DeserializeBlocks();
                tempMap = map;
                if (!MapBuilder.boxesAdded)
                {
                    MapBuilder.AddPictureBoxes(pictureBox1, pictureBox2, control, size);
                    MapBuilder.boxesAdded = true;
                }
                MapBuilder.CreateMap(imageList1, map);
                button1.Hide();
            });
            return MapBuilder;
        }

        public Map.MapBase GetMap() { return tempMap; }
        public MapBuilder GetBuilder() { return MapBuilder; }

        public async Task SendMap(Map.MapBase map)
        {
            map.SerializeBlocks();
            string jsonString = JsonConvert.SerializeObject(map);
            await connection.InvokeAsync("SendMap", jsonString);
        }
    }
}
