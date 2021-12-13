﻿namespace Client.Effect
{
    public class JumpEffect : IEffect
    {
        private readonly string _effectType;
        public string EffectType  // read-write instance property
=> "Jump";

        private int _duration;
        public int Duration  // read-only instance property
        {
            get => _duration;
            set => _duration = value;
        }
    }
}
