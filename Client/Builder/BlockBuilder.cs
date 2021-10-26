using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Map;
using Client.Effect;

namespace Client.Builder
{
    abstract class BlockBuilder
    {
        Block block;
        public BlockBuilder()
        {
            
        }
        public abstract BlockBuilder startBuild(Block _block);
        public abstract BlockBuilder addName(string name);
        public abstract BlockBuilder addImage(string image);
        public abstract BlockBuilder addBlockType(string type);
        public abstract BlockBuilder addEffect(IEffect effect);
        public abstract BlockBuilder addHealth(string health);
        public abstract Block getBuildable();
    }
}
