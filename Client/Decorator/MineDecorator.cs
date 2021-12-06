namespace Client.Decorator
{
    abstract class MineDecorator : Player
    {
        Character wrapee;

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
