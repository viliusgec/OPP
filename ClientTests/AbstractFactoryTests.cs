using Microsoft.VisualStudio.TestTools.UnitTesting;
using Client.Map;
using System.Windows.Forms;
using Client.Effect;
using System.IO;
using System;

namespace ClientTests
{
    [TestClass]
    public class AbstractFactoryTests
    {
        AbstractFactory factory;
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
    }
}