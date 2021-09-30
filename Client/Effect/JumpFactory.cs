using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Effect
{
    class JumpFactory : EffectFactory
    {
        private int _effectDuration;

        public JumpFactory(int effectDuration)
        {
            _effectDuration = effectDuration;
          
        }

        public override Effect GetEffect()
        {
            return new JumpEffect(_effectDuration);
        }
    }
}
