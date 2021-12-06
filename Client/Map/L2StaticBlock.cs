using System;

namespace Client.Map
{
    [Serializable]
    public class L2StaticBlock : StaticBlock
    {
        public L2StaticBlock(string name, string image, Effect.IEffect effect, string health) : base(name, image, effect, health)
        {

        }

        public L2StaticBlock() { }
    }
}
