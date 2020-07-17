using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] GameObject _enemyPrefab;
    [SerializeField] GameObject _pathPrefab;
    [SerializeField] float _timeBetweenSpawns = 0.5f;
    [SerializeField] float _spawnRandomFactor = 0.3f;
    [SerializeField] int _numberOfEnemies = 5;
    [SerializeField] float _moveSpeed = 2f;

    public GameObject GetEnemyPrefab(){ return _enemyPrefab; }

    public List<Transform> GetWaypoints()
    {
        var waveWaypoints = new List<Transform>();
        foreach(Transform child in _pathPrefab.transform)
        {
            waveWaypoints.Add(child);
        }

        return waveWaypoints;
    }

    public float GetTimeBetweenSpawns(){ return _timeBetweenSpawns; }

    public float GetSpawnRandomFactor(){ return _spawnRandomFactor; }

    public int GetNumberOfEnemies(){ return _numberOfEnemies; }

    public float GetMoveSpeed(){ return _moveSpeed; }
}
