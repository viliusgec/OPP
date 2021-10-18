using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Map
{
    [Serializable]
    class UnbreakableBlock : Block
    {
        public UnbreakableBlock(string name, string image, Effect.IEffect effect) : base(name, image, effect)
        {

        }
        public override void CreateBlock()
        {

        }
        public override UnbreakableBlock Clone()
        {
            return (UnbreakableBlock)this.MemberwiseClone();
        }
    }
}
