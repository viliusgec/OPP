using System;

namespace Client.Map
{
    [Serializable]
    public class L3FallingBlock : FallingBlock
    {
        public L3FallingBlock(string name, string image, Effect.IEffect effect, string health) : base(name, image, effect, health)
        {

        }
        public L3FallingBlock() { }
    }
}
