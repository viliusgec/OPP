using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Map
{
    class L3Factory : AbstractFactory
    {
        public override Block GetStatic(string name)
        {
            return new L3StaticBlock(name);
        }
        public override Block GetFalling(string name)
        {
            return new L3FallingBlock(name);
        }
        public override Block GetUnbreakable(string name)
        {
            return new L3UnbreakableBlock(name);
        }
    }
}
