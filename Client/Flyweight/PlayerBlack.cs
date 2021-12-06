using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Client.Flyweight
{
    class PlayerBlack : PlayerSkin
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
            /*            temp.Size = new Size(boxWidth, boxHeight);*/
            temp.Image = Image.FromFile(currentDir + @"\Resources\PlayerBlack.png");
            return temp;
        }
        public override string ReturnEnemySkin()
        {
            return "skin";
        }
    }
}
