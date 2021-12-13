using System;

namespace Client.Map
{
    [Serializable]
    public class StaticBlock : Block
    {
        public StaticBlock(string name, string image, Effect.IEffect effect, string health) : base(name, image, effect, health)
        {

        }
        public StaticBlock() { }
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

        public override StaticBlock Clone()
        {
            return (StaticBlock)MemberwiseClone();
        }
    }
}
