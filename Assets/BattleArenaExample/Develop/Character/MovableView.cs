using UnityEngine;

public class MovableView : MonoBehaviour, IInitializeble
{
    private readonly int IsRunningKey = Animator.StringToHash("isRunning");

    [SerializeField] private Animator _animator;

    private IMovable _movable;

    private bool _isInit;

    public void Initialize()
    {
        _movable = GetComponentInParent<IMovable>();

        _isInit = true;
    }

    private void Update()
    {
        if (_isInit == false)
            return;

        if (_movable.CurrentVelocity.magnitude > 0.05f)
            StartRunning();
        else
            StopRunning();
    }

    private void StartRunning()
    {
        _animator.SetBool(IsRunningKey, true);
    }

    private void StopRunning()
    {
        _animator.SetBool(IsRunningKey, false);
    }
}
