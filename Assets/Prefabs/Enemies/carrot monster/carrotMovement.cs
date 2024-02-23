using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class carrotMovement : MonoBehaviour, IknifeInteraction
{
    private Transform target;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator anim;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float castRange;
    [SerializeField] private ParticleSystem stunParticle;
    [SerializeField] private GameObject bulletPref;
    public LayerMask mask;

    private bool ableToAtt = false;
    private float nextAttackAt = 0;
    private bool attacking = false;
    private bool stun = false;
    private float stunOutAt = 0;
    private float stunTime = 1;

    private void Start()
    {
        agent.updateRotation = true;
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
            target = null;
            agent.SetDestination(transform.position);

            if (Time.time > stunOutAt)
            {
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
            Vector3 dir = (target.position - transform.position).normalized;
           
            RaycastHit hit;
            if (Physics.Raycast(transform.position, dir, out hit, castRange, mask))
            {
                Debug.Log(hit.collider.name);
                if(hit.collider.CompareTag("Player"))
                {
                    agent.speed = 0;
                    ableToAtt = true;
                    //anim.SetBool("move", false);
                }
                else
                {
                    agent.speed = 6;
                    ableToAtt = false;
                    //anim.SetBool("move", true);
                }
            }

            if(ableToAtt && Time.time > nextAttackAt && !attacking)
            {
                StartCoroutine(MakeAttack());
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
        nextAttackAt = Time.time + attackCooldown;
        yield return new WaitForSeconds(0.1f);
        anim.Play("attack");

        attacking = false;
    }

    public void GiveDamage()
    {
        target.GetComponent<Hp>().DamagePlayer(1);
    }

    public void StunEnemy()
    {
        stun = true;
        stunParticle.Play();
        stunOutAt = Time.time + stunTime;
    }


    //wywo³ana przez interfejs gdy trafi ten obiekt no¿em
    public void knifeInteract()
    {
        GetComponent<EnemyInfo>().DamageEnemy(GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().knifeDamage);

        StunEnemy();
    }
}
