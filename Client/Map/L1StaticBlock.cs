using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Map
{
    [Serializable]
    public class L1StaticBlock : StaticBlock
    {
        public L1StaticBlock(string name, string image, Effect.IEffect effect, string health) : base(name, image, effect, health)
        {

        }

        public L1StaticBlock() { }
    }
}
