using Microsoft.VisualStudio.TestTools.UnitTesting;
using Client.Builder;
using Client.Map;
using Client.Effect;

namespace ClientTests
{
    [TestClass]
    class BuilderTests
    {
        [TestMethod]
        public void TestFallingBuilder()
        {
            Block block = new L1FallingBlock();
            string name = "name";
            string image = "image";
            IEffect effect = new JumpEffect();
            string health = "125";
            var block2 = new L1FallingBlock(name,image,effect,health);
            block2.SetBlockType("falling");
            BlockBuilder builder = new FallingBuilder();
            block = builder.startBuild(block).addHealth(health).addName(name).addImage(image).addBlockType("falling").addEffect(effect).getBuildable();
            Assert.AreEqual(block2, block);
        }

        [TestMethod]
        public void TestStaticBuilder()
        {
            Block block = new L1StaticBlock();
            string name = "name";
            string image = "image";
            IEffect effect = new JumpEffect();
            string health = "125";
            var block2 = new L1StaticBlock(name, image, effect, health);
            block2.SetBlockType("static");
            BlockBuilder builder = new StaticBuilder();
            block = builder.startBuild(block).addHealth(health).addName(name).addImage(image).addBlockType("static").addEffect(effect).getBuildable();
            Assert.AreEqual(block2, block);
        }

        [TestMethod]
        public void TestUnbreakableBuilder()
        {
            Block block = new L1UnbreakableBlock();
            string name = "name";
            string image = "image";
            IEffect effect = new JumpEffect();
            string health = "125";
            var block2 = new L1UnbreakableBlock(name, image, effect, health);
            block2.SetBlockType("unbreakable");
            BlockBuilder builder = new UnbreakableBuilder();
            block = builder.startBuild(block).addHealth(health).addName(name).addImage(image).addBlockType("unbreakable").addEffect(effect).getBuildable();
            Assert.AreEqual(block2, block);
        }
    }
}
