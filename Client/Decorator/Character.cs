using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Decorator
{
    public abstract class Character
    {
        public abstract string Mine(string s);
        public abstract void addStr(int number);
        public abstract int getStr ();
    }
}
