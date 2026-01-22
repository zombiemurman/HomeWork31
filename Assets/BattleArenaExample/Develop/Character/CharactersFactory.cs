using System;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;

public class CharactersFactory
{
    public Character CreateCharacter(
        Character prefab,
        Vector3 spawnPosition,
        float moveSpeed,
        float rotationSpeed)
    {
        Character instance = Object.Instantiate(prefab, spawnPosition, Quaternion.identity, null);

        DirectionalMover mover;
        DirectionalRotator rotater;

        if (instance.TryGetComponent(out CharacterController characterController))
        {
            mover = new CharacterControllerDirectionalMover(characterController, moveSpeed);
            rotater = new TransformDirectionalRotater(instance.transform, rotationSpeed);
        }
        else if (instance.TryGetComponent(out Rigidbody rigidbody))
        {
            mover = new RigidbodyDirectionalMover(rigidbody, moveSpeed);
            rotater = new RigidbodyDirectionRotator(rigidbody, rotationSpeed);
        }
        else
        {
            throw new InvalidOperationException("Not found mover component");
        }

        instance.Initialize(mover, rotater);

        return instance;
    }

    public AgentCharacter CreateAgentCharacter(
        AgentCharacter prefab,
        Vector3 spawnPosition,
        float moveSpeed,
        float rotationSpeed,
        float jumpSpeed,
        AnimationCurve jumpCurve,
        float timeToSpawn)
    {
        AgentCharacter instance = Object.Instantiate(prefab, spawnPosition, Quaternion.identity, null);

        NavMeshAgent agent;

        if (instance.TryGetComponent(out agent) == false)
            throw new InvalidOperationException("Not found agent component");

        agent.updateRotation = false;

        AgentMover mover = new AgentMover(agent, moveSpeed);
        TransformDirectionalRotater rotator = new TransformDirectionalRotater(instance.transform, rotationSpeed);
        AgentJumper jumper = new AgentJumper(jumpSpeed, agent, instance, jumpCurve);

        Timer spawnTimer = new Timer(instance);

        instance.Initialize(agent, mover, rotator, jumper, spawnTimer, timeToSpawn);

        return instance;
    }
}
