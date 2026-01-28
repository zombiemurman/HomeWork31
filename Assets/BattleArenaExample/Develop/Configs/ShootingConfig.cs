using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Gameplay/ShootingConfig", fileName = "ShootingConfig")]
public class ShootingConfig: ScriptableObject
{
    [field: SerializeField] public Bullet Prefab { get; private set; }
    [field: SerializeField] public float Speed { get; private set; }
    [field: SerializeField] public float LifeTime { get; private set; }
    [field: SerializeField] public float Damage { get; private set; }
}

