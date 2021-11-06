using Client.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
