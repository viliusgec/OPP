using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Map
{
    [Serializable]
    [System.Xml.Serialization.XmlInclude(typeof(L1Factory))]
    [System.Xml.Serialization.XmlInclude(typeof(L2Factory))]
    [System.Xml.Serialization.XmlInclude(typeof(L3Factory))]
    abstract class Block : ICloneable
    {
        private string name { get; set; }
        private string image;
        private string blockType;
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

        public string GetBlockType()
        {
            return blockType;
        }

        public void SetImage(string image)
        {
            this.image = image;
        }

        public void SetName(string n)
        {
            this.name = n;
        }

        public void SetEffect(Effect.Effect ef)
        {
            this.effect = ef;
        }

        public void SetBlockType(string type)
        {
            this.blockType = type;
        }

        public abstract void CreateBlock();

        public abstract object Clone();
    }
}
