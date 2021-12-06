namespace Client.Effect
{
    public class JumpEffect : IEffect
    {
#pragma warning disable CS0169 // The field 'JumpEffect._effectType' is never used
        private string _effectType;
#pragma warning restore CS0169 // The field 'JumpEffect._effectType' is never used
        public string EffectType  // read-write instance property
        {
            get => "Jump";
        }

        private int _duration;
        public int Duration  // read-only instance property
        {
            get => _duration;
            set => _duration = value;
        }
    }
}
