using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Map
{
    [Serializable]
    class L2Factory : AbstractFactory
    {
        public override Block GetStatic()
        {
            string name = "";
            string image = "";
            Effect.Effect effect = null;
            return new L2StaticBlock(name,image,effect);
        }
        public override Block GetFalling()
        {
            string name = "";
            string image = "";
            Effect.Effect effect = null;
            return new L2FallingBlock(name, image, effect);
        }
        public override Block GetUnbreakable()
        {
            string name = "";
            string image = "";
            Effect.Effect effect = null;
            return new L2UnbreakableBlock(name, image, effect);
        }
    }
}
