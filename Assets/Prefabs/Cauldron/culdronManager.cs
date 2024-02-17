using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class culdronManager : MonoBehaviour, IknifeInteraction
{
    [Header("Main")]
    [SerializeField] private float startSpawnCooldown = 5;
    [SerializeField] private float middleSpawnCooldown = 5;
    [SerializeField] private int minEnemies = 3;
    [SerializeField] private float freezeTime = 10;
    [SerializeField] private int shootsToFreeze = 3;
    [SerializeField] private int MinEnemiesToSpawn = 2;
    [SerializeField] private int MaxEnemiesToSpawn = 3;

    [Header("Prefabs")]
    [SerializeField] private GameObject[] enemiesPrefabs; //prefaby dla danego levela
    [SerializeField] private GameObject spawnerPrefab;    //prefab gluta wypluwanego z kot³a
    [SerializeField] private Transform spawningPos;


    private List<GameObject> currentEnemiesObjects = new List<GameObject>();
    private int enemyCount = 0;
    private int shootCounter = 0;
    private float timeToRestart = 3;
    private float restartIn = 0;
    private bool freeze = false;
    private float unfreeze = 0;
    private bool canSpawn = false;




    private void Awake()
    {
        canSpawn = false;
        StartCoroutine(SpawnEnemies(3));
    }


    public void knifeInteract()
    {
        if (!freeze)
        {
            restartIn = Time.time + timeToRestart;
            shootCounter++;
            Debug.Log("count: " + shootCounter);
        }
    }

    private void Update()
    {
        //zamra¿anie
        if(Time.time > restartIn && shootCounter > 0)
        {
            shootCounter = 0;
        }

        if(shootCounter > shootsToFreeze && !freeze)
        {
            shootCounter = 0;
            freeze = true;
            canSpawn = false;
            unfreeze = Time.time + freezeTime;
            StartCoroutine(SpawnEnemies(Random.Range(MinEnemiesToSpawn, MaxEnemiesToSpawn)));
        }

        if(Time.time > unfreeze && freeze)
        {
            shootCounter = 0;
            freeze = false;
        }

        if(canSpawn && !freeze)
        {
            canSpawn = false;
            StartCoroutine(SpawnEnemies(Random.Range(MinEnemiesToSpawn, MaxEnemiesToSpawn)));
        }
    }

    private IEnumerator SpawnEnemies(int count)
    {
        enemyCount += count;
        yield return new WaitForSeconds(startSpawnCooldown);

        for(int i = 0; i < count; i++)
        {
            yield return new WaitForSeconds(middleSpawnCooldown);
            GameObject glut = Instantiate(spawnerPrefab, spawningPos.position, Quaternion.identity);
            gameObject.GetComponentInChildren<Animator>().Play("spawning");

            //losuje parametry rzucenia
            float randomAngle = Random.Range(0f, Mathf.PI * 2f);
            float randomRadius = Random.Range(2, 6);
            float x = randomRadius * Mathf.Cos(randomAngle);
            float z = randomRadius * Mathf.Sin(randomAngle);
            float y = Mathf.Tan(Mathf.PI / 3f) * randomRadius;

            Vector3 direction = new Vector3(x, y, z);

            //stosuje
            Rigidbody rb = glut.GetComponent<Rigidbody>();
            rb.AddForce(direction, ForceMode.Impulse);

            //nadaje mu przeciwnika
            GameObject enemyToSpawn = enemiesPrefabs[Random.Range(0, enemiesPrefabs.Length)];
            glut.GetComponent<spawner>().SetEnemy(enemyToSpawn, this);
        }

        if (enemyCount < minEnemies)
        {
            canSpawn = true;
        }
    }

    public void AddEnemyToList(GameObject e)
    {
        currentEnemiesObjects.Add(e);
    }

    public void RemoveEnemyFromList(GameObject e)
    {
        currentEnemiesObjects.Remove(e);
        enemyCount--;

        if (enemyCount < minEnemies)
        {
            canSpawn = true;
        }
    }
}
