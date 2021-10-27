using Client.PictureBoxBuilder;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.Observer
{
    interface IServerObserver
    {
        public void ReceiveCoordinates(PictureBox enemy);
        public void ReceiveMinedBoxCoordinates();
        public Task SendMap(Map.MapBase map);
        public MapBuilder ReceiveMap(Map.MapBase map, PictureBox pictureBox1, PictureBox pictureBox2, Button button1, ImageList imageList1, Control.ControlCollection control, Size size);
    }
}
