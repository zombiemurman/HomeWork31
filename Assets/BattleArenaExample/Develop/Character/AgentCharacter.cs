using UnityEngine;
using UnityEngine.AI;

public class AgentCharacter : MonoDestroyable, IMovable, ITakingDamage
{
    private NavMeshAgent _agent;

    private AgentMover _mover;

    private TransformDirectionalRotater _rotator;

    private Health _health;

    private float _damage;

    public Vector3 CurrentVelocity => _mover.CurrentVelocity;

    public Quaternion CurrentRotation => _rotator.CurrentRotation;

    public void Initialize(
        NavMeshAgent agent,
        AgentMover mover,
        TransformDirectionalRotater rotator,
        float health,
        float damage)
    {
        _agent = agent;
        _mover = mover;
        _rotator = rotator;
        _damage = damage;

        _health = new Health(health);

    }

    private void Update()
    {
        _rotator.SetInputDirection(_agent.desiredVelocity);
        _rotator.Upadate(Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        ITakingDamage objectDamage = other.GetComponent<ITakingDamage>();

        objectDamage?.TakeDamage(_damage);
    }

    public void SetDestination(Vector3 position) => _mover.SetDestination(position);

    public void StopMove() => _mover.Stop();

    public void ResumeMove() => _mover.Resume();

    public void SetRotationDirection(Vector3 inputDirection) => _rotator.SetInputDirection(inputDirection);

    public bool TryGetPath(Vector3 targetPosition, NavMeshPath pathToTarget)
        => NavMeshUtils.TryGetPah(_agent, targetPosition, pathToTarget);

    public bool IsOnNavMeshLink(out OffMeshLinkData offMeshLinkData)
    {
        if(_agent.isOnOffMeshLink)
        {
            offMeshLinkData = _agent.currentOffMeshLinkData;
            return true;
        }

        offMeshLinkData = default(OffMeshLinkData);
        return false;
    }

    public void TakeDamage(float damage)
    {
        _health.Remove(damage);

        if(_health.CurrentHealth <= 0)
            Destroy();
    }
}
