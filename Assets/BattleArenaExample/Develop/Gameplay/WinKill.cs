namespace Assets.BattleArenaExample.Develop.Gameplay
{
    public class WinKill : IGameRules
    {
        private EnemiesSpawner _enemiesSpawner;

        private int KillToWin;

        public WinKill(EnemiesSpawner enemiesSpawner, int killToWin)
        {
            _enemiesSpawner = enemiesSpawner;
            KillToWin = killToWin;
        }

        public void Update(float deltaTime)
        {
        }

        public bool Result()
        {
            if(_enemiesSpawner.CountKill >= KillToWin)
                return true;

            return false;
        }
    }
}
