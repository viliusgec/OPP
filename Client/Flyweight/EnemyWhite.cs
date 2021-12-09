using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.Flyweight
{
    public class EnemyWhite : PlayerSkin
    {
        public override PictureBox ReturnPlayerSkin()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string currentDir = Directory.GetParent(workingDirectory).Parent.Parent.FullName;

            PictureBox temp = new PictureBox();
            temp.Enabled = false;
            temp.Hide();
            temp.BackColor = System.Drawing.Color.Transparent;
            temp.SizeMode = PictureBoxSizeMode.StretchImage;
            temp.Image = Image.FromFile(currentDir + @"\Resources\EnemyWhite.png");
            return temp;
        }
    }
}
