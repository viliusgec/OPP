using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Map
{
    [Serializable]
    class L2FallingBlock : FallingBlock
    {
        public L2FallingBlock(string name, string image, Effect.Effect effect) : base(name, image, effect)
        {

        }
    }
}
