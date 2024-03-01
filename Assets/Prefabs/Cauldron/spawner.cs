using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    private GameObject enemyToSpawn;
    private culdronManager culdronManager;
    private bool sp = false;
    public GameObject particlesPref;
    private GameObject particles;

    private float minimumSize = 1.5f;
    private float maximumSize = 2.5f;

    private void Awake()
    {
        GetComponent<Animator>().SetBool("glut", true);
        particles = Instantiate(particlesPref, transform.position, Quaternion.identity);
    }
    public void SetEnemy(GameObject enemy, culdronManager mng)
    {
        enemyToSpawn = enemy;
        culdronManager = mng;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other != null && enemyToSpawn != null && !sp && !other.CompareTag("cauldron") && !other.CompareTag("Player") && !other.CompareTag("item") && !other.CompareTag("knife") && !other.CompareTag("enemy"))
        {
            sp = true;
            GameObject e = Instantiate(enemyToSpawn, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            CustomizeEnemy(e);
            culdronManager.AddEnemyToList(e);
            e.GetComponentInChildren<EnemyInfo>().SetManager(culdronManager);
            Destroy(particles);
            Destroy(gameObject);
        }
    }

    private void CustomizeEnemy(GameObject e)
    {
        EnemyMovement mvm = e.GetComponentInChildren<EnemyMovement>();

        float rand = Random.Range(minimumSize, maximumSize);
        Vector3 randomSize = new Vector3(rand, rand, rand);
        e.transform.localScale = randomSize;

        float speed = 5 - rand;
        float health = 10 * rand;
        mvm.SetCustomData(speed, health);
    }

    private void Update()
    {
        if(particles != null)
        {
            particles.transform.position = transform.position;
        }
    }
}
