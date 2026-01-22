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

        LevelListConfig levelListConfig = Resources.Load<LevelListConfig>("Configs/LevelListConfig");

        //создание каких-то сервисов вспомогательных

        _controllersUpdateService = new ControllersUpdateService();

        ControllersFactory controllersFactory = new ControllersFactory();
        CharactersFactory charactersFactory = new CharactersFactory();

        MainHeroFactory mainHeroFactory = new MainHeroFactory(_controllersUpdateService, controllersFactory, charactersFactory);
        EnemiesFactory enemiesFactory = new EnemiesFactory(_controllersUpdateService, controllersFactory, charactersFactory);

        EnemiesSpawner enemiesSpawner = new EnemiesSpawner(enemiesFactory);

        LevelConfig levelConfig = levelListConfig.GetRandom();

        _gameplayCycle = new GameplayCycle(
            mainHeroFactory, 
            mainHeroConfig, 
            levelConfig, 
            _confirmPopup, 
            enemiesSpawner, 
            this);

        //процесс инициализации рекламных сервисов, аналитики
        //подгрузка настроек
        //загрузка или генерация уровня/окружения
        //другие подготовительные операции

        yield return new WaitForSeconds(1.5f); //симуляция

        //подготовка игры

        yield return _gameplayCycle.Prepare();

        _loadingScreen.Hide();

        //старт игры

        yield return _gameplayCycle.Launch();
    }
}
