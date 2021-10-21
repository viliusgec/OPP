using Client.Decorator;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Decorator
{
    class RedPickaxe : IPickaxe
    {
        public Image Image
        {
            //string currentDir = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            get => Client.Properties.Resources.pickaxe_red1;
        }
    }
}
