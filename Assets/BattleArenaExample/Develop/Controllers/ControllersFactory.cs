using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllersFactory
{
    public PlayerDirectionalMovableController CreatePlayerDirectionalMovableController(IDirectionalMovable movable)
    {
        return new PlayerDirectionalMovableController(movable);
    }

    public AlomgMovableVelocityRotatableController CreateAlomgMovableVelocityRotatableController(IDirectionalRotatable rotatable, IDirectionalMovable movable)
    {
        return new AlomgMovableVelocityRotatableController(rotatable, movable);
    }

    public CompositController CreateMainHeroController(Character character)
    {
        return new CompositController(
            CreatePlayerDirectionalMovableController(character),
            CreateAlomgMovableVelocityRotatableController(character, character));
    }

    public AgentCharacterAgroController CreateAgentCharacterAgroController(
        AgentCharacter character,
        Transform target,
        float agroRange,
        float minDistanceToTarget,
        float timeToIdle)
    {
        return new AgentCharacterAgroController(character, target, agroRange, minDistanceToTarget, timeToIdle);
    }
}
