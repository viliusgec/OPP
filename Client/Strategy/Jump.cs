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
        public int height;
        public int width;
        public Jump(int x, int y, int height, int width)
        {
            this.x = x;
            this.y = y;
            this.height = height;
            this.width = width;
        }
        public int[] Behave(int x, int y, int height, int width)
        {
            int[] coords = { x, y };
            coords[0] = x;
            coords[1] = y - width;
            return coords;
        }
    }
}
