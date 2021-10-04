using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class L1Factory : AbstractFactory
    {
        public override Block GetBlock(string BlockType, string BlockName)
        {
            if (BlockType.Equals("Static"))
            {
                return new StaticBlock(BlockName);
            }
            else if (BlockType.Equals("Falling"))
            {
                return new FallingBlock(BlockName);
            }
            else if (BlockType.Equals("Unbreakable"))
            {
                return new UnbreakableBlock(BlockName);
            }
            else
                return null;
        }
    }
}
