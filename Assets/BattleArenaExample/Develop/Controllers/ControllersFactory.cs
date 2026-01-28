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

    public ShootingController CreateShootingController(IShooting shooting)
    {
        return new ShootingController(shooting);
    }

    public CompositController CreateMainHeroController(Character character)
    {
        return new CompositController(
            CreatePlayerDirectionalMovableController(character),
            CreateAlomgMovableVelocityRotatableController(character, character),
            CreateShootingController(character));
    }

    public AgentCharacterAgroController CreateAgentCharacterAgroController(
        AgentCharacter character,
        Transform target)
    {
        return new AgentCharacterAgroController(character, target);
    }

    public AgentCharacterRandomMoveController CreateAgentCharacterRandomMoveController(
        AgentCharacter character,
        float timeMove,
        float radius)
    {
        return new AgentCharacterRandomMoveController(character, timeMove, radius);
    }
}
