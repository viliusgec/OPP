using Client.Mediator;
using System;

namespace Client.Map
{
    [Serializable]
    public class MapBase : BaseComponent
    {
        private AbstractFactory factory { get; set; }
        private Block[,] blocks { get; set; }
        public string[,] blockNames { get; set; }
        public string[,] blockImages { get; set; }
        public string[,] blockTypes { get; set; }
        public string[,] blockEffectTypes { get; set; }
        public string[,] blockHealths { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public MapBase(int mapX, int mapY)
        {
            x = mapX;
            y = mapY;
            blocks = new Block[mapX, mapY];
        }

        public void setBlocks(Block[,] newBlocks)
        {
            blocks = newBlocks;
        }
        public Block[,] getBlocks()
        {
            return blocks;
        }

        public void SerializeBlocks()
        {
            blockNames = new string[x, y];
            blockImages = new string[x, y];
            blockTypes = new string[x, y];
            blockEffectTypes = new string[x, y];
            blockHealths = new string[x, y];
            for (int i = 0; i < x; i++)
            {

                for (int j = 0; j < y; j++)
                {
                    blockNames[i, j] = blocks[i, j].GetName();
                    blockImages[i, j] = blocks[i, j].GetImage();
                    blockTypes[i, j] = blocks[i, j].GetBlockType();
                    blockHealths[i, j] = blocks[i, j].GetHealth();
                    if (blocks[i, j].GetEffect() != null)
                        blockEffectTypes[i, j] = blocks[i, j].GetEffect().EffectType;
                    else
                        blockEffectTypes[i, j] = "";
                }
            }
        }

        public void DeserializeBlocks()
        {
            blocks = new Block[x, y];
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    Effect.IEffect effect = Effect.EffectFactory.Create(blockEffectTypes[i, j]);
#pragma warning disable CS0168 // The variable 'block' is declared but never used
                    Map.Block block;
#pragma warning restore CS0168 // The variable 'block' is declared but never used

                    switch (blockTypes[i, j])
                    {
                        case "static":
                            blocks[i, j] = new L1StaticBlock(blockNames[i, j], blockImages[i, j], effect, blockHealths[i, j]);
                            blocks[i, j].SetBlockType("static");
                            break;
                        case "falling":
                            blocks[i, j] = new L1FallingBlock(blockNames[i, j], blockImages[i, j], effect, blockHealths[i, j]);
                            blocks[i, j].SetBlockType("falling");
                            break;
                        case "unbreakable":
                            blocks[i, j] = new L1UnbreakableBlock(blockNames[i, j], blockImages[i, j], effect, blockHealths[i, j]);
                            blocks[i, j].SetBlockType("unbreakable");
                            break;
                    }

                }
            }
        }

        public void setFactory(int choice)
        {
            if (choice == 2)
                factory = new L2Factory();
            else if (choice == 3)
                factory = new L3Factory();
            else
                factory = new L1Factory();

            mediator.notify("A");
        }

        public void CreateMap()
        {
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    Random rnd = new Random();
                    int a = rnd.Next(100);
                    if (i == 5)
                    {
                        blocks[i, j] = factory.GetUnbreakable();
                        continue;
                    }
                    if (a < 50)
                    {
                        blocks[i, j] = factory.GetStatic();
                        // Sitoj vietoj galima paclonint ta bloka tsg, kad parodyt veikima
                        // ir idet i random koordinates kazkokias ar kazka
                        // blocks[i,j].Clone
                    }
                    else if (a >= 50 && a < 80)
                    {
                        blocks[i, j] = factory.GetFalling();
                        // Sitoj vietoj galima paclonint ta bloka tsg, kad parodyt veikima
                        // ir idet i random koordinates kazkokias ar kazka
                        // blocks[i,j].Clone
                    }
                    else
                    {
                        blocks[i, j] = factory.GetUnbreakable();
                        // Sitoj vietoj galima paclonint ta bloka tsg, kad parodyt veikima
                        // ir idet i random koordinates kazkokias ar kazka
                        // blocks[i,j].Clone
                    }
                    var unbreakableCount = 0;
                    for (int ii = 0; ii < x; ii++)
                    {
                        if (blocks[ii, j] != null && blocks[ii, j].GetBlockType() == "unbreakable")
                        {
                            unbreakableCount++;
                        }
                    }
                    if (unbreakableCount > 3)
                    {
                        if (a < 50)
                            blocks[i, j] = factory.GetStatic();
                        else
                            blocks[i, j] = factory.GetFalling();
                    }
                }
            }

            mediator.notify("B");
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
