using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Map
{
    class L2Factory : AbstractFactory
    {
        public override Block GetStatic(string name)
        {
            return new L2StaticBlock(name);
        }
        public override Block GetFalling(string name)
        {
            return new L2FallingBlock(name);
        }
        public override Block GetUnbreakable(string name)
        {
            return new L2UnbreakableBlock(name);
        }
    }
}
