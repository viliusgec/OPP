using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    class Player
    {
        public void Move(object sender, KeyEventArgs e, PictureBox pictureBox1)
        {
            int x = pictureBox1.Location.X;
            int y = pictureBox1.Location.Y;

            if (e.KeyCode == Keys.D) x += 10;
            else if (e.KeyCode == Keys.A) x -= 10;
            else if (e.KeyCode == Keys.W) y -= 10;
            else if (e.KeyCode == Keys.S) y += 10;

            pictureBox1.Location = new Point(x, y);
        }

        public void MoveWithCoordinates(int x, int y, PictureBox pictureBox)
        {
            pictureBox.Location = new Point(x, y);
        }
    }
}
