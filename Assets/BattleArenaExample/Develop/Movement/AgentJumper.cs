using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AgentJumper
{
    private float _jumpSpeed;

    private NavMeshAgent _agent;

    private AnimationCurve _yOffsetCurve;

    private MonoBehaviour _coroutineRunner;
        
    private Coroutine _jumpProcess;

    public AgentJumper(float jumpSpeed, NavMeshAgent agent, MonoBehaviour coroutineRunner, AnimationCurve yOffsetCurve)
    {
        _jumpSpeed = jumpSpeed;
        _agent = agent;
        _coroutineRunner = coroutineRunner;
        _yOffsetCurve = yOffsetCurve;
    }

    public bool InProcess => _jumpProcess != null;

    public void Jump(OffMeshLinkData offMeshLinkData)
    {
        if(InProcess)
            return;

        _jumpProcess = _coroutineRunner.StartCoroutine(JumpProcess(offMeshLinkData));  
    }

    private IEnumerator JumpProcess(OffMeshLinkData offMeshLinkData)
    {
        Vector3 startPosition = offMeshLinkData.startPos;
        Vector3 endPosition = offMeshLinkData.endPos;

        float duration = Vector3.Distance(startPosition, endPosition) / _jumpSpeed;

        float progress = 0;

        while(progress < duration)
        {
            float yOffset = _yOffsetCurve.Evaluate(progress / duration);

            _agent.transform.position = Vector3.Lerp(startPosition, endPosition, progress / duration) + Vector3.up * yOffset;

            progress += Time.deltaTime;

            yield return null;
        }

        _agent.CompleteOffMeshLink();
        _jumpProcess = null;
    }
}
