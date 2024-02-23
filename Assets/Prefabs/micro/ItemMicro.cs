using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemMicro : MonoBehaviour
{
    private ItemScriptableObject item;
    public Image img;

    public void SetItem(ItemScriptableObject i)
    {
        item = i;
        img.sprite = i.item_image;
        transform.localRotation = Quaternion.identity;
    }
}
