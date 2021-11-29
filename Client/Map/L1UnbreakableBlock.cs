using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Map
{
    [Serializable]
    public class L1UnbreakableBlock : UnbreakableBlock
    {
        public L1UnbreakableBlock(string name, string image, Effect.IEffect effect, string health) : base(name, image, effect, health)
        {

        }

        public L1UnbreakableBlock() { }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is L1UnbreakableBlock))
                return false;
            return ((this.GetName() == ((L1UnbreakableBlock)obj).GetName()) && (this.GetImage() == ((L1UnbreakableBlock)obj).GetImage())
                && (this.GetHealth() == ((L1UnbreakableBlock)obj).GetHealth()) && (this.GetEffect() == ((L1UnbreakableBlock)obj).GetEffect())
                && (this.GetBlockType() == ((L1UnbreakableBlock)obj).GetBlockType()));
        }
    }
}
