using System;

namespace Client.Map
{
    [Serializable]
    public class L2UnbreakableBlock : UnbreakableBlock
    {
        public L2UnbreakableBlock(string name, string image, Effect.IEffect effect, string health) : base(name, image, effect, health)
        {

        }
        public L2UnbreakableBlock() { }
    }
}
