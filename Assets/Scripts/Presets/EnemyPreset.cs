using System;
using Simulation.Physics2D;
using static EnemySpawning.EnemySpawnSystem;

namespace Presets
{
    [Serializable]
    public struct EnemyPreset
    {
        public EnemyType Type;
        public Rigidbody2D Rb;
        public Radius Radius;
        public string ViewPath;
    }
}