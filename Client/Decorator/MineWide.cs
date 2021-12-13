namespace Client.Decorator
{
    internal class MineWide : MineDecorator
    {
        public MineWide(Character wrapee) : base(wrapee)
        {

        }

        public override string Mine(string s)
        {
            return base.Mine(s + "mineWide;");
        }
    }
}
