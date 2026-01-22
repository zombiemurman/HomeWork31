using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Gameplay/LevelConfigs", fileName = "LevelConfigs")]
public class LevelConfig : ScriptableObject
{
    [field: SerializeField] public float DistanceTraveledToWin {  get; private set; }
    [field: SerializeField] public float TimeToDefeat {  get; private set; }
    [field: SerializeField] public AgentEnemyConfig EnemyConfig {  get; private set; }
    [field: SerializeField] public int EnemiesCount {  get; private set; }
    [field: SerializeField] public float EnemiesSpawnRange {  get; private set; }
    [field: SerializeField] public Vector3 MainHeroStartPosition {  get; private set; }
    [field: SerializeField] public string EnviromentSceneName {  get; private set; }

    [ContextMenu("UpdateStartHeroPosition")]
    private void UpdateStartHeroPosition()
    {
        GameObject point = GameObject.FindGameObjectWithTag("StartHeroPosition");

        MainHeroStartPosition = point.transform.position;
    }
}
