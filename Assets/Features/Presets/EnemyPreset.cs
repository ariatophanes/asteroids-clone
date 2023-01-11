using System;
using Damaging.Components;
using EnemySpawning;
using Simulation.Physics2D;

namespace Presets
{
    [Serializable]
    public struct EnemyPreset
    {
        public EnemyType Type;
        public Rigidbody2D Rb;
        public Radius Radius;
        public string ViewPath;
        public Damageable Damageable;
    }
}