
using Client.Builder;
using System;
using System.IO;

namespace Client.Map
{
    [Serializable]
    public class L1Factory : AbstractFactory
    {
#pragma warning disable CS0169 // The field 'L1Factory.effect' is never used
        Effect.IEffect effect;
#pragma warning restore CS0169 // The field 'L1Factory.effect' is never used
        public override Block GetStatic()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string currentDir = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            string name = "Dirt";
            string image = currentDir + @"\Resources\dirt1.png";
            Effect.IEffect effect = GetEffect();
            string health = "125";
            var block = new L1StaticBlock();
            BlockBuilder builder = new StaticBuilder();

            return builder.startBuild(block).addHealth(health).addName(name).addImage(image).addBlockType("static").addEffect(effect).getBuildable();
        }
        public override Block GetFalling()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string currentDir = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            string name = "Sand";
            string image = currentDir + @"\Resources\sand1.png";
            Effect.IEffect effect = GetEffect();
            string health = "125";
            var block = new L1FallingBlock();
            BlockBuilder builder = new StaticBuilder();

            return builder.startBuild(block).addHealth(health).addName(name).addImage(image).addBlockType("falling").addEffect(effect).addHealth(health).getBuildable();
        }
        public override Block GetUnbreakable()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string currentDir = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            string name = "Rock";
            string image = currentDir + @"\Resources\rock1.png";
            Effect.IEffect effect = GetEffect();
            string health = "125";
            var block = new L1UnbreakableBlock();
            BlockBuilder builder = new StaticBuilder();

            return builder.startBuild(block).addHealth(health).addName(name).addImage(image).addBlockType("unbreakable").addEffect(effect).addHealth(health).getBuildable();
        }

        public Effect.IEffect GetEffect()
        {
            Random rnd = new Random();
            int ef = rnd.Next(5);
            if (ef == 1)
                return Effect.EffectFactory.Create("Jump");
            if (ef == 2)
                return Effect.EffectFactory.Create("Blind");
            if (ef == 3)
                return Effect.EffectFactory.Create("Speed");
            return null;
        }
    }
}
