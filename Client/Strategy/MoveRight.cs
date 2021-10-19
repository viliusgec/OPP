using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Strategy
{
    class MoveRight : Algorithm
    {
        public int x;
        public int y;
        public MoveRight(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public int[] Behave(int x, int y)
        {
            int[] coords = { x, y };
            if (x<504)
                coords[0] = x + 42;
            coords[1] = y;
            return coords;
        }
    }
}
