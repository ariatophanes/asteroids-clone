using System;

namespace Weapons
{
    [Serializable]
    public struct Damage
    {
        public int Value;

        public Damage(int value) => this.Value = value;
    }
}