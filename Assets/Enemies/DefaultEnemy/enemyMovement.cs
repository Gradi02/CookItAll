using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    private Transform target;
    [SerializeField] private float speed;

    
    void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 targetPosition = target.transform.position - transform.position;

            transform.position += targetPosition * Time.fixedDeltaTime * speed;
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
