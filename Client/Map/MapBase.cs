using Client.Mediator;
using System;

namespace Client.Map
{
    [Serializable]
    public class MapBase : BaseComponent
    {
        private AbstractFactory Factory { get; set; }
        private Block[,] Blocks { get; set; }
        public string[,] BlockNames { get; set; }
        public string[,] BlockImages { get; set; }
        public string[,] BlockTypes { get; set; }
        public string[,] BlockEffectTypes { get; set; }
        public string[,] BlockHealths { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public MapBase(int mapX, int mapY)
        {
            X = mapX;
            Y = mapY;
            Blocks = new Block[mapX, mapY];
        }

        public void SetBlocks(Block[,] newBlocks)
        {
            Blocks = newBlocks;
        }
        public Block[,] GetBlocks()
        {
            return Blocks;
        }

        public void SerializeBlocks()
        {
            BlockNames = new string[X, Y];
            BlockImages = new string[X, Y];
            BlockTypes = new string[X, Y];
            BlockEffectTypes = new string[X, Y];
            BlockHealths = new string[X, Y];
            for (int i = 0; i < X; i++)
            {

                for (int j = 0; j < Y; j++)
                {
                    BlockNames[i, j] = Blocks[i, j].GetName();
                    BlockImages[i, j] = Blocks[i, j].GetImage();
                    BlockTypes[i, j] = Blocks[i, j].GetBlockType();
                    BlockHealths[i, j] = Blocks[i, j].GetHealth();
                    if (Blocks[i, j].GetEffect() != null)
                    {
                        BlockEffectTypes[i, j] = Blocks[i, j].GetEffect().EffectType;
                    }
                    else
                    {
                        BlockEffectTypes[i, j] = "";
                    }
                }
            }
        }

        public void DeserializeBlocks()
        {
            Blocks = new Block[X, Y];
            for (int i = 0; i < X; i++)
            {
                for (int j = 0; j < Y; j++)
                {
                    Effect.IEffect effect = Effect.EffectFactory.Create(BlockEffectTypes[i, j]);

                    switch (BlockTypes[i, j])
                    {
                        case "static":
                            Blocks[i, j] = new L1StaticBlock(BlockNames[i, j], BlockImages[i, j], effect, BlockHealths[i, j]);
                            Blocks[i, j].SetBlockType("static");
                            break;
                        case "falling":
                            Blocks[i, j] = new L1FallingBlock(BlockNames[i, j], BlockImages[i, j], effect, BlockHealths[i, j]);
                            Blocks[i, j].SetBlockType("falling");
                            break;
                        case "unbreakable":
                            Blocks[i, j] = new L1UnbreakableBlock(BlockNames[i, j], BlockImages[i, j], effect, BlockHealths[i, j]);
                            Blocks[i, j].SetBlockType("unbreakable");
                            break;
                    }

                }
            }
        }

        public void SetFactory(int choice)
        {
            if (choice == 2)
            {
                Factory = new L2Factory();
            }
            else if (choice == 3)
            {
                Factory = new L3Factory();
            }
            else
            {
                Factory = new L1Factory();
            }

            mediator.Notify("A");
        }

        public void CreateMap()
        {
            for (int i = 0; i < X; i++)
            {
                for (int j = 0; j < Y; j++)
                {
                    Random rnd = new();
                    int a = rnd.Next(100);
                    if (i == 5)
                    {
                        Blocks[i, j] = Factory.GetUnbreakable();
                        continue;
                    }
                    if (a < 50)
                    {
                        Blocks[i, j] = Factory.GetStatic();
                        // Sitoj vietoj galima paclonint ta bloka tsg, kad parodyt veikima
                        // ir idet i random koordinates kazkokias ar kazka
                        // blocks[i,j].Clone
                    }
                    else if (a >= 50 && a < 80)
                    {
                        Blocks[i, j] = Factory.GetFalling();
                        // Sitoj vietoj galima paclonint ta bloka tsg, kad parodyt veikima
                        // ir idet i random koordinates kazkokias ar kazka
                        // blocks[i,j].Clone
                    }
                    else
                    {
                        Blocks[i, j] = Factory.GetUnbreakable();
                        // Sitoj vietoj galima paclonint ta bloka tsg, kad parodyt veikima
                        // ir idet i random koordinates kazkokias ar kazka
                        // blocks[i,j].Clone
                    }
                    int unbreakableCount = 0;
                    for (int ii = 0; ii < X; ii++)
                    {
                        if (Blocks[ii, j] != null && Blocks[ii, j].GetBlockType() == "unbreakable")
                        {
                            unbreakableCount++;
                        }
                    }
                    if (unbreakableCount > 3)
                    {
                        if (a < 50)
                        {
                            Blocks[i, j] = Factory.GetStatic();
                        }
                        else
                        {
                            Blocks[i, j] = Factory.GetFalling();
                        }
                    }
                }
            }

            mediator.Notify("B");
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
