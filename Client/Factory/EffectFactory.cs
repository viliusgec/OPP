namespace Client.Effect
{
    public class EffectFactory
    {
        public static IEffect Create(string effectType)
        {
            switch (effectType)
            {
                case "Jump":
                    return new JumpEffect();
                case "Blind":
                    return new BlindEffect();
                case "Speed":
                    return new SpeedEffect();
                default:
                    return null; // basically no effect

            }
        }
    }
}
