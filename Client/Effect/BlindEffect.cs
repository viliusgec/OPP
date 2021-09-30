using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Effect
{
    class BlindEffect : Effect
    {
        private readonly string _effectType;
        private int _effectDuration;

        public BlindEffect(int effectDuration)
        {
            _effectType = "Blind";
            _effectDuration = effectDuration;
        }

        public override string EffectType
        {
            get { return _effectType; }
        }

        public override int EffectDuration
        {
            get { return _effectDuration; }
            set { _effectDuration = value; }
        }
    }
}
