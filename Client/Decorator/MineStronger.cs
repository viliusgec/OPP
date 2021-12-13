namespace Client.Decorator
{
    internal class MineStronger : MineDecorator
    {
        public MineStronger(Character wrapee) : base(wrapee)
        {

        }

        public override string Mine(string s)
        {
            return base.Mine(s + "mineStronger;");
        }
    }
}
