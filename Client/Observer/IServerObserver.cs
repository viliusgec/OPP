using Client.PictureBoxBuilder;
using Client.Strategy;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.Observer
{
    interface IServerObserver
    {
        public void ReceiveCoordinates(PictureBox enemy, Movement movement);
        public void ReceiveMinedBoxCoordinates(MapBuilder tempMapBuilder, Map.MapBase map, FormsEditor editor);
        public Task SendMap(Map.MapBase map, string room);
        public void SendMessage(string message, string room);
        public void ReceiveMessage(TextBox textBox);
        public void UndoMessage(string room);
        public void ReceiveUndoMessage(TextBox textBox);
        public MapBuilder ReceiveMap(Map.MapBase map, PictureBox pictureBox1, PictureBox pictureBox2, Button button1, ImageList imageList1, Control.ControlCollection control, Size size);
        public void AddPlayer(string room);
        public void RemovePlayer(string room);
        public void SendRoom(string name, string password, int players);

    }
}
