using System;

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
            {
                return false;
            }

            if (!(obj is L1FallingBlock))
            {
                return false;
            }

            return ((GetName() == ((L1FallingBlock)obj).GetName()) && (GetImage() == ((L1FallingBlock)obj).GetImage())
                && (GetHealth() == ((L1FallingBlock)obj).GetHealth()) && (GetEffect() == ((L1FallingBlock)obj).GetEffect())
                && (GetBlockType() == ((L1FallingBlock)obj).GetBlockType()));
        }
    }
}
