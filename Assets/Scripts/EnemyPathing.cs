﻿using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    [SerializeField] List<Transform> waypoints;
    [SerializeField] float moveSpeed = 2f;
    int waypointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = waypoints[waypointIndex].position;        
    }

    // Update is called once per frame
    void Update()
    {
        Move();                            
    }

    private void Move()
    {
        if (waypointIndex <= waypoints.Count - 1)
        {            
            var targetPosition = waypoints[waypointIndex].position;                        
            var movementThisFrame = moveSpeed * Time.deltaTime;            
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);            
            if(targetPosition == transform.position)
            {
                waypointIndex++;                
            }
        }
        else
        {
            Destroy(gameObject);
        }                                        
    }    
}
