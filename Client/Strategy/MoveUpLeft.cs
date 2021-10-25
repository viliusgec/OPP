using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Strategy
{
    class MoveUpLeft : Algorithm
    {
        public int x;
        public int y;
        public int height;
        public int width;
        public MoveUpLeft(int x, int y, int height, int width)
        {
            this.x = x;
            this.y = y;
            this.height = height;
            this.width = width;
        }
        public int[] Behave(int x, int y, int height, int width)
        {
            int[] coords = { x, y };
            coords[0] = x - width;
            coords[1] = y - height;
            return coords;
        }
    }
}
