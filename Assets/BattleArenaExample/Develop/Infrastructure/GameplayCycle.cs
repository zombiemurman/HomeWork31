using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayCycle : IDisposable
{
    private MainHeroFactory _mainHeroFactory;

    private MainHeroConfig _mainHeroConfig;

    private LevelConfig _levelConfig;

    private Character _mainHero;

    private ConfirmPopup _confirmPopup;

    private GameMode _gameMode;

    private EnemiesSpawner _enemySpawner;

    private MonoBehaviour _context;

    public GameplayCycle(
        MainHeroFactory mainHeroFactory, 
        MainHeroConfig mainHeroConfig, 
        LevelConfig levelConfig, 
        ConfirmPopup confirmPopup, 
        EnemiesSpawner enemySpawner, 
        MonoBehaviour context)
    {
        _mainHeroFactory = mainHeroFactory;
        _mainHeroConfig = mainHeroConfig;
        _levelConfig = levelConfig;
        _confirmPopup = confirmPopup;
        _enemySpawner = enemySpawner;
        _context = context;
    }

    public IEnumerator Prepare()
    {
        yield return null;

        _mainHero = _mainHeroFactory.Create(_mainHeroConfig, _levelConfig.MainHeroStartPosition);
    }

    public IEnumerator Launch()
    {
        _confirmPopup.Show();
        _confirmPopup.ShowMessage($"Press {KeyCode.F.ToString()} for begin");

        yield return _confirmPopup.WaitConfirm(KeyCode.F);

        _confirmPopup.Hide();

        _gameMode = new GameMode(_levelConfig, _mainHero, _enemySpawner);

        _gameMode.Win += OnGameModeWin;
        _gameMode.Defeat += OnGameModeDefeat;

        _gameMode.Start();

    }

    public void Update(float deltaTime) => _gameMode?.Update(deltaTime);

    private void OnGameModeEnded()
    {
        if (_gameMode != null)
        {
            _gameMode.Win -= OnGameModeWin;
            _gameMode.Defeat -= OnGameModeDefeat;
        }
    }

    private void OnGameModeDefeat()
    {
        OnGameModeEnded();

        Debug.Log("Defeat");

        _context.StartCoroutine(Launch());
    }

    private void OnGameModeWin()
    {
        OnGameModeEnded();

        Debug.Log("Win");

        _context.StartCoroutine(Launch());
    }

    public void Dispose()
    {
        OnGameModeEnded();
    }
}
