using Client.Effect;
using Client.Map;

namespace Client.Builder
{
    public class UnbreakableBuilder : BlockBuilder
    {
        private Block block;
        public UnbreakableBuilder()
        {

        }
        public override BlockBuilder StartBuild(Block _block)
        {
            block = _block;
            return this;
        }
        public override BlockBuilder AddName(string name)
        {
            block.SetName(name);
            return this;
        }

        public override BlockBuilder AddImage(string image)
        {
            block.SetImage(image);
            return this;
        }

        public override BlockBuilder AddBlockType(string type)
        {
            block.SetBlockType(type);
            return this;
        }

        public override BlockBuilder AddEffect(IEffect effect)
        {
            block.SetEffect(effect);
            return this;
        }

        public override BlockBuilder AddHealth(string health)
        {
            block.SetHealth(health);
            return this;
        }

        public override Block GetBuildable()
        {
            return block;
        }
    }
}
