using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Map
{
    class MapBase
    {
        private Block[,] blocks;
        public MapBase(int mapX, int mapY)
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

        public AbstractFactory GetL1Factory()
        {
            return new L1Factory();
        }
        public AbstractFactory GetL2Factory()
        {
            return new L2Factory();
        }
        public AbstractFactory GetL3Factory()
        {
            return new L3Factory();
        }
    }
}
