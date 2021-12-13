namespace Client.Decorator
{
    internal class Player : Character
    {
        private int strength;
        public Player()
        {
            strength = 5;
        }

        public override string Mine(string s)
        {
            return s + "mine";
        }
        public override void AddStr(int number)
        {
            strength = number;
        }
        public override int GetStr()
        {
            return strength;
        }
    }
}
