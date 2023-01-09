using System.Collections.Generic;
using static EnemySpawning.EnemySpawnSystem;

namespace EnemySpawning
{
    public class EnemyPickerByRatio : IEnemyTypePicker
    {
        private readonly Dictionary<EnemyType, int> spawnCounter;
        private readonly float ratio;
        private int result;

        public EnemyPickerByRatio(float ratio)
        {
            this.spawnCounter = new Dictionary<EnemyType, int>(2) {{EnemyType.Asteroid, 0}, {EnemyType.UFO, 1}};
            this.ratio = ratio;
        }

        public EnemyType Next()
        {
            var type = (float) this.spawnCounter[EnemyType.Asteroid] / this.spawnCounter[EnemyType.UFO] > this.ratio
                ? EnemyType.UFO
                : EnemyType.Asteroid;
            
            this.spawnCounter[type] += 1;
            
            return type;
        }
    }
}