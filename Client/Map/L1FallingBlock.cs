using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Map
{
    [Serializable]
    class L1FallingBlock : FallingBlock
    {

        public L1FallingBlock(string name, string image, Effect.IEffect effect) : base(name, image, effect)
        {

        }

        public L1FallingBlock()
        {

        }
    }
}
