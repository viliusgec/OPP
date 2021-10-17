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
        public MoveRight(int x)
        {
            this.x = x;
        }
        public int Behave(int x)
        {
            int coords = x;
            coords = x + 10;
            return coords;
        }
    }
}
