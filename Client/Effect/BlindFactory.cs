using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Effect
{
    class BlindFactory : EffectFactory
    {
        private int _effectDuration;

        public BlindFactory(int effectDuration)
        {
            _effectDuration = effectDuration;

        }

        public override Effect GetEffect()
        {
            return new BlindEffect(_effectDuration);
        }
    }
}
