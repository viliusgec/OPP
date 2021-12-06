using Client.Effect;
using Client.Map;

namespace Client.Builder
{
    public abstract class BlockBuilder
    {
#pragma warning disable CS0169 // The field 'BlockBuilder.block' is never used
        Block block;
#pragma warning restore CS0169 // The field 'BlockBuilder.block' is never used
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
