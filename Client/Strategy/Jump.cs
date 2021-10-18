using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Strategy
{
    internal class Jump : Algorithm
    {
        // not working for now
        public int x;
        public Jump(int x)
        {
            this.x = x;
        }
        public int Behave(int x)
        {
            return 0;
        }
    }
}
