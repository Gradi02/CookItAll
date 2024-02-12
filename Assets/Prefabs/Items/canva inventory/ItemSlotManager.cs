using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlotManager : MonoBehaviour
{
    private ItemScriptableObject item;
    private int itemCount = 0;

    [SerializeField] private Image img;
    [SerializeField] private TextMeshProUGUI count;






    public string GetItemName()
    {
        return item.item_name;
    }

    public void SetItem(ItemScriptableObject it)
    {
        item = it;
        itemCount = 1;
        SetItemInfo();
    }

    public void AddItem()
    {
        itemCount++;
        SetItemInfo();
    }

    private void SetItemInfo()
    {
        img.sprite = item.item_image;
        count.text = "x" + itemCount;
    }

    public void ItemThrowed()
    {
        itemCount--;
        SetItemInfo();

        if (itemCount <= 0)
        {
            transform.parent.GetComponent<InventoryManager>().DestroySlotRequest(this);
        }
    }
}
