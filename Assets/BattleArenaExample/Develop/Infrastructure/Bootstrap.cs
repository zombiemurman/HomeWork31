using Cinemachine;
using System.Collections;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private LoadingScreen _loadingScreen;
    [SerializeField] private ConfirmPopup _confirmPopup;

    private ControllersUpdateService _controllersUpdateService;
    
    private GameplayCycle _gameplayCycle;

    private void Awake()
    {
        StartCoroutine(StartProcess());
    }

    private void Update()
    {
        _controllersUpdateService?.Update(Time.deltaTime);
        _gameplayCycle?.Update(Time.deltaTime);
    }

    private void OnDestroy()
    {
        _gameplayCycle?.Dispose();
    }

    private IEnumerator StartProcess()
    {
        _loadingScreen.Show();
        _loadingScreen.ShowMessage("Loading...");

        MainHeroConfig mainHeroConfig = Resources.Load<MainHeroConfig>("Configs/MainHeroConfig");

        _controllersUpdateService = new ControllersUpdateService();

        ControllersFactory controllersFactory = new ControllersFactory();
        CharactersFactory charactersFactory = new CharactersFactory();

        MainHeroFactory mainHeroFactory = new MainHeroFactory(_controllersUpdateService, controllersFactory, charactersFactory);
        EnemiesFactory enemiesFactory = new EnemiesFactory(_controllersUpdateService, controllersFactory, charactersFactory);

        EnemiesSpawner enemiesSpawner = new EnemiesSpawner(enemiesFactory);

        LevelConfig levelConfig = Resources.Load<LevelConfig>("Configs/LevelConfigs");

        _gameplayCycle = new GameplayCycle(
            mainHeroFactory, 
            mainHeroConfig, 
            levelConfig, 
            _confirmPopup, 
            enemiesSpawner, 
            this);

        yield return new WaitForSeconds(1.5f);

        yield return _gameplayCycle.Prepare();

        _loadingScreen.Hide();

        yield return _gameplayCycle.Launch();
    }
}
