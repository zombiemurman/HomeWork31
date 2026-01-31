using System;

namespace Assets.BattleArenaExample.Develop.Gameplay
{
    public class GameRulesFactory
    {
        private LevelConfig _levelConfig;
        
        private EnemiesSpawner _enemiesSpawner;

        private Character _hero;

        public GameRulesFactory(LevelConfig levelConfig, EnemiesSpawner enemiesSpawner, Character hero)
        {
            _levelConfig = levelConfig;
            _enemiesSpawner = enemiesSpawner;
            _hero = hero;
        }

        public IGameRules CreateWinCondition()
        {
            switch(_levelConfig.WinType)
            {
                case WinType.LostTime:
                    return new WinTimeGame(_levelConfig.TimeToWin);

                case WinType.EnemyDie:
                    return new WinKill(_enemiesSpawner, _levelConfig.KillToWin);

                default:
                    throw new ArgumentException("Не определен класс для ", nameof(_levelConfig.WinType));
            }
        }

        public IGameRules CreateDefeatCondition()
        {
            switch (_levelConfig.DefeatType)
            {
                case DefeatType.EnemySpawnSize:
                    return new DefearSpawnEnemiesCount(_enemiesSpawner, _levelConfig.SpawnEnemiesToDefeat);

                case DefeatType.HeroDie:
                    return new DefeatHeroDead(_hero);

                default:
                    throw new ArgumentException("Не определен класс для ", nameof(_levelConfig.DefeatType));
            }
        }
    }
}
