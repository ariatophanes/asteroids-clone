using System;

namespace Features.Damaging.Components
{
    [Serializable]
    public struct Damageable
    {
        public int ReceivedDamage;
        public int TolerableDamage;
        
        public Damageable(int tolerableDamage)
        {
            this.TolerableDamage = tolerableDamage;
            this.ReceivedDamage = 0;
        }
    }
}