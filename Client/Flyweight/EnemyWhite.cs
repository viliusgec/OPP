using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Client.Flyweight
{
    public class EnemyWhite : PlayerSkin
    {
        public override PictureBox ReturnPlayerSkin()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string currentDir = Directory.GetParent(workingDirectory).Parent.Parent.FullName;

            PictureBox temp = new()
            {
                Enabled = false
            };
            temp.Hide();
            temp.BackColor = System.Drawing.Color.Transparent;
            temp.SizeMode = PictureBoxSizeMode.StretchImage;
            temp.Image = Image.FromFile(currentDir + @"\Resources\EnemyWhite.png");
            return temp;
        }
    }
}
