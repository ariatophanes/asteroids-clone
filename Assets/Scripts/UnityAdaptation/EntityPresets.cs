using Presets;
using UnityEngine;

namespace UnityAdaptation
{
    [CreateAssetMenu(fileName = "EntityPresets", menuName = "Presets/EntityPresets")]
    public class EntityPresets : ScriptableObject
    {
        [field: SerializeField] public PlayerPreset Player { get; private set; }
        [field: SerializeField] public MissilePreset[] Bullets { get; private set; }
        [field: SerializeField] public EnemyPreset[] Enemies { get; private set; }
    }
}