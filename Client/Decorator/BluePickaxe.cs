﻿using Client.Decorator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Decorator
{
    class BluePickaxe : IPickaxe
    {
        public string Image
        {
            //string currentDir = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            get => Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\Resources\pickaxe-blue1.png";
        }
    }
}