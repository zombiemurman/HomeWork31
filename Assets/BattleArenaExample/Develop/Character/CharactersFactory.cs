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
        float rotationSpeed,
        float health)
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

        instance.Initialize(mover, rotater, health);

        return instance;
    }

    public AgentCharacter CreateAgentCharacter(
        AgentCharacter prefab,
        Vector3 spawnPosition,
        float moveSpeed,
        float rotationSpeed,
        float health,
        float damage)
    {
        AgentCharacter instance = Object.Instantiate(prefab, spawnPosition, Quaternion.identity, null);

        NavMeshAgent agent;

        if (instance.TryGetComponent(out agent) == false)
            throw new InvalidOperationException("Not found agent component");

        agent.updateRotation = false;

        AgentMover mover = new AgentMover(agent, moveSpeed);
        TransformDirectionalRotater rotator = new TransformDirectionalRotater(instance.transform, rotationSpeed);

        instance.Initialize(agent, mover, rotator, health, damage);

        return instance;
    }
}
