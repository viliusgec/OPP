using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Decorator
{
    class Player : Character
    {
        public Player()
        {

        }

        public override string Mine(string s)
        {
            return s + "mine";
        }
    }
}
