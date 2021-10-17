using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Effect
{
    public static class EffectFactory
    {
        public static IEffect Create(int effectType)
        {
            switch (effectType)
            {
                case 1:
                    return new JumpEffect();
                case 2:
                    return new BlindEffect();
                case 3:
                    return new SpeedEffect();
                default:
                    return null; // basically no effect

            }
        }
    }
}
