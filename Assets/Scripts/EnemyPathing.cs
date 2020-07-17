using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    private WaveConfig _waveConfig;        
    private List<Transform> _waypoints;
    private int _waypointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (_waveConfig)
        {
            _waypoints = _waveConfig.GetWaypoints();
            transform.position = _waypoints[_waypointIndex].position;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_waveConfig)
        {
            Move();
        }        
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this._waveConfig = waveConfig;
    }

    private void Move()
    {
        if (_waypointIndex <= _waypoints.Count - 1)
        {            
            var targetPosition = _waypoints[_waypointIndex].position;                        
            var movementThisFrame = _waveConfig.GetMoveSpeed() * Time.deltaTime;            
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);            
            if(targetPosition == transform.position)
            {
                _waypointIndex++;                
            }
        }
        else
        {
            Destroy(gameObject);
        }                                        
    }    
}
