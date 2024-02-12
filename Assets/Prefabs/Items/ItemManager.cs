using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [Header("Item Info")]
    [SerializeField] private ItemScriptableObject item;  






    //Funkcja podnoszenia przedmiotu
    private void PickUpItem()
    {

        DestroyItem();
    }


    //Funkcja niszcz�ca obiekt
    private void DestroyItem()
    {
        //animacja i particle
        Destroy(gameObject);
    }
}
