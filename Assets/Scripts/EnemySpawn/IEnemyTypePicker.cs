namespace EnemySpawn
{
    public interface IEnemyTypePicker
    {
        public ref int GetEnemyType(in float elapsedTime);
    }
}