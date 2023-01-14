using System;
using Damaging.Components;
using PlayerBehaviour.Attack;
using Simulation.Physics2D;

namespace Presets
{
    [Serializable]
    public struct MissilePreset
    {
        public Rigidbody2D Rb;
        public Radius Radius;
        public Damage Damage;
        public Damageable Damageable;
        public AttackType AttackType;
        public string ViewPath;
    }
}