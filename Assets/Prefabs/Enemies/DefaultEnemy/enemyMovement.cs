using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator anim;

    private void Start()
    {
        agent.updateRotation = true;
    }

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

    private void Update()
    {
        //  if (!agent.isStopped)
        //  {
        //      if (agent.desiredVelocity.magnitude > agent.stoppingDistance)
        //      {

        if (target != null)
        {
            float distance = Vector3.Distance(this.gameObject.transform.position, target.position);

            if (distance > 3)
            {
                anim.SetBool("walk", true);

                // Oblicz kierunek do celu
                Vector3 targetDirection = (agent.steeringTarget - transform.position).normalized;

                // Obróæ w kierunku celu
                if (targetDirection != Vector3.zero)
                {
                    Quaternion lookRotation = Quaternion.LookRotation(-targetDirection);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, 360 * Time.deltaTime);
                }
            }
            else
            {
                anim.SetBool("walk", false);
                anim.Play("attack");
            }
        }
    }
}

//}
