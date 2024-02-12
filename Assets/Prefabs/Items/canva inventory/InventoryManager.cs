using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject slotPrefab;
    private List<GameObject> slots = new List<GameObject>();
    private ItemSlotManager tempSlot = null;
    private ItemSelecting selector;

    private void Awake()
    {
        selector = transform.parent.GetComponent<ItemSelecting>();
    }




    //Funkcja ¿e przedmiot zosta³ podniesiony
    public void PickUpItem(ItemScriptableObject item)
    {
        if(CheckIfSlotExist(item))
        {
            tempSlot.AddItem();
        }
        else
        {
            GameObject new_slot = Instantiate(slotPrefab, transform.position, Quaternion.identity, transform);
            new_slot.GetComponent<ItemSlotManager>().SetItem(item);
            slots.Add(new_slot);
            selector.AddSlot(new_slot);
        }
    }

    public void RemoveItemFromInv(ItemScriptableObject item)
    {
        if (CheckIfSlotExist(item))
        {
            tempSlot.ItemThrowed();
        }
        else
        {
            Debug.Log("Chcesz usun¹æ przedmiot którego gracz nie posiada!");
        }
    }



    //Proœba o zniszczenie slota gdy jest pusty
    public void DestroySlotRequest(ItemSlotManager slot)
    {
        GameObject slotToDst = null;
        for(int i = 0; i < slots.Count; i++)
        {
            if(slots[i].GetComponent<ItemSlotManager>().GetItemName() == slot.GetItemName())
            {
                slotToDst = slots[i];
            }
        }

        if(slotToDst != null)
        {
            slots.Remove(slotToDst);
            Destroy(slotToDst);

        }
    }


    //sprawdzam czy slot juz istnieje
    private bool CheckIfSlotExist(ItemScriptableObject item)
    {
        if(slots.Count > 0)
        {
            for(int i=0; i<slots.Count; i++)
            {
                if(slots[i].GetComponent<ItemSlotManager>().GetItemName() == item.item_name)
                {
                    tempSlot = slots[i].GetComponent<ItemSlotManager>();
                    return true;
                }
            }
        }

        //nie ma slota z tym przedmiotem
        return false;
    }
}
