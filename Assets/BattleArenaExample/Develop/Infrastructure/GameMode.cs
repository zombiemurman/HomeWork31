using Assets.BattleArenaExample.Develop.Gameplay;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameMode
{
    public event Action Win;
    public event Action Defeat;

    private LevelConfig _levelConfig;

    private Character _mainHero;

    private EnemiesSpawner _enemiesSpawner;

    private bool _isRunning;

    private IGameRules _winConditions;
    private IGameRules _defeatConditions;

    public GameMode(LevelConfig levelConfig, Character mainHero, EnemiesSpawner enemiesSpawner)
    {
        _levelConfig = levelConfig;
        _mainHero = mainHero;
        _enemiesSpawner = enemiesSpawner;
    }

    public void Start()
    {
        GameRulesFactory gameRulesFactory = new GameRulesFactory(_levelConfig, _enemiesSpawner, _mainHero);

        _winConditions = gameRulesFactory.CreateWinCondition();
        
        _defeatConditions = gameRulesFactory .CreateDefeatCondition();

        _isRunning = true;
    }

    public void Update(float deltaTime)
    {
        if (_isRunning == false)
            return;

        _winConditions.Update(deltaTime);

        _defeatConditions.Update(deltaTime);

        _enemiesSpawner.Upadate(deltaTime);

        if (DefeatConditionCompleted())
        {
            ProcessDefeat();
            return;
        }

        if (WinConditionCompleted())
        {
            ProcessWin();
            return;
        }
    }

    private bool DefeatConditionCompleted()
    {
        return _defeatConditions.Result();
    }

    private bool WinConditionCompleted()
    {
        return _winConditions.Result();
    }

    private void ProcessEndGame()
    {
        _isRunning = false;

        _enemiesSpawner.ClearEnemies();
    }

    private void ProcessDefeat()
    {
        ProcessEndGame();
        Defeat?.Invoke();
    }

    private void ProcessWin()
    {
        ProcessEndGame();
        Win?.Invoke();
    }
}
