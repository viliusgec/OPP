using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Map
    {
        Block[,] blocks;
        public Map(int mapX, int mapY)
        {
            blocks = new Block[mapX,mapY];
        }

        public void setBlocks(Block[,] newBlocks)
        {
            blocks = newBlocks;
        }
        public Block[,] getBlocks()
        {
            return blocks;
        }

    }
}
