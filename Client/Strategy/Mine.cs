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
        public int y;
        public Mine(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public int[] Behave(int x, int y)
        {
            int[] coords = { x, y };
            return coords;
        }
    }
}
 