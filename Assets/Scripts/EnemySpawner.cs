using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> _waveConfigs;
    [SerializeField] int _startingWave = 0;
    [SerializeField] bool _looping = false;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
        while (_looping);        
    }

    private IEnumerator SpawnAllWaves()
    {
        for(int waveIndex = _startingWave; waveIndex < _waveConfigs.Count; waveIndex++)
        {
            var currentWave = _waveConfigs[waveIndex];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {        
        for (int enemyCount = 0; enemyCount < waveConfig.NumberOfEnemies; enemyCount++)
        {
            var newEnemy = Instantiate(waveConfig.EnemyPrefab,
                    waveConfig.GetWaypoints()[0].transform.position,
                    Quaternion.identity);

            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);

            yield return new WaitForSeconds(waveConfig.TimeBetweenSpawns);
        }        
    }   
}
