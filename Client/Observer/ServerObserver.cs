﻿using Client.PictureBoxBuilder;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System.Drawing;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.Observer
{
    public class ServerObserver
    {
        private readonly HubConnection connection;
        readonly MapBuilder MapBuilder;

        public ServerObserver()
        {
            connection = SingletonConnection.GetInstance().GetConnection();
            MapBuilder = new MapBuilder();
        }

        public void ReceiveCoordinates(PictureBox pictureBox)
        {
            connection.On<string, string>("ReceiveCoordinates", (x, y) =>
            {
                pictureBox.Location = new Point(int.Parse(x), int.Parse(y));
            });
        }

        public void ReceiveMap(Map.MapBase map, PictureBox pictureBox1, PictureBox pictureBox2, Button button1, ImageList imageList1, Control.ControlCollection control, Size size)
        {
            connection.On<string>("ReceiveMap", (jsonString) =>
            {
                map = JsonConvert.DeserializeObject<Map.MapBase>(jsonString);
                map.DeserializeBlocks();
                if (!MapBuilder.boxesAdded)
                {
                    MapBuilder.AddPictureBoxes(pictureBox1, pictureBox2, control, size);
                    MapBuilder.boxesAdded = true;
                }
                MapBuilder.CreateMap(imageList1, map);
                button1.Hide();
            });
        }

        public async Task SendMap(Map.MapBase map)
        {
            map.SerializeBlocks();
            string jsonString = JsonConvert.SerializeObject(map);
            await connection.InvokeAsync("SendMap", jsonString);
        }
    }
}