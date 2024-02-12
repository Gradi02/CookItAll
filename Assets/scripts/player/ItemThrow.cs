using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemThrow : MonoBehaviour
{
    private ItemScriptableObject selectedItem;
    private GameObject item = null;
    public Transform itemTransform;

    public void SetItem(ItemScriptableObject it)
    {
        RemoveCurrentItem();
        selectedItem = it;
    }

    private void RemoveCurrentItem()
    {
        if (item != null)
        {
            Destroy(item);
            item = null;
        }
    }

    private void Update()
    {
        if(selectedItem != null && item == null)
        {
            item = Instantiate(selectedItem.item_model, transform.position, Quaternion.identity, itemTransform);
        }
        else if(selectedItem == null && item != null)
        {
            Destroy(item);
            item = null;
        }
    }
}
