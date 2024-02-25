using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : MonoBehaviour
{
    [Header("Basic Info")]
    [SerializeField] private float health = 20;

    [Header("Drop Info")]
    [SerializeField] private GameObject lootTableItem;
    [SerializeField, Range(0, 100)] private float lootChance;

    [Header("References")]
    private Transform ItemsHolder;
    private culdronManager culdronManager;
    [SerializeField] private Rigidbody rb;
    public ParticleSystem blood;




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

    public void SetManager(culdronManager m)
    {
        culdronManager = m;
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
        if(culdronManager != null)
            culdronManager.RemoveEnemyFromList(gameObject);

        Vector3 offset = blood.transform.localPosition;
        blood.transform.parent = null;
        blood.transform.localScale = new Vector3(1, 1, 1);
        blood.transform.position = transform.position + offset;
        blood.Play();
        Destroy(blood.gameObject, 3);
        Destroy(gameObject);
    }


    //Funkcja spawnu przedmiotu po úmierci
    private void SpawnLootObject()
    {
        float rand = Random.Range(0f, 100f);

        if (rand <= lootChance && lootTableItem != null)
        {
            GameObject item = Instantiate(lootTableItem, transform.position + new Vector3(0, 2, 0), Quaternion.identity, ItemsHolder);
            item.GetComponent<Rigidbody>().AddForce(3*transform.up, ForceMode.Impulse);
            item.GetComponent<Rigidbody>().AddTorque(0.3f * transform.right + 0.2f * transform.forward, ForceMode.Impulse);
        }
    }
}
