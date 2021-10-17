using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Map
{
    [Serializable]
    class L3FallingBlock : FallingBlock
    {
        public L3FallingBlock(string name, string image, Effect.IEffect effect) : base(name, image, effect)
        {

        }
    }
}
