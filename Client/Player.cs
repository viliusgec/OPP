using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public class Player
    {   
        private Decorator.IPickaxe pickaxe { get; set; }

        public Decorator.IPickaxe GetPickaxe()
        {
            return pickaxe;
        }

        public void SetPickaxe(string entered)
        {
            //Gal cia butu geriau grazint ne stringa o image, kuri reiketu renderint?
            //Sita dalyka reikes idet ant tada kai rinksis mapa ir gales dar pickaxe pasirinkt tada
            this.pickaxe = Decorator.PickaxeDecorator.GetPickaxe(entered);
        }
    }
}
