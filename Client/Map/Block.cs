using Client.Bridge;
using System;

namespace Client.Map
{
    [Serializable]
    [System.Xml.Serialization.XmlInclude(typeof(L1Factory))]
    [System.Xml.Serialization.XmlInclude(typeof(L2Factory))]
    [System.Xml.Serialization.XmlInclude(typeof(L3Factory))]
    public abstract class Block : ICloneable
    {
        private string Name { get; set; }
        private string image;
        private string blockType;
        private Effect.IEffect effect;
        public string health;
        public Block(string _name, string _image, Effect.IEffect _effect, string _health)
        {
            Name = _name;
            image = _image;
            effect = _effect;
            health = _health;
        }

        public Block()
        {

        }

        public string GetImage()
        {
            return image;
        }

        public string GetName()
        {
            return Name;
        }
        public Effect.IEffect GetEffect()
        {
            return effect;
        }

        public string GetBlockType()
        {
            return blockType;
        }

        public string GetHealth()
        {
            return health;
        }

        public void SetImage(string image)
        {
            this.image = BlockSkin.SetImage(health, Name);
        }

        public void SetName(string n)
        {
            Name = n;
        }

        public void SetEffect(Effect.IEffect ef)
        {
            effect = ef;
        }

        public void SetBlockType(string type)
        {
            blockType = type;
        }

        public abstract void SetHealth(string health);

        public abstract int GetPoints();

        public virtual bool IsBreakable()
        {
            return true;
        }


        public abstract void CreateBlock();

        //Butu galima naudot jei efektas, kad grazintu bloka i vieta
        public abstract object Clone();
    }
}
