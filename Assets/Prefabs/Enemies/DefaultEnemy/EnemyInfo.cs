using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : MonoBehaviour // potencjalnie interfejs
{
    [Header("Basic Info")]
    [SerializeField] private float health = 20;

    [Header("Drop Info")]
    [SerializeField] private GameObject lootTableItem;
    [SerializeField, Range(0, 100)] private float lootChance;

    [Header("References")]
    private Transform ItemsHolder;






    /////////////////////////////////////////////////////////////////////

    public void Awake()
    {
        ItemsHolder = GameObject.FindGameObjectWithTag("ItemsHolder").transform;
    }



    ///////////////////////////////////////////////////////////////////////


    //Funkcja publiczna od zadawania damage przeciwnikowi
    public void DamageEnemy(float dmg)
    {
        if (health - dmg > 0)
        {
            health -= dmg;
        }
        else
        {
            health = 0;
        }

        CheckForDeath();
    }








    /////////////////////////////////////////////////////////////////////

    //Sprawdü czy hp <= 0 i zabij
    private void CheckForDeath()
    {
        if(health <= 0)
        {
            SpawnLootObject();
            DestroyEnemy();
        }
    }

    //Funkcja niszczπca obiekt przeciwnika
    private void DestroyEnemy()
    {
        //particle i inne
        Destroy(gameObject);
    }


    //Funkcja spawnu przedmiotu po úmierci
    private void SpawnLootObject()
    {
        float rand = Random.Range(0f, 100f);

        if (rand <= lootChance)
        {
            GameObject item = Instantiate(lootTableItem, transform.position, Quaternion.identity, ItemsHolder);
            item.GetComponent<Rigidbody>().AddForce(3*transform.up, ForceMode.Impulse);
        }
    }
}
