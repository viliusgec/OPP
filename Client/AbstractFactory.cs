using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    abstract class AbstractFactory
    {
        public abstract Block GetBlock(string BlockType, string BlockName);

        public static AbstractFactory CreateBlockFactory(string FactoryType)
        {
            if (FactoryType.Equals("Block"))
            {
                return new BlockFactory();
            }
            return null;
        }
    }
}
