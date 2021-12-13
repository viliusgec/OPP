namespace Client.Effect
{
    public class EffectFactory
    {
        public static IEffect Create(string effectType)
        {
            return effectType switch
            {
                "Jump" => new JumpEffect(),
                "Blind" => new BlindEffect(),
                "Speed" => new SpeedEffect(),
                _ => null,// basically no effect
            };
        }
    }
}
