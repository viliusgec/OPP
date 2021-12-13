using Client.Effect;
using Client.Map;

namespace Client.Builder
{
    public abstract class BlockBuilder
    {
        private readonly Block block;
        public BlockBuilder()
        {

        }
        public abstract BlockBuilder StartBuild(Block _block);
        public abstract BlockBuilder AddName(string name);
        public abstract BlockBuilder AddImage(string image);
        public abstract BlockBuilder AddBlockType(string type);
        public abstract BlockBuilder AddEffect(IEffect effect);
        public abstract BlockBuilder AddHealth(string health);
        public abstract Block GetBuildable();
    }
}
