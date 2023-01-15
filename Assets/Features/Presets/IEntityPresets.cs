using Features.Damaging.Presets;
using Features.EnemySpawning.Presets;
using Features.PlayerBehaviours.Presets;

namespace Features.Presets
{
    public interface IEntityPresets
    {
        PlayerPreset PlayerPreset { get; }
        MissilePresets MissilePresets { get; }
        EnemyPresets EnemyPresets { get; }
        AttackPresets AttackPresets { get; }
    }
}