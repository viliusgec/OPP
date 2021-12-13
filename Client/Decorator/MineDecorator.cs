namespace Client.Decorator
{
    internal abstract class MineDecorator : Player
    {
        private readonly Character wrapee;

        public MineDecorator(Character _wrapee)
        {
            wrapee = _wrapee;
        }

        public override string Mine(string s)
        {
            return wrapee.Mine(s);
        }
    }
}
