using Microsoft.VisualStudio.TestTools.UnitTesting;
using Client.Builder;
using Client.Map;
using Client.Effect;

namespace ClientTests
{
    [TestClass]
    public class BuilderTests2
    {
        [TestMethod]
        public void TestFallingBuilder()
        {
            Block block = new L1FallingBlock();
            string name = "name";
            string image = "image";
            IEffect effect = new JumpEffect();
            string health = "125";
            var block2 = new L1FallingBlock(name, image, effect, health);
            block2.SetBlockType("falling");
            block2.SetImage(image);
            BlockBuilder builder = new FallingBuilder();
            block = builder.startBuild(block).addHealth(health).addName(name).addImage(image).addBlockType("falling").addEffect(effect).getBuildable();
            Assert.IsTrue(block2.Equals(block));
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
            block2.SetImage(image);
            BlockBuilder builder = new StaticBuilder();
            block = builder.startBuild(block).addHealth(health).addName(name).addImage(image).addBlockType("static").addEffect(effect).getBuildable();
            Assert.IsTrue(block2.Equals(block));
        }


        [TestMethod]
        public void TestUnbreakable()
        {
            Block block = new L1UnbreakableBlock();
            string name = "name";
            string image = "image";
            IEffect effect = new JumpEffect();
            string health = "125";
            var block2 = new L1UnbreakableBlock(name, image, effect, health);
            block2.SetBlockType("unbreakable");
            block2.SetImage(image);
            BlockBuilder builder = new UnbreakableBuilder();
            block = builder.startBuild(block).addHealth(health).addName(name).addImage(image).addBlockType("unbreakable").addEffect(effect).getBuildable();
            Assert.IsTrue(block2.Equals(block));
        }
    }
}