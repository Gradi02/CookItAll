using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemThrow : MonoBehaviour
{
    private ItemScriptableObject selectedItem;
    private GameObject item = null;
    public Transform itemTransform;
    [SerializeField] private float power;
    [SerializeField] private Transform itemHolder;

    public void SetItem(ItemScriptableObject it)
    {
        if (it != null && selectedItem != null)
        {
            if (it.item_name != selectedItem.item_name)
            {
                RemoveCurrentItem();
            }
        }

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
            item.GetComponent<BoxCollider>().enabled = false;
            item.GetComponent<Rigidbody>().useGravity = false;
            item.GetComponent<Rigidbody>().freezeRotation = true;
        }
        else if(selectedItem == null && item != null)
        {
            Destroy(item);
            item = null;
        }

        if(item != null) item.transform.localPosition = Vector3.zero;




        //strzelanie
        if (item != null && selectedItem != null)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                item.GetComponent<BoxCollider>().enabled = true;
                item.GetComponent<Rigidbody>().useGravity = true;
                item.GetComponent<Rigidbody>().freezeRotation = false;
                item.transform.parent = itemHolder;
                item.GetComponent<ItemManager>().throwed = true;

                Vector3 pos = Camera.main.transform.forward;
                item.GetComponent<Rigidbody>().AddForce(pos * power, ForceMode.Impulse);
                item.GetComponent<Rigidbody>().AddTorque(0.3f * transform.right + 0.2f * transform.forward, ForceMode.Impulse);

                GameObject.FindGameObjectWithTag("inv").GetComponent<InventoryManager>().RemoveItemFromInv();

                selectedItem = null;
                item = null;
            } 
        }
    }
}
