using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemiesSpawner
{
    private EnemiesFactory _enemiesFactory;

    private LevelConfig _levelConfig;

    private float _currentEnemySpawnTime;

    private int _countKill;

    private List<AgentCharacter> _spawnedEnemies = new();

    public EnemiesSpawner(
        EnemiesFactory enemiesFactory,
        LevelConfig levelConfig)
    {
        _enemiesFactory = enemiesFactory;
        _levelConfig = levelConfig;

        _countKill = 0;
        _currentEnemySpawnTime = 0;
    }

    public int CountKill => _countKill;

    public int SpawnEnemiesCount => _spawnedEnemies.Count;

    public void Upadate(float deltaTime)
    {
        _currentEnemySpawnTime += deltaTime;

        if (_currentEnemySpawnTime >= _levelConfig.EnemySpawnTime)
        {
            AgentCharacter enemy = Spawn(
                _levelConfig.EnemyConfig,
                _levelConfig.EnemySpawnPoints,
                _levelConfig.EnemiesSpawnRange);

            if (enemy == null)
                return;

            enemy.Destroyed += OnDestroyed;

            _spawnedEnemies.Add(enemy);

            _currentEnemySpawnTime = 0;
        }
    }

    public void ClearEnemies()
    {
        foreach (AgentCharacter enemy in _spawnedEnemies)
        {
            enemy.Destroyed -= OnDestroyed;
            enemy.Destroy();
        }

        _spawnedEnemies.Clear();
    }

    private void OnDestroyed(MonoDestroyable destroyable)
    {
        _countKill++;

        destroyable.Destroyed -= OnDestroyed;

        _spawnedEnemies.RemoveAll(item => item.IsDestroyed);
    }

    private AgentCharacter Spawn(
        AgentEnemyConfig enemyConfig,
        List<Vector3> spawnPoints,
        float radius)
    {
        Vector3 positionAroundTarget;
        NavMeshHit spawnPoint;

        NavMeshQueryFilter queryFilter = new NavMeshQueryFilter();
        queryFilter.agentTypeID = 0;
        queryFilter.areaMask = 1;

        Vector2 randomPositionInCircle = Random.insideUnitCircle * radius;
        Vector3 offset = new Vector3(randomPositionInCircle.x, 0, randomPositionInCircle.y);

        positionAroundTarget = spawnPoints[Random.Range(0, spawnPoints.Count)] + offset;

        if (NavMesh.SamplePosition(positionAroundTarget, out spawnPoint, 1f, queryFilter))
            return _enemiesFactory.CreateAgentEnemy(enemyConfig, spawnPoint.position);
        else
            return null;
    }
}
