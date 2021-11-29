using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Map
{
    [Serializable]
    public class L1FallingBlock : FallingBlock
    {

        public L1FallingBlock(string name, string image, Effect.IEffect effect, string health) : base(name, image, effect, health)
        {

        }

        public L1FallingBlock()
        {

        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is L1FallingBlock))
                return false;
            return ((this.GetName() == ((L1FallingBlock)obj).GetName()) && (this.GetImage() == ((L1FallingBlock)obj).GetImage())
                && (this.GetHealth() == ((L1FallingBlock)obj).GetHealth()) && (this.GetEffect() == ((L1FallingBlock)obj).GetEffect())
                && (this.GetBlockType() == ((L1FallingBlock)obj).GetBlockType()));
        }
    }
}
