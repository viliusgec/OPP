using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Map
{
    [Serializable]
    class L2UnbreakableBlock : UnbreakableBlock
    {
        public L2UnbreakableBlock(string name, string image, Effect.IEffect effect) : base(name, image, effect)
        {

        }
        public L2UnbreakableBlock() { }
    }
}
