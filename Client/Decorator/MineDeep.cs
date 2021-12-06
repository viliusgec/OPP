namespace Client.Decorator
{
    class MineDeep : MineDecorator
    {
        public MineDeep(Character wrapee) : base(wrapee)
        {

        }

        public override string Mine(string s)
        {
            return base.Mine(s + "mineDeep;");
        }
    }
}
