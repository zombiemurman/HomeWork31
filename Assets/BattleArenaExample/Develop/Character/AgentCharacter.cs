using UnityEngine;
using UnityEngine.AI;

public class AgentCharacter : MonoDestroyable, IMovable, IJumper, ICanSpawn
{
    private NavMeshAgent _agent;

    private AgentMover _mover;

    private AgentJumper _jumper;

    private TransformDirectionalRotater _rotator;

    private Timer _spawnTimer;

    private float _timeToSpawn;

    public Vector3 CurrentVelocity => _mover.CurrentVelocity;

    public Quaternion CurrentRotation => _rotator.CurrentRotation;

    public bool InJumpProcess => _jumper.InProcess;

    public float TimeToSpawn => _timeToSpawn;

    public void Initialize(
        NavMeshAgent agent,
        AgentMover mover,
        TransformDirectionalRotater rotator,
        AgentJumper jumper,
        Timer spawnTimer,
        float timeToSpawn)
    {
        _agent = agent;
        _mover = mover;
        _rotator = rotator;
        _jumper = jumper;
        _spawnTimer = spawnTimer;
        _timeToSpawn = timeToSpawn;

        _spawnTimer.StartProcess(_timeToSpawn);

        foreach(IInitializeble initializeble in GetComponentsInChildren<IInitializeble>())
            initializeble.Initialize();
    }

    private void Update()
    {
        _rotator.SetInputDirection(_agent.desiredVelocity);
        _rotator.Upadate(Time.deltaTime);
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

    public void Jump(OffMeshLinkData offMeshLinkData) => _jumper.Jump(offMeshLinkData);

    public bool InSpawnProcess(out float elapsedTime) => _spawnTimer.InProcess(out elapsedTime);
}
