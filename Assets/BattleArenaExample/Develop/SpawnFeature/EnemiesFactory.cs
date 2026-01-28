using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesFactory
{
    private ControllersUpdateService _controllersUpdateService;
    private ControllersFactory _controllersFactory;
    private CharactersFactory _charactersFactory;

    public EnemiesFactory(
        ControllersUpdateService controllersUpdateService, 
        ControllersFactory controllersFactory, 
        CharactersFactory charactersFactory)
    {
        _controllersUpdateService = controllersUpdateService;
        _controllersFactory = controllersFactory;
        _charactersFactory = charactersFactory;
    }

    public AgentCharacter CreateAgentEnemy(AgentEnemyConfig config, Vector3 spawnPosition)
    {

        AgentCharacter instance = _charactersFactory.CreateAgentCharacter(
            config.Prefab,
            spawnPosition,
            config.MoveSpeed,
            config.RotationSpeed,
            config.Health,
            config.Damage);

        Controller controller = _controllersFactory.CreateAgentCharacterRandomMoveController(instance, config.TimeMove, config.Radius);

        controller.Enable();

        _controllersUpdateService.Add(controller, () => instance.IsDestroyed);

        return instance;
    }

}
