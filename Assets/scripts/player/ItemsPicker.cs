using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemsPicker : MonoBehaviour
{
	//INTERAKCJA
	private Camera playerCamera;
	public float maxDistance = 5f;
	public TextMeshProUGUI info;

	private void Start()
	{
		playerCamera = Camera.main;
	}

	void Update()
	{
		if (playerCamera != null)
		{
			Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, maxDistance))
			{
				if (hit.collider != null && hit.transform.CompareTag("item"))
				{
					//POKAZ INFO ZE MOZESZ COS ZROBIC
					info.color = new Color(255, 255, 255, 1);
					info.text = "[e] to pickup the " + hit.transform.GetComponent<ItemManager>().GetItemName() + "!";

					if (Input.GetKeyDown(KeyCode.E))
					{
						hit.transform.GetComponent<ItemManager>().PickUpItem();
					}
				}
				else
				{
					info.color = new Color(255, 255, 255, 0);
				}
			}
			else
			{
				info.color = new Color(255, 255, 255, 0);
			}
		}
	}
}
