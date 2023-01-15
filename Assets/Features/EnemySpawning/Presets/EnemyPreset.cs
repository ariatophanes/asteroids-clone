using System;
using Core.Simulation.Common;
using Core.Simulation.Physics2D;
using Features.Damaging.Components;
using Features.EnemySpawning.Factories;

namespace Features.EnemySpawning.Presets
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