using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelecting : MonoBehaviour
{
    private List<GameObject> itemsSlots = new List<GameObject>();
    private int selectedSlot = 0;
    private int maxSlot = 3;
    [SerializeField] private GameObject[] standardSlots = new GameObject[3];
    private GameObject player;

    public void AddSlot(GameObject s)
    {
        itemsSlots.Add(s);
    }

    public void RemoveSlot(GameObject s)
    {
        itemsSlots.Remove(s);
        maxSlot = 3 + itemsSlots.Count;
        selectedSlot = (selectedSlot - 1 + maxSlot) % maxSlot;
    }

    private void Update()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            return;
        }

        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        maxSlot = 3 + itemsSlots.Count;

        //ustalam wybrany slot
        if (scrollInput < 0f)
        {
            selectedSlot = (selectedSlot + 1) % maxSlot;
        }
        else if (scrollInput > 0f)
        {
            selectedSlot = (selectedSlot - 1 + maxSlot) % maxSlot;
        }

        //aktualizuje wybrany slot na canvie
        DisableAllSlots();
        if(selectedSlot < 3)
        {
            standardSlots[selectedSlot].GetComponent<Image>().enabled = true;
        }
        else
        {
            itemsSlots[selectedSlot-3].transform.GetChild(0).GetComponent<Image>().enabled = true;
        }

        //Ustaw mo¿liwoœci gracza wzglêdem wybranego obiektu
        player.GetComponent<Shooting>().SetSelection(selectedSlot == 0 ? true : false);
        player.GetComponent<ItemThrow>().SetItem(selectedSlot < 3 ? null : itemsSlots[selectedSlot-3].GetComponent<ItemSlotManager>().GetItem());
    }


    private void DisableAllSlots()
    {
        for (int i = 0; i < 3; i++)
        {
            standardSlots[i].GetComponent<Image>().enabled = false;
        }

        for(int i = 0; i < itemsSlots.Count; i++)
        {
            itemsSlots[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
        }
    }
}
