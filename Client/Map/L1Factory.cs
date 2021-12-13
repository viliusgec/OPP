
using Client.Builder;
using System;
using System.IO;

namespace Client.Map
{
    [Serializable]
    public class L1Factory : AbstractFactory
    {
        private readonly Effect.IEffect effect;
        public override Block GetStatic()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string currentDir = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            string name = "Dirt";
            string image = currentDir + @"\Resources\dirt1.png";
            Effect.IEffect effect = GetEffect();
            string health = "125";
            L1StaticBlock block = new();
            BlockBuilder builder = new StaticBuilder();

            return builder.StartBuild(block).AddHealth(health).AddName(name).AddImage(image).AddBlockType("static").AddEffect(effect).GetBuildable();
        }
        public override Block GetFalling()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string currentDir = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            string name = "Sand";
            string image = currentDir + @"\Resources\sand1.png";
            Effect.IEffect effect = GetEffect();
            string health = "125";
            L1FallingBlock block = new();
            BlockBuilder builder = new StaticBuilder();

            return builder.StartBuild(block).AddHealth(health).AddName(name).AddImage(image).AddBlockType("falling").AddEffect(effect).AddHealth(health).GetBuildable();
        }
        public override Block GetUnbreakable()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string currentDir = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            string name = "Rock";
            string image = currentDir + @"\Resources\rock1.png";
            Effect.IEffect effect = GetEffect();
            string health = "125";
            L1UnbreakableBlock block = new();
            BlockBuilder builder = new StaticBuilder();

            return builder.StartBuild(block).AddHealth(health).AddName(name).AddImage(image).AddBlockType("unbreakable").AddEffect(effect).AddHealth(health).GetBuildable();
        }

        public Effect.IEffect GetEffect()
        {
            Random rnd = new();
            int ef = rnd.Next(5);
            if (ef == 1)
            {
                return Effect.EffectFactory.Create("Jump");
            }

            if (ef == 2)
            {
                return Effect.EffectFactory.Create("Blind");
            }

            if (ef == 3)
            {
                return Effect.EffectFactory.Create("Speed");
            }

            return null;
        }
    }
}
