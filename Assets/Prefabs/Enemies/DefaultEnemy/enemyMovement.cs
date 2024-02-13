using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyMovement : MonoBehaviour
{
    private Transform target;
    [SerializeField] private NavMeshAgent agent;

    void FixedUpdate()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
        else
        {
            if (GameObject.FindGameObjectWithTag("Player") != null)
            {
                target = GameObject.FindGameObjectWithTag("Player").transform;
            }
        }
    }
}
