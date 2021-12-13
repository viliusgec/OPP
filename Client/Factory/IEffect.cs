namespace Client.Effect
{
    public interface IEffect
    {
        public string EffectType { get; }
        public int Duration { get; set; }
    }
}
