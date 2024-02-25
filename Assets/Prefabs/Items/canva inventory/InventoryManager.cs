using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject slotPrefab;
    private List<GameObject> slots = new List<GameObject>();
    private ItemSlotManager tempSlot = null;
    private ItemSelecting selector;
    private int maxItemsPerSlot = 5;

    private void Awake()
    {
        selector = transform.parent.GetComponent<ItemSelecting>();
    }




    //Funkcja ¿e przedmiot zosta³ podniesiony
    public bool PickUpItem(ItemScriptableObject item)
    {
        if(item.stackable && CheckIfSlotExist(item))
        {
            tempSlot.AddItem();
            return true;     
        }
        else
        {
            if (slots.Count < 7)
            {
                GameObject new_slot = Instantiate(slotPrefab, transform.position, Quaternion.identity, transform);
                new_slot.GetComponent<ItemSlotManager>().SetItem(item);
                slots.Add(new_slot);
                selector.AddSlot(new_slot);
                return true;
            }
            else
            {
                Debug.Log("brak miejsca na wiêcej itemów");
                return false;
            }
        }
    }

    public void RemoveItemFromInv()
    {
        slots[selector.GetSelectedSlot() - 3].GetComponent<ItemSlotManager>().ItemThrowed();
    }



    //Proœba o zniszczenie slota gdy jest pusty
    public void DestroySlotRequest(GameObject slot)
    {
        slots.Remove(slot);
        selector.RemoveSlot(slot);
        Destroy(slot);
    }


    //sprawdzam czy slot juz istnieje
    private bool CheckIfSlotExist(ItemScriptableObject item)
    {
        if(slots.Count > 0)
        {
            for(int i=0; i<slots.Count; i++)
            {
                if(slots[i].GetComponent<ItemSlotManager>().GetItemName() == item.item_name && slots[i].GetComponent<ItemSlotManager>().GetItemCount() < maxItemsPerSlot)
                {
                    tempSlot = slots[i].GetComponent<ItemSlotManager>();
                    return true;
                }
            }
        }

        //nie ma slota z tym przedmiotem
        return false;
    }


    //czy moge usun¹æ
    private bool CheckIfCanRemove(ItemScriptableObject item)
    {
        if (slots.Count > 0)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (slots[i].GetComponent<ItemSlotManager>().GetItemName() == item.item_name)
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
