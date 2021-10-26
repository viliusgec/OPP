using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Map
{
    [Serializable]
    class StaticBlock : Block
    {
        public StaticBlock(string name, string image, Effect.IEffect effect, string health) : base(name, image, effect, health)
        {

        }
        public StaticBlock() { }
        public override void CreateBlock()
        {

        }
        public override StaticBlock Clone()
        {
            return (StaticBlock)this.MemberwiseClone();
        }
    }
}
