using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Effect
{
    abstract class Effect
    {
        public abstract string EffectType { get; }
        public abstract int EffectDuration { get; set; }
    }
}
