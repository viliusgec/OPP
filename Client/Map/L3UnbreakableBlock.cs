using System;

namespace Client.Map
{
    [Serializable]
    public class L3UnbreakableBlock : UnbreakableBlock
    {
        public L3UnbreakableBlock(string name, string image, Effect.IEffect effect, string health) : base(name, image, effect, health)
        {

        }
        public L3UnbreakableBlock() { }
    }
}
