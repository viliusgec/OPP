using System;

namespace Client.Map
{
    [Serializable]
    [System.Xml.Serialization.XmlInclude(typeof(FallingBlock))]
    [System.Xml.Serialization.XmlInclude(typeof(StaticBlock))]
    [System.Xml.Serialization.XmlInclude(typeof(UnbreakableBlock))]
    public abstract class AbstractFactory
    {
        public abstract Block GetStatic();
        public abstract Block GetFalling();
        public abstract Block GetUnbreakable();
    }
}
