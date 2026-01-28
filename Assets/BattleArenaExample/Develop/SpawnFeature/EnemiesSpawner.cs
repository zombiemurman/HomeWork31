using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemiesSpawner
{
    private EnemiesFactory _enemiesFactory;

    public EnemiesSpawner(EnemiesFactory enemiesFactory)
    {
        _enemiesFactory = enemiesFactory;
    }

    public AgentCharacter Spawn(
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
