using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Map
{
    [Serializable]
    class L3UnbreakableBlock : UnbreakableBlock
    {
        public L3UnbreakableBlock(string name, string image, Effect.IEffect effect, string health) : base(name, image, effect, health)
        {

        }
        public L3UnbreakableBlock() { }
    }
}
