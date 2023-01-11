using System;
using Presets;
using UnityEngine;

namespace UnityAdaptation
{
    [CreateAssetMenu(fileName = "EntityPresets", menuName = "Presets/EntityPresets")]
    public class EntityPresets : ScriptableObject
    {
        [field: SerializeField] public PlayerPreset PlayerPreset { get; private set; }
        [field: SerializeField] public Missiles MissilePresets { get; private set; }
        [field: SerializeField] public Enemies EnemyPresets { get; private set; }

        [Serializable]
        public class Enemies
        {
            public EnemyPreset Ufo, Asteroid;
        }

        [Serializable]
        public class Missiles
        {
            public MissilePreset Gun, Laser;
        }
    }
}