using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Decorator
{
    class Player : Character
    {
        int strength;
        public Player()
        {
            this.strength = 5;
        }

        public override string Mine(string s)
        {
            return s + "mine";
        }
        public override void addStr(int number)
        {
            strength = number;
        }
        public override int getStr()
        {
            return strength;
        }
    }
}
