using UnityEngine;

public class Character : MonoDestroyable, IDirectionalMovable, IDirectionalRotatable
{
    private DirectionalMover _mover;
    private DirectionalRotator _rotator;

    [SerializeField] private Transform _cameraTarget;

    public Vector3 CurrentVelocity => _mover.CurrentVelocity;

    public Quaternion CurrentRotation => _rotator.CurrentRotation;

    public Vector3 Position => transform.position;

    public Transform CameraTarget => _cameraTarget;

    public void Initialize(DirectionalMover mover, DirectionalRotator rotator)
    {
        _mover = mover;
        _rotator = rotator;

        foreach (IInitializeble initializeble in GetComponentsInChildren<IInitializeble>())
            initializeble.Initialize();
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

}
