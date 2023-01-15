using System;

namespace Features.Damaging.Components
{
    [Serializable]
    public struct Damage
    {
        public int Value;

        public Damage(int value) => this.Value = value;
    }
}