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
        public void ReceiveMinedBoxCoordinates();
        public Task SendMap(Map.MapBase map);
        public void SendMessage(string message);
        public void ReceiveMessage(TextBox textBox);
        public void UndoMessage(string message);
        public void ReceiveUndoMessage(TextBox textBox);
        public MapBuilder ReceiveMap(Map.MapBase map, PictureBox pictureBox1, PictureBox pictureBox2, Button button1, ImageList imageList1, Control.ControlCollection control, Size size);
    }
}
