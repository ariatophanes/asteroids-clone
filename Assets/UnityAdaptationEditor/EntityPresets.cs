using Features.Damaging;
using Features.Damaging.Presets;
using Features.EnemySpawning.Presets;
using Features.PlayerBehaviours.Presets;
using Features.Presets;
using UnityEngine;

namespace UnityAdaptation
{
    [CreateAssetMenu(fileName = "EntityPresets", menuName = "Presets/EntityPresets")]
    public class EntityPresets : ScriptableObject, IEntityPresets
    {
        [field: SerializeField] public PlayerPreset PlayerPreset { get; private set; }
        [field: SerializeField] public MissilePresets MissilePresets { get; private set; }
        [field: SerializeField] public EnemyPresets EnemyPresets { get; private set; }
        [field: SerializeField] public AttackPresets AttackPresets { get; private set; }
    }
}