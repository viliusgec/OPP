﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Map
{
    [Serializable]
    class L3Factory : AbstractFactory
    {
        public override Block GetStatic()
        {
            string name = "";
            string image = "";
            Effect.Effect effect = null;
            return new L3StaticBlock(name, image, effect);
        }
        public override Block GetFalling()
        {
            string name = "";
            string image = "";
            Effect.Effect effect = null;
            return new L3FallingBlock(name, image, effect);
        }
        public override Block GetUnbreakable()
        {
            string name = "";
            string image = "";
            Effect.Effect effect = null;
            return new L3UnbreakableBlock(name, image, effect);
        }
    }
}
