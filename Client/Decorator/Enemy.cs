using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Decorator
{
    class Enemy : Character
    {
        public Enemy()
        {

        }
        public override string Mine(string s)
        {
            return s + "mine";
        }
    }
}
