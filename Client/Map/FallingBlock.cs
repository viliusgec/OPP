using System;

namespace Client.Map
{
    [Serializable]
    public class FallingBlock : Block
    {

        public FallingBlock(string name, string image, Effect.IEffect effect, string health) : base(name, image, effect, health)
        {

        }

        public FallingBlock() { }
        public override void CreateBlock()
        {

        }

        public sealed override void SetHealth(string health)
        {
            if (IsBreakable())
            {
                this.health = health;
            }
            else
            {
                this.health = "1000";
            }
        }

        public sealed override int GetPoints()
        {
            Random r = new();
            int money = r.Next(1, 5);
            if (IsBreakable())
            {
                return money;
            }
            else
            {
                return 0;
            }
        }

        public override FallingBlock Clone()
        {
            return (FallingBlock)MemberwiseClone();
        }
    }
}
