using System;

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
            {
                return false;
            }

            if (!(obj is L1UnbreakableBlock))
            {
                return false;
            }

            return ((GetName() == ((L1UnbreakableBlock)obj).GetName()) && (GetImage() == ((L1UnbreakableBlock)obj).GetImage())
                && (GetHealth() == ((L1UnbreakableBlock)obj).GetHealth()) && (GetEffect() == ((L1UnbreakableBlock)obj).GetEffect())
                && (GetBlockType() == ((L1UnbreakableBlock)obj).GetBlockType()));
        }
    }
}
