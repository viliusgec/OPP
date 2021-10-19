using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Strategy
{
    class MoveLeft : Algorithm
    {
        public int x;
        public int y;
        public MoveLeft(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public int[] Behave(int x, int y)
        {
            int[] coords = { x, y };
            if (x > 168)
                   coords[0] = x - 42;
            coords[1] = y;
            return coords;
        }
    }
}
