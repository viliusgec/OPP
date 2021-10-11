using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Map
{
    [Serializable]
    [System.Xml.Serialization.XmlInclude(typeof(FallingBlock))]
    [System.Xml.Serialization.XmlInclude(typeof(StaticBlock))]
    [System.Xml.Serialization.XmlInclude(typeof(UnbreakableBlock))]
    abstract class AbstractFactory
    {
        public abstract Block GetStatic();
        public abstract Block GetFalling();
        public abstract Block GetUnbreakable();

        /*
        public static AbstractFactory CreateBlockFactory(string FactoryType)
        {
            if (FactoryType.Equals("L1"))
            {
                return new L1Factory();
            }
            else if (FactoryType.Equals("L2"))
            {
                return new L2Factory();
            }
            else if (FactoryType.Equals("L3"))
            {
                return new L3Factory();
            }
            else
                return null;
        }
        */
    }
}
