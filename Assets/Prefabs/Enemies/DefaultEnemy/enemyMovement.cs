using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour, IknifeInteraction, IitemInteraction
{
    private Transform target;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator anim;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float attackRange;
    private float nextAttackAt = 0;
    private bool attacking = false;
    private bool stun = false;
    private float stunOutAt = 0;
    //private float stunTime = 1;
    [SerializeField] private ParticleSystem stunParticle;
    private float normalSpeed;

    private void Start()
    {
        agent.updateRotation = true;
        normalSpeed = agent.speed;
    }

    void FixedUpdate()
    {
        if (!stun)
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
        else
        {
            //target = null;
            //agent.SetDestination(transform.position);
            agent.speed = 0;

            if(Time.time > stunOutAt)
            {
                agent.speed = normalSpeed;
                stun = false;
                stunOutAt = 0;
            }
        }
    }

    private void Update()
    {
        Vector3 targetDirection;
        if (target != null)
        {
            float distance = Vector3.Distance(this.gameObject.transform.position, target.position);

            if (distance > 3)
            {
                nextAttackAt = 0;
                attacking = false;

                if(!stun)
                    anim.SetBool("walk", true);
                else
                    anim.SetBool("walk", false);
            }
            else
            {
                anim.SetBool("walk", false);

                if(Time.time > nextAttackAt && !attacking)
                {
                    StartCoroutine(MakeAttack());
                }
            }
        
            targetDirection = (agent.steeringTarget - transform.position).normalized;

            if (targetDirection != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(-targetDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, 360 * Time.deltaTime);
            }
            
        }


    }

    private IEnumerator MakeAttack()
    {
        attacking = true;
        anim.Play("attack");
        yield return new WaitForSeconds(1f);
        nextAttackAt = Time.time + attackCooldown;

        //zadaj dmg
        if (!stun)
        {
            if (Vector3.Distance(transform.position, target.position) < attackRange)
            {
                GiveDamage();
            }
        }
        attacking = false;
    }

    public void GiveDamage()
    {
        target.GetComponent<Hp>().DamagePlayer(1);
	}

    public void StunEnemy(float stunTimeIn)
    {
        if (!stun)
        {
            stun = true;

            ParticleSystem.MainModule main = stunParticle.main;
            main.duration = stunTimeIn;

            stunParticle.Play();
            stunOutAt = Time.time + stunTimeIn;
        }
    }


    //wywo³ana przez interfejs gdy trafi ten obiekt no¿em
    public void knifeInteract()
    {
        GetComponent<EnemyInfo>().DamageEnemy(GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().knifeDamage);
    }

    public void itemInteract(float stunTimeIn)
    {
        GetComponent<EnemyInfo>().DamageEnemy(2);
        StunEnemy(stunTimeIn);
    }
}
