using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Map
{
    [Serializable]
    class L3StaticBlock : StaticBlock
    {
        public L3StaticBlock(string name, string image, Effect.IEffect effect) : base(name, image, effect)
        {

        }
        public L3StaticBlock() { }
    }
}
