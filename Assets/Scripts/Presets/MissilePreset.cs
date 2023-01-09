using System;
using PlayerBehaviour;
using Simulation.Physics2D;
using Weapons;

namespace Presets
{
    [Serializable]
    public struct MissilePreset
    {
        public Rigidbody2D Rb;
        public Radius Radius;
        public Damage Damage;
        public AttackType AttackType;
        public string ViewPath;
    }
}