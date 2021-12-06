using System.Windows.Forms;

namespace Client.Flyweight
{
    public abstract class PlayerSkin
    {
        public abstract PictureBox ReturnPlayerSkin();
        public abstract string ReturnEnemySkin();
    }
}
