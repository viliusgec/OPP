using Client.Effect;
using Client.Map;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace ClientTests
{
    [TestClass]
    public class AbstractFactoryTests
    {
        private AbstractFactory factory;
        private MapBase mapBase;
        [TestMethod]
        public void TestL1FactoryGetStatic()
        {
            factory = new L1Factory();
            string workingDirectory = Environment.CurrentDirectory;
            string currentDir = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            string name = "Dirt";
            string image = currentDir + @"\Resources\dirt1.png";
            IEffect effect = new JumpEffect();
            string health = "125";
            L1StaticBlock block = new();
            block.SetName(name);
            block.SetImage(image);
            block.SetHealth(health);
            block.SetEffect(effect);
            block.SetBlockType("static");
            Block block2 = factory.GetStatic();
            block2.SetEffect(effect);
            Assert.IsTrue(block2.Equals(block));
        }

        [TestMethod]
        public void TestL1FactoryGetFalling()
        {
            factory = new L1Factory();
            string workingDirectory = Environment.CurrentDirectory;
            string currentDir = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            string name = "Sand";
            string image = currentDir + @"\Resources\sand1.png";
            IEffect effect = new JumpEffect();
            string health = "125";
            L1FallingBlock block = new();
            block.SetName(name);
            block.SetImage(image);
            block.SetHealth(health);
            block.SetEffect(effect);
            block.SetBlockType("falling");
            Block block2 = factory.GetFalling();
            block2.SetEffect(effect);
            Assert.IsTrue(block2.Equals(block));
        }

        [TestMethod]
        public void TestL1FactoryGetUngreakable()
        {
            factory = new L1Factory();
            string workingDirectory = Environment.CurrentDirectory;
            string currentDir = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            string name = "Rock";
            string image = currentDir + @"\Resources\rock1.png";
            IEffect effect = new JumpEffect();
            string health = "125";
            L1UnbreakableBlock block = new();
            block.SetName(name);
            block.SetImage(image);
            block.SetHealth(health);
            block.SetEffect(effect);
            block.SetBlockType("unbreakable");
            Block block2 = factory.GetUnbreakable();
            block2.SetEffect(effect);
            Assert.IsTrue(block2.Equals(block));
        }

        [TestMethod]
        public void TestMapBaseCreateMap()
        {
            int sizeX = 50;
            int sizeY = 50;

            mapBase = new MapBase(sizeX, sizeY);
            mapBase.SetFactory(1);

            mapBase.CreateMap();
            Block[,] blocks = mapBase.GetBlocks();
            Assert.AreEqual(sizeX, blocks.GetLength(0));
            for (int i = 0; i < blocks.Length; i++)
            {
                Assert.AreEqual(blocks.GetLength(1), sizeY);
            }
        }

        [TestMethod]
        public void TestMapBaseSerializeBlocks()
        {
            int sizeX = 50;
            int sizeY = 50;

            mapBase = new MapBase(sizeX, sizeY);
            mapBase.SetFactory(1);

            mapBase.CreateMap();

            Block[,] blocks = mapBase.GetBlocks();

            mapBase.SerializeBlocks();
            for (int i = 0; i < mapBase.X; i++)
            {
                for (int j = 0; j < mapBase.Y; j++)
                {
                    if (blocks[i, j].GetEffect() != null)
                    {
                        Assert.AreEqual(mapBase.BlockEffectTypes[i, j], blocks[i, j].GetEffect().EffectType);
                    }

                    Assert.AreEqual(mapBase.BlockHealths[i, j], blocks[i, j].GetHealth());
                    Assert.AreEqual(mapBase.BlockImages[i, j], blocks[i, j].GetImage());
                    Assert.AreEqual(mapBase.BlockNames[i, j], blocks[i, j].GetName());
                    Assert.AreEqual(mapBase.BlockTypes[i, j], blocks[i, j].GetBlockType());
                }
            }
        }

        [TestMethod]
        public void TestMapBaseDeserializeBlocks()
        {
            int sizeX = 50;
            int sizeY = 50;

            mapBase = new MapBase(sizeX, sizeY);
            mapBase.SetFactory(1);

            mapBase.CreateMap();

            Block[,] blocks = mapBase.GetBlocks();

            mapBase.SerializeBlocks();
            string[,] blockEffectTypes = mapBase.BlockEffectTypes;
            string[,] blockHealths = mapBase.BlockHealths;
            string[,] blockImages = mapBase.BlockImages;
            string[,] blockNames = mapBase.BlockNames;
            string[,] blockTypes = mapBase.BlockTypes;
            mapBase.DeserializeBlocks();
            Block[,] deserializedBlocks = mapBase.GetBlocks();
            for (int i = 0; i < mapBase.X; i++)
            {
                for (int j = 0; j < mapBase.Y; j++)
                {
                    if (blocks[i, j].GetEffect() != null)
                    {
                        Assert.AreEqual(blockEffectTypes[i, j], blocks[i, j].GetEffect().EffectType);
                    }

                    Assert.AreEqual(blockHealths[i, j], blocks[i, j].GetHealth());
                    Assert.AreEqual(blockImages[i, j], blocks[i, j].GetImage());
                    Assert.AreEqual(blockNames[i, j], blocks[i, j].GetName());
                    Assert.AreEqual(blockTypes[i, j], blocks[i, j].GetBlockType());
                }
            }
        }
    }
}