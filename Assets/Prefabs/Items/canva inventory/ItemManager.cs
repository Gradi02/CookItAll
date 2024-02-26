using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [Header("Item Info")]
    [SerializeField] private ItemScriptableObject item;
    private InventoryManager inv_manager;
    public bool throwed = false;
    private void Awake()
    {
        inv_manager = GameObject.FindGameObjectWithTag("inv").GetComponent<InventoryManager>();
    }



    //Funkcja podnoszenia przedmiotu
    public void PickUpItem()
    {
        if (inv_manager != null)
        {
            if (inv_manager.PickUpItem(item))
            {
                DestroyItem();
            }
        }
        else
        {
            Debug.Log("Nie mo¿na podnieœæ przedmiotu - nie znaleziono inventoryManager");
        }
    }


    //Funkcja niszcz¹ca obiekt
    private void DestroyItem()
    {
        //animacja i particle
        Destroy(gameObject);
    }

    public string GetItemName()
    {
        return item.item_name;
    }

    public Sprite GetIcon()
    {
        return item.item_image;
    }

    public ItemScriptableObject GetItem()
    {
        return item;
    }


    public void OnCollisionEnter(Collision other)
    {
        if (other != null && item.stunTime > 0 && GetComponent<Rigidbody>().velocity.magnitude > 2 && throwed)
        {
            IitemInteraction interaction = other.collider.GetComponent<IitemInteraction>();

            if (interaction != null)
            {
                interaction.itemInteract(item.stunTime);
            }
        }
        else
        {
            throwed = false;
        }
    }
}
