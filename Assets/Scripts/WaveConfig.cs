using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    // Config params
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _pathPrefab;
    [SerializeField] private float _timeBetweenSpawns = 0.5f;
    [SerializeField] private float _spawnRandomFactor = 0.3f;
    [SerializeField] private int _numberOfEnemies = 5;
    [SerializeField] private float _moveSpeed = 2f;

    // Properties
    public GameObject EnemyPrefab => _enemyPrefab;
    public float TimeBetweenSpawns => _timeBetweenSpawns;
    public float SpawnRandomFactor => _spawnRandomFactor;
    public int NumberOfEnemies => _numberOfEnemies;
    public float MoveSpeed => _moveSpeed;
        
    //TODO refactor GetWaypoints and SetWaypoints to use a
    // property with a getter - remember to update this
    // in other files that call this
    public List<Transform> GetWaypoints()
    {
        var waveWaypoints = new List<Transform>();
        foreach(Transform child in _pathPrefab.transform)
        {
            waveWaypoints.Add(child);
        }

        return waveWaypoints;
    }
}
