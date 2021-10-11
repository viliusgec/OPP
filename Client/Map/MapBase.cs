using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Map
{
    class MapBase
    {
        AbstractFactory factory;
        private Block[,] blocks;
        private int x, y;
        public MapBase(int mapX, int mapY)
        {
            x = mapX;
            y = mapY;
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

        public void setFactory(int choice)
        {
            if (choice == 2)
                factory = new L2Factory();
            else if (choice == 3)
                factory = new L3Factory();
            else
                factory = new L1Factory();
        }

        public void CreateMap()
        {
            for(int i = 0; i < x; i++)
            {
                for(int j = 0; j < y; j++)
                {
                    Random rnd = new Random();
                    int a = rnd.Next(100);
                    if(a < 50)
                    {
                        blocks[i, j] = factory.GetStatic();
                    }
                    else if(a >=50 && a < 80)
                    {
                        blocks[i, j] = factory.GetFalling();
                    }
                    else
                    {
                        blocks[i, j] = factory.GetUnbreakable();
                    }
                }
            }
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
