using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Effect
{
    public interface IEffect
    {
        public string EffectType { get; }
        public int Duration { get; set; }
    }
}
