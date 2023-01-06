using EnemySpawn;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/GameConfig")]
public class GameConfiguration : ScriptableObject
{
    [field: SerializeField] public EnemySpawningModel EnemySpawningModel { get; private set; }
}