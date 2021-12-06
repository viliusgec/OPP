using System;

namespace Client.Map
{
    [Serializable]
#pragma warning disable CS0659 // 'L1StaticBlock' overrides Object.Equals(object o) but does not override Object.GetHashCode()
    public class L1StaticBlock : StaticBlock
#pragma warning restore CS0659 // 'L1StaticBlock' overrides Object.Equals(object o) but does not override Object.GetHashCode()
    {
        public L1StaticBlock(string name, string image, Effect.IEffect effect, string health) : base(name, image, effect, health)
        {

        }

        public L1StaticBlock() { }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is L1StaticBlock))
                return false;
            return ((this.GetName() == ((L1StaticBlock)obj).GetName()) && (this.GetImage() == ((L1StaticBlock)obj).GetImage())
                && (this.GetHealth() == ((L1StaticBlock)obj).GetHealth()) && (this.GetEffect() == ((L1StaticBlock)obj).GetEffect())
                && (this.GetBlockType() == ((L1StaticBlock)obj).GetBlockType()));
        }
    }
}
