using System;
using Core.Simulation.Common;
using Core.Simulation.Physics2D;
using Features.Damaging.Components;

namespace Features.Damaging.Presets
{
    [Serializable]
    public struct MissilePreset
    {
        public Rigidbody2D Rb;
        public Radius Radius;
        public Damage Damage;
        public Damageable Damageable;
        public string ViewPath;
    }
}