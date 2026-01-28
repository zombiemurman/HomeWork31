using UnityEngine;
using UnityEngine.AI;

public class AgentCharacterAgroController : Controller
{
    private AgentCharacter _character;

    private Transform _target;

    private NavMeshPath _pathToTarget = new NavMeshPath();

    public AgentCharacterAgroController(AgentCharacter character, Transform target)
    {
        _character = character;
        _target = target;
    }

    protected override void UpdateLogic(float deltaTime)
    {

        _character.SetRotationDirection(_character.CurrentVelocity);

        if (_character.TryGetPath(_target.position, _pathToTarget))
        {
            float distanceToTarget = NavMeshUtils.GetPathLength(_pathToTarget);

            _character.ResumeMove();
            _character.SetDestination(_target.position);
        }

        _character.StopMove();
    }

}
