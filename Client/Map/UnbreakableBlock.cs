using System;

namespace Client.Map
{
    [Serializable]
    public class UnbreakableBlock : Block
    {
        public UnbreakableBlock(string name, string image, Effect.IEffect effect, string health) : base(name, image, effect, health)
        {

        }
        public UnbreakableBlock() { }
        public override void CreateBlock()
        {

        }

        public override bool IsBreakable()
        {
            return false;
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
            if (IsBreakable())
            {
                return 2;
            }
            else
            {
                return 0;
            }
        }

        public override UnbreakableBlock Clone()
        {
            return (UnbreakableBlock)MemberwiseClone();
        }
    }
}
