using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Map;
using Client.Effect;

namespace Client.Builder
{
    class FallingBuilder : BlockBuilder
    {
        Block block;
        public FallingBuilder()
        {

        }
        public override BlockBuilder startBuild(Block _block)
        {
            block = _block;
            return this;
        }
        public override BlockBuilder addName(string name)
        {
            block.SetName(name);
            return this;
        }

        public override BlockBuilder addImage(string image)
        {
            block.SetImage(image);
            return this;
        }

        public override BlockBuilder addBlockType(string type)
        {
            block.SetBlockType(type);
            return this;
        }

        public override BlockBuilder addEffect(IEffect effect)
        {
            block.SetEffect(effect);
            return this;
        }
        public override Block getBuildable()
        {
            return block;
        }
    }
}
