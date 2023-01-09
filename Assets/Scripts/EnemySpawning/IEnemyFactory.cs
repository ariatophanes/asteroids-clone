namespace EnemySpawning
{
    public interface IEnemyFactory
    {
        public int Create(EnemySpawnSystem.EnemyType type);
    }
}