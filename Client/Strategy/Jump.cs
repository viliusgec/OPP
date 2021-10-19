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
        public int y;
        public Jump(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public int[] Behave(int x, int y)
        {
            int[] coords = { x, y };
            coords[0] = x;
           // if (y > 42)
                coords[1] = y - 42;
            return coords;
        }
    }
}
