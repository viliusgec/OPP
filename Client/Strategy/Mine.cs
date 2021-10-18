using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Strategy
{
    class Mine : Algorithm
    {
        // not working for now
        public int x;
        public Mine(int x)
        {
            this.x = x;
        }
        public int Behave(int x)
        {
            return 0;
        }
    }
}
 