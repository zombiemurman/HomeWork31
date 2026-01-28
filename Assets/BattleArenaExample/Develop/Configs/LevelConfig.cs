using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Gameplay/LevelConfigs", fileName = "LevelConfigs")]
public class LevelConfig : ScriptableObject
{
    [field: SerializeField] public AgentEnemyConfig EnemyConfig {  get; private set; }
    [field: SerializeField] public float EnemiesSpawnRange {  get; private set; }
    [field: SerializeField] public float EnemySpawnTime {  get; private set; }
    [field: SerializeField] public Vector3 MainHeroStartPosition {  get; private set; }
    [field: SerializeField] public List<Vector3> EnemySpawnPoints {  get; private set; }
    [field: SerializeField] public WinType WinType {  get; private set; }
    [field: SerializeField] public DefeatType DefeatType {  get; private set; }
    [field: SerializeField] public float  TimeToWin {  get; private set; }
    [field: SerializeField] public int  KillToWin {  get; private set; }
    [field: SerializeField] public int  SpawnEnemiesToDefeat {  get; private set; }

    [ContextMenu("UpdateStartHeroPosition")]
    private void UpdateStartHeroPosition()
    {
        GameObject point = GameObject.FindGameObjectWithTag("StartHeroPosition");

        MainHeroStartPosition = point.transform.position;
    }

    [ContextMenu("UpdateEnemySpawnPoints")]
    private void UpdateEnemySpawnPoints()
    {
        GameObject gameObject = GameObject.FindGameObjectWithTag("EnemySpawnPoints");

        EnemySpawnPoints enemySpawnPoints = gameObject.GetComponent<EnemySpawnPoints>();

        EnemySpawnPoints = enemySpawnPoints.SpawnPoints.Select(point => point.position).ToList();
    }
}
