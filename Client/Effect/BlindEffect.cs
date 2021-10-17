using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Effect
{
    class BlindEffect : IEffect
    {
        private string _effectType;
        public string EffectType  // read-write instance property
        {
            get => _effectType;
            set => _effectType = "blind";
        }

        private int _duration;
        public int Duration  // read-only instance property
        {
            get => _duration;
            set => _duration = value;
        }
    }
}
