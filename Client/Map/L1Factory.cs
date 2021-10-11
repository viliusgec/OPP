
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Map
{
    [Serializable]
    class L1Factory : AbstractFactory
    {
        Effect.Effect effect;
        Effect.EffectFactory blind = new Effect.BlindFactory(3);
        Effect.EffectFactory jump = new Effect.JumpFactory(3);
        public override Block GetStatic()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string currentDir = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            string name = "Dirt";
            string image = currentDir + @"\Resources\dirt.png";
            Effect.Effect effect = GetEffect();
            var block = new L1StaticBlock(name, image, effect);
            block.SetBlockType("static");
            return block;
        }
        public override Block GetFalling()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string currentDir = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            string name = "Sand";
            string image = currentDir + @"\Resources\sand.png";
            Effect.Effect effect = GetEffect();
            var block = new L1FallingBlock(name, image, effect);
            block.SetBlockType("falling");
            return block;
        }
        public override Block GetUnbreakable()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string currentDir = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            string name = "Rock";
            string image = currentDir + @"\Resources\rock.png";
            Effect.Effect effect = GetEffect();
            var block = new L1UnbreakableBlock(name, image, effect);
            block.SetBlockType("unbreakable");
            return block;
        }

        public Effect.Effect GetEffect()
        {
            Random rnd = new Random();
            int ef = rnd.Next(5);
            if (ef == 1)
                return blind.GetEffect();
            if (ef == 2)
                return jump.GetEffect();
            return null;
        }
    }
}
