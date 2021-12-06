using Client.Effect;
using Client.Map;

namespace Client.Builder
{
    public class UnbreakableBuilder : BlockBuilder
    {
        Block block;
        public UnbreakableBuilder()
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

        public override BlockBuilder addHealth(string health)
        {
            block.SetHealth(health);
            return this;
        }

        public override Block getBuildable()
        {
            return block;
        }
    }
}
