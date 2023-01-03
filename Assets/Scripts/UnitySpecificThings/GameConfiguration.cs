using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/GameConfig")]
public class GameConfiguration : ScriptableObject
{
    [field: SerializeField] public float EnemiesSpawnRatio { get; private set; }
    [field: SerializeField] public float EnemiesSpawnInterval { get; private set; }
}