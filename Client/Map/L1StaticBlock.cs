using System;

namespace Client.Map
{
    [Serializable]
    public class L1StaticBlock : StaticBlock
    {
        public L1StaticBlock(string name, string image, Effect.IEffect effect, string health) : base(name, image, effect, health)
        {

        }

        public L1StaticBlock() { }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (!(obj is L1StaticBlock))
            {
                return false;
            }

            return ((GetName() == ((L1StaticBlock)obj).GetName()) && (GetImage() == ((L1StaticBlock)obj).GetImage())
                && (GetHealth() == ((L1StaticBlock)obj).GetHealth()) && (GetEffect() == ((L1StaticBlock)obj).GetEffect())
                && (GetBlockType() == ((L1StaticBlock)obj).GetBlockType()));
        }
    }
}
