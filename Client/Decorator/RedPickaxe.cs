using Client.Decorator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Decorator
{
    interface RedPickaxe : IPickaxe
    {
        public string Image
        {
            //string currentDir = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            get =>  @"\Resources\pickaxe-red.png";
        }
    }
}
