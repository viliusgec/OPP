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
        AbstractFactory factory;
        MapBase mapBase;
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
            var block = new L1StaticBlock();
            block.SetName(name);
            block.SetImage(image);
            block.SetHealth(health);
            block.SetEffect(effect);
            block.SetBlockType("static");
            var block2 = factory.GetStatic();
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
            var block = new L1FallingBlock();
            block.SetName(name);
            block.SetImage(image);
            block.SetHealth(health);
            block.SetEffect(effect);
            block.SetBlockType("falling");
            var block2 = factory.GetFalling();
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
            var block = new L1UnbreakableBlock();
            block.SetName(name);
            block.SetImage(image);
            block.SetHealth(health);
            block.SetEffect(effect);
            block.SetBlockType("unbreakable");
            var block2 = factory.GetUnbreakable();
            block2.SetEffect(effect);
            Assert.IsTrue(block2.Equals(block));
        }

        [TestMethod]
        public void TestMapBaseCreateMap()
        {
            int sizeX = 50;
            int sizeY = 50;

            mapBase = new MapBase(sizeX, sizeY);
            mapBase.setFactory(1);

            mapBase.CreateMap();
            Block[,] blocks = mapBase.getBlocks();
            Assert.AreEqual(sizeX, blocks.GetLength(0));
            for (int i = 0; i < blocks.Length; i++)
                Assert.AreEqual(blocks.GetLength(1), sizeY);
        }

        [TestMethod]
        public void TestMapBaseSerializeBlocks()
        {
            int sizeX = 50;
            int sizeY = 50;

            mapBase = new MapBase(sizeX, sizeY);
            mapBase.setFactory(1);

            mapBase.CreateMap();

            Block[,] blocks = mapBase.getBlocks();

            mapBase.SerializeBlocks();
            for (int i = 0; i < mapBase.x; i++)
            {
                for (int j = 0; j < mapBase.y; j++)
                {
                    if (blocks[i, j].GetEffect() != null)
                        Assert.AreEqual(mapBase.blockEffectTypes[i, j], blocks[i, j].GetEffect().EffectType);
                    Assert.AreEqual(mapBase.blockHealths[i, j], blocks[i, j].GetHealth());
                    Assert.AreEqual(mapBase.blockImages[i, j], blocks[i, j].GetImage());
                    Assert.AreEqual(mapBase.blockNames[i, j], blocks[i, j].GetName());
                    Assert.AreEqual(mapBase.blockTypes[i, j], blocks[i, j].GetBlockType());
                }
            }
        }

        [TestMethod]
        public void TestMapBaseDeserializeBlocks()
        {
            int sizeX = 50;
            int sizeY = 50;

            mapBase = new MapBase(sizeX, sizeY);
            mapBase.setFactory(1);

            mapBase.CreateMap();

            Block[,] blocks = mapBase.getBlocks();

            mapBase.SerializeBlocks();
            var blockEffectTypes = mapBase.blockEffectTypes;
            var blockHealths = mapBase.blockHealths;
            var blockImages = mapBase.blockImages;
            var blockNames = mapBase.blockNames;
            var blockTypes = mapBase.blockTypes;
            mapBase.DeserializeBlocks();
            var deserializedBlocks = mapBase.getBlocks();
            for (int i = 0; i < mapBase.x; i++)
            {
                for (int j = 0; j < mapBase.y; j++)
                {
                    if (blocks[i, j].GetEffect() != null)
                        Assert.AreEqual(blockEffectTypes[i, j], blocks[i, j].GetEffect().EffectType);
                    Assert.AreEqual(blockHealths[i, j], blocks[i, j].GetHealth());
                    Assert.AreEqual(blockImages[i, j], blocks[i, j].GetImage());
                    Assert.AreEqual(blockNames[i, j], blocks[i, j].GetName());
                    Assert.AreEqual(blockTypes[i, j], blocks[i, j].GetBlockType());
                }
            }
        }
    }
}