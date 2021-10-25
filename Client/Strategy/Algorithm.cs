using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Strategy
{
    internal interface Algorithm
    {
        int[] Behave(int x, int y, int height, int width);
    }
}
