namespace Assets.BattleArenaExample.Develop.Gameplay
{
    public class DefearSpawnEnemiesCount : IGameRules
    {
        private EnemiesSpawner _enemiesSpawner;

        private int SpawnEnemiesToDefeat;

        public DefearSpawnEnemiesCount(EnemiesSpawner enemiesSpawner, int spawnEnemiesToDefeat)
        {
            _enemiesSpawner = enemiesSpawner;
            SpawnEnemiesToDefeat = spawnEnemiesToDefeat;
        }

        public void Update(float deltaTime)
        {
        }

        public bool Result()
        {
            if (_enemiesSpawner.SpawnEnemiesCount >= SpawnEnemiesToDefeat)
                return true;

            return false;
        }
    }
}
