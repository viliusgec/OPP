using System;

namespace Client.Map
{
    [Serializable]
    public class L3Factory : AbstractFactory
    {
        public override Block GetStatic()
        {
            string name = "";
            string image = "";
            Effect.IEffect effect = null;
            string health = "125";
            return new L3StaticBlock(name, image, effect, health);
        }
        public override Block GetFalling()
        {
            string name = "";
            string image = "";
            Effect.IEffect effect = null;
            string health = "125";
            return new L3FallingBlock(name, image, effect, health);
        }
        public override Block GetUnbreakable()
        {
            string name = "";
            string image = "";
            Effect.IEffect effect = null;
            string health = "125";
            return new L3UnbreakableBlock(name, image, effect, health);
        }
    }
}
