using System.Collections.Generic;

namespace EnemySpawn
{
    public class EnemyTypePicker : IEnemyTypePicker
    {
        private readonly Dictionary<int, int> spawnCounter;
        private readonly float ratio, interval;
        private float spawnTime;
        private int result;

        public EnemyTypePicker(float ratio, float interval)
        {
            this.spawnCounter = new Dictionary<int, int>(2) {{0, 1}, {1, 1}};
            this.interval = interval;
            this.ratio = ratio;
            this.spawnTime = interval;
        }

        public ref int GetEnemyType(in float elapsedTime)
        {
            if (elapsedTime < spawnTime)
            {
                result = -1;
                return ref result;
            }

            result = spawnCounter[0] / spawnCounter[1] > ratio ? spawnCounter[1] : spawnCounter[0];
            spawnTime += interval;

            return ref result;
        }
    }
}