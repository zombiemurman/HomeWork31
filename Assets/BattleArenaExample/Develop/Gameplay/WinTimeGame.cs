namespace Assets.BattleArenaExample.Develop.Gameplay
{
    public class WinTimeGame : IGameRules
    {
        private float _currentTime;

        private float _gameTime;

        public WinTimeGame(float gameTime)
        {
            _gameTime = gameTime;
        }
        public void Update(float deltaTime)
        {
            _currentTime += deltaTime;
        }

        public bool Result()
        {
            if (_currentTime >= _gameTime)
                return true;

            return false;
        }

    }
}
