using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Effect
{
    interface IEffect
    {
        public string EffectType { get; set; }
        public int Duration { get; set; }
    }
}
