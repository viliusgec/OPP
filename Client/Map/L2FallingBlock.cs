using System;

namespace Client.Map
{
    [Serializable]
    public class L2FallingBlock : FallingBlock
    {
        public L2FallingBlock(string name, string image, Effect.IEffect effect, string health) : base(name, image, effect, health)
        {

        }
        public L2FallingBlock() { }
    }
}
