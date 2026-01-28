using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Gameplay/AgentEnemyConfig", fileName = "AgentEnemyConfig")]
public class AgentEnemyConfig : ScriptableObject
{
    [field: SerializeField] public AgentCharacter Prefab {  get; private set; }
    [field: SerializeField] public float MoveSpeed { get; private set; } = 6;
    [field: SerializeField] public float RotationSpeed { get; private set; } = 900;
    [field: SerializeField] public float Radius { get; private set; } = 900;
    [field: SerializeField] public float TimeMove { get; private set; } = 900;
    [field: SerializeField] public float Health { get; private set; } = 10;
    [field: SerializeField] public float Damage { get; private set; } = 10;

}
