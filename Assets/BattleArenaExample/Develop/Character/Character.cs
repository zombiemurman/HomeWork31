using UnityEngine;

public class Character : MonoDestroyable, IDirectionalMovable, IDirectionalRotatable, IShooting, ITakingDamage
{
    private DirectionalMover _mover;
    private DirectionalRotator _rotator;

    private Health _health;

    private Shooting _shooting;

    [SerializeField] private Transform _cameraTarget;
    [SerializeField] private Transform _shootingTarget;

    public Vector3 CurrentVelocity => _mover.CurrentVelocity;

    public Quaternion CurrentRotation => _rotator.CurrentRotation;

    public Vector3 Position => transform.position;

    public Transform CameraTarget => _cameraTarget;

    public Vector3 LookDirection => transform.forward;

    public float Health => _health.CurrentHealth;


    public void Initialize(DirectionalMover mover, DirectionalRotator rotator, float health)
    {
        _mover = mover;
        _rotator = rotator;

        _health = new Health(health);

        foreach (IInitializeble initializeble in GetComponentsInChildren<IInitializeble>())
            initializeble.Initialize();
    }

    public void InitializeShooting(Shooting shooting)
    {
        _shooting = shooting;
    }

    private void Update()
    {
        _mover.Update(Time.deltaTime);
        _rotator.Upadate(Time.deltaTime);
    }

    public void SetMoveDirection(Vector3 inputDirection)
    {
        _mover.SetInputDirection(inputDirection);
    }

    public void SetRotateDirection(Vector3 inputDirection)
    {
        _rotator.SetInputDirection(inputDirection);
    }

    public void Shoot(Vector3 direction)
    {
        _shooting?.Shoot(LookDirection, _shootingTarget.position);
    }

    public void TakeDamage(float damage)
    {
        _health.Remove(damage);

        Debug.Log(_health.CurrentHealth);
    }
}
