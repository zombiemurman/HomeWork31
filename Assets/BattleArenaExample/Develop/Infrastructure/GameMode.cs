using System;
using System.Collections.Generic;
using UnityEngine;

public class GameMode
{
    public event Action Win;
    public event Action Defeat;

    private int _countDie;

    private LevelConfig _levelConfig;

    private Character _mainHero;

    private Vector3 _previousMainHeroPosition;

    private EnemiesSpawner _enemiesSpawner;

    private bool _isRunning;

    private List<AgentCharacter> _spawnedEnemies = new();

    private float _enemySpawnTime;

    private float _timeGame;

    private List<Func<bool>> _winConditions = new();
    private List<Func<bool>> _defeatConditions = new();

    public GameMode(LevelConfig levelConfig, Character mainHero, EnemiesSpawner enemiesSpawner)
    {
        _levelConfig = levelConfig;
        _mainHero = mainHero;
        _enemiesSpawner = enemiesSpawner;
        _enemySpawnTime = 0;
    }

    public void Start()
    {
        InitializeRules();

        _previousMainHeroPosition = _mainHero.transform.position;

        _isRunning = true;

        _timeGame = 0;
    }

    public void Update(float deltaTime)
    {
        if (_isRunning == false)
            return;

        UpdateTimeGame(deltaTime);

        SpawnEnemies(deltaTime);

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

    private void InitializeRules()
    {
        switch (_levelConfig.WinType)
        {
            case WinType.LostTime:
                _winConditions.Add(() => _timeGame > _levelConfig.TimeToWin);
                break;

            case WinType.EnemyDie:
                _winConditions.Add(() => _countDie >= _levelConfig.KillToWin);
                break;

            default:
                throw new ArgumentException("Нет условия для такого типа победы", nameof(_levelConfig.WinType));
        }

        switch (_levelConfig.DefeatType)
        {
            case DefeatType.EnemySpawnSize:
                _defeatConditions.Add(() => _spawnedEnemies.Count >= _levelConfig.SpawnEnemiesToDefeat);
                break;

            case DefeatType.HeroDie:
                _defeatConditions.Add(() => _mainHero.Health <= 0);
                break;
        }
    }

    private void UpdateTimeGame(float deltaTime)
    {
        _timeGame += deltaTime;
    }

    private void SpawnEnemies(float deltaTime)
    {
        _enemySpawnTime += deltaTime;

        if (_enemySpawnTime >= _levelConfig.EnemySpawnTime)
        {
            AgentCharacter enemy = _enemiesSpawner.Spawn(
                _levelConfig.EnemyConfig,
                _levelConfig.EnemySpawnPoints,
                _levelConfig.EnemiesSpawnRange);

            if (enemy == null)
                return;

            enemy.Destroyed += OnDestroyed;

            _spawnedEnemies.Add(enemy);

            _enemySpawnTime = 0;
        }
    }

    private void OnDestroyed(MonoDestroyable destroyable)
    {
        _countDie++;

        destroyable.Destroyed -= OnDestroyed;

        _spawnedEnemies.RemoveAll(item => item.IsDestroyed);
    }

    private bool DefeatConditionCompleted()
    {
        bool defeat = false;

        foreach (var valueDefaet in _defeatConditions)
            defeat = valueDefaet.Invoke();

        return defeat;
    }

    private bool WinConditionCompleted()
    {
        bool win = false;

        foreach (var valueWin in _winConditions)
            win = valueWin.Invoke();

        return win;
    }

    private void ProcessEndGame()
    {
        _isRunning = false;

        foreach (AgentCharacter enemy in _spawnedEnemies)
        {
            enemy.Destroyed -= OnDestroyed;
            enemy.Destroy();
        }

        _spawnedEnemies.Clear();
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
