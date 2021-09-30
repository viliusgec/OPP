using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Effect
{
    abstract class EffectFactory
    {
        public abstract Effect GetEffect();
    }
}
