using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Decorator
{
    public class MineDeep : MineDecorator
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
