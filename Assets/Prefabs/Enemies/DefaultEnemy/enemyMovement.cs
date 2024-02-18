using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator anim;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float attackRange;
    private float nextAttackAt = 0;
    private bool attacking = false;

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
        if (target != null)
        {
            float distance = Vector3.Distance(this.gameObject.transform.position, target.position);

            if (distance > 3)
            {
                nextAttackAt = 0;
                attacking = false;
                anim.SetBool("walk", true);

                Vector3 targetDirection = (agent.steeringTarget - transform.position).normalized;

                if (targetDirection != Vector3.zero)
                {
                    Quaternion lookRotation = Quaternion.LookRotation(-targetDirection);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, 360 * Time.deltaTime);
                }
            }
            else
            {
                anim.SetBool("walk", false);
                //anim.Play("attack");

                if(Time.time > nextAttackAt && !attacking)
                {
                    StartCoroutine(MakeAttack());
                }
            }
        }
    }

    private IEnumerator MakeAttack()
    {
        attacking = true;
        anim.Play("attack");
        yield return new WaitForSeconds(1f);
        nextAttackAt = Time.time + attackCooldown;
        attacking = false;
        
        Vector3 targetDirection = (agent.steeringTarget - transform.position).normalized;

        if (targetDirection != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(-targetDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, 360 * Time.deltaTime);
        }

        //zadaj dmg
        if (Vector3.Distance(transform.position, target.position) < attackRange)
        {
            Debug.Log("Zadano dmg");
            GiveDamage();
        }
    }

    public void GiveDamage()
    {
        target.GetComponent<Hp>().health_val -= 1;
	}
}
