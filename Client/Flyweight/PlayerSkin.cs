using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.Flyweight
{
    public abstract class PlayerSkin
    {
        public abstract PictureBox ReturnPlayerSkin();
        public abstract string ReturnEnemySkin();
    }
}
