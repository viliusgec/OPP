using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Map
{
    [Serializable]
    class L1UnbreakableBlock : UnbreakableBlock
    {
        public L1UnbreakableBlock(string name, string image, Effect.IEffect effect) : base(name, image, effect)
        {

        }

        public L1UnbreakableBlock() { }
    }
}
