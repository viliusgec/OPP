using Client.Decorator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Decorator
{
    class BlackPickaxe : IPickaxe
    {
        public string Image
        {
            //string currentDir = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            get => Client.Properties.Resources.pickaxe_black1.ToString();
        }
    }
}
