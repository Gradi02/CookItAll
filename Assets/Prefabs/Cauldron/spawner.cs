using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    private GameObject enemyToSpawn;
    private culdronManager culdronManager;
    private bool sp = false;

    private void Awake()
    {
        GetComponent<Animator>().SetBool("glut", true);
    }
    public void SetEnemy(GameObject enemy, culdronManager mng)
    {
        enemyToSpawn = enemy;
        culdronManager = mng;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other != null && enemyToSpawn != null && !sp && !other.CompareTag("cauldron"))
        {
            sp = true;
            GameObject e = Instantiate(enemyToSpawn, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            culdronManager.AddEnemyToList(e);
            e.GetComponent<EnemyInfo>().SetManager(culdronManager);
            Destroy(gameObject);
        }
    }
}
