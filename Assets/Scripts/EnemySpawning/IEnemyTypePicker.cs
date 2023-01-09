using static EnemySpawning.EnemySpawnSystem;

namespace EnemySpawning
{
    public interface IEnemyTypePicker
    {
        public EnemyType Next();
    }
}