using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Map
{
    abstract class Block
    {
        private string name { get; set; }
        private string image;
        private Effect.Effect effect;
        public Block(string _name, string _image, Effect.Effect _effect)
        {
            name = _name;
            image = _image;
            effect = _effect;
        }

        public string GetImage()
        {
            return image;
        }

        public string GetName()
        {
            return name;
        }
        public Effect.Effect GetEffect()
        {
            return effect;
        }

        public abstract void CreateBlock();
    }
}
