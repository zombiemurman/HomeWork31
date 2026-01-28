using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.PhysicsVisualizationSettings;
public class AgentCharacterRandomMoveController : Controller
{
    private AgentCharacter _character;

    private float _timeMove;

    private float _currentTimeMove;

    private float _radius;

    private NavMeshPath _pathToTarget = new NavMeshPath();

    private Vector3 _direction;

    public AgentCharacterRandomMoveController(AgentCharacter character, float timeMove, float radius)
    {
        _character = character;
        _timeMove = timeMove;
        _radius = radius;

        _currentTimeMove = _timeMove;

        _direction = GetPath();
    }

    protected override void UpdateLogic(float deltaTime)
    {
        _character.SetRotationDirection(_character.CurrentVelocity);

        _character.SetDestination(_direction);

        _currentTimeMove -= deltaTime;

        if(_currentTimeMove <= 0)
        {
            _currentTimeMove = _timeMove;
            _direction = GetPath();
        }
    }

    private Vector3 GetPath()
    {
        Vector3 positionAroundTarget;
        NavMeshHit spawnPoint;

        NavMeshQueryFilter queryFilter = new NavMeshQueryFilter();
        queryFilter.agentTypeID = 0;
        queryFilter.areaMask = 1;

        do
        {
            Vector2 randomPositionInCircle = Random.insideUnitCircle * _radius;
            Vector3 offset = new Vector3(randomPositionInCircle.x, 0, randomPositionInCircle.y);

            positionAroundTarget = _character.transform.position + offset;
        } while (NavMesh.SamplePosition(positionAroundTarget, out spawnPoint, 0.1f, queryFilter) == false);

        return spawnPoint.position;
    }
}

