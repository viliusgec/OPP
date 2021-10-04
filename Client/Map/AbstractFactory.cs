﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Map
{
    abstract class AbstractFactory
    {
        public abstract Block GetStatic(string name);
        public abstract Block GetFalling(string name);
        public abstract Block GetUnbreakable(string name);

        /*
        public static AbstractFactory CreateBlockFactory(string FactoryType)
        {
            if (FactoryType.Equals("L1"))
            {
                return new L1Factory();
            }
            else if (FactoryType.Equals("L2"))
            {
                return new L2Factory();
            }
            else if (FactoryType.Equals("L3"))
            {
                return new L3Factory();
            }
            else
                return null;
        }
        */
    }
}