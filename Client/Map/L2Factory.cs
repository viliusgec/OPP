using System;

namespace Client.Map
{
    [Serializable]
    public class L2Factory : AbstractFactory
    {
        public override Block GetStatic()
        {
            string name = "";
            string image = "";
            Effect.IEffect effect = null;
            string health = "125";
            return new L2StaticBlock(name, image, effect, health);
        }
        public override Block GetFalling()
        {
            string name = "";
            string image = "";
            Effect.IEffect effect = null;
            string health = "125";
            return new L2FallingBlock(name, image, effect, health);
        }
        public override Block GetUnbreakable()
        {
            string name = "";
            string image = "";
            Effect.IEffect effect = null;
            string health = "125";
            return new L2UnbreakableBlock(name, image, effect, health);
        }
    }
}
