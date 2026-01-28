
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class EnemySpawnPoints : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnPoints;

    public List<Transform> SpawnPoints => _spawnPoints;
}

