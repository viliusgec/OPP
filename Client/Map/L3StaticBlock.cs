using System;

namespace Client.Map
{
    [Serializable]
    public class L3StaticBlock : StaticBlock
    {
        public L3StaticBlock(string name, string image, Effect.IEffect effect, string health) : base(name, image, effect, health)
        {

        }
        public L3StaticBlock() { }
    }
}
