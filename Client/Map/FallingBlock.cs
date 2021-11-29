using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Map
{
    [Serializable]
    public class FallingBlock : Block
    {

        public FallingBlock(string name, string image, Effect.IEffect effect, string health) : base(name, image, effect, health)
        {

        }
        
        public FallingBlock() { }
        public override void CreateBlock()
        {

        }

        public override FallingBlock Clone()
        {
            return (FallingBlock)this.MemberwiseClone();
        }
    }
}
