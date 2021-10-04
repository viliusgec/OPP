
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Map
{
    class L1Factory : AbstractFactory
    {
        public override Block GetStatic(string name)
        {
            return new L1StaticBlock(name);
        }
        public override Block GetFalling(string name)
        {
            return new L1FallingBlock(name);
        }
        public override Block GetUnbreakable(string name)
        {
            return new L1UnbreakableBlock(name);
        }
    }
}
