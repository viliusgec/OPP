using Client.Builder;
using Client.Effect;
using Client.Map;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClientTests
{
    [TestClass]
    public class BuilderTests
    {
        [TestMethod]
        public void TestFallingBuilder()
        {
            Block block = new L1FallingBlock();
            string name = "name";
            string image = "image";
            IEffect effect = new JumpEffect();
            string health = "125";
            L1FallingBlock block2 = new(name, image, effect, health);
            block2.SetBlockType("falling");
            block2.SetImage(image);
            BlockBuilder builder = new FallingBuilder();
            block = builder.StartBuild(block).AddHealth(health).AddName(name).AddImage(image).AddBlockType("falling").AddEffect(effect).GetBuildable();
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
            L1StaticBlock block2 = new(name, image, effect, health);
            block2.SetBlockType("static");
            block2.SetImage(image);
            BlockBuilder builder = new StaticBuilder();
            block = builder.StartBuild(block).AddHealth(health).AddName(name).AddImage(image).AddBlockType("static").AddEffect(effect).GetBuildable();
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
            L1UnbreakableBlock block2 = new(name, image, effect, health);
            block2.SetBlockType("unbreakable");
            block2.SetImage(image);
            BlockBuilder builder = new UnbreakableBuilder();
            block = builder.StartBuild(block).AddHealth(health).AddName(name).AddImage(image).AddBlockType("unbreakable").AddEffect(effect).GetBuildable();
            Assert.IsTrue(block2.Equals(block));
        }
    }
}