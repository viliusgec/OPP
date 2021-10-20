
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Builder;

namespace Client.Map
{
    [Serializable]
    public class L1Factory : AbstractFactory
    {
        Effect.IEffect effect;
        public override Block GetStatic()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string currentDir = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            string name = "Dirt";
            string image = currentDir + @"\Resources\dirt.png";
            Effect.IEffect effect = GetEffect();
            var block = new L1StaticBlock();
            BlockBuilder builder = new StaticBuilder();
            
            return builder.startBuild(block).addName(name).addImage(image).addBlockType("static").addEffect(effect).getBuildable();
        }
        public override Block GetFalling()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string currentDir = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            string name = "Sand";
            string image = currentDir + @"\Resources\sand.png";
            Effect.IEffect effect = GetEffect();
            var block = new L1StaticBlock();
            BlockBuilder builder = new StaticBuilder();

            return builder.startBuild(block).addName(name).addImage(image).addBlockType("falling").addEffect(effect).getBuildable();
        }
        public override Block GetUnbreakable()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string currentDir = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            string name = "Rock";
            string image = currentDir + @"\Resources\rock.png";
            Effect.IEffect effect = GetEffect();
            var block = new L1StaticBlock();
            BlockBuilder builder = new StaticBuilder();

            return builder.startBuild(block).addName(name).addImage(image).addBlockType("unbreakable").addEffect(effect).getBuildable();
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
