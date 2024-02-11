using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering;

public class Interacting : MonoBehaviour
{
	//INTERAKCJA
	private Camera playerCamera;
	public float maxDistance = 5f;
	public TextMeshProUGUI info;


	//STRZELANIE
	public GameObject knife;
	private float shootSpeed = 50f;
	private float drag = 2;



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
				if (hit.collider != null)
				{
					//POKAZ INFO ZE MOZESZ COS ZROBIC
					info.color = new Color(255,255,255,1);
					info.text = "E - To Destroy";

					if (Input.GetKeyDown(KeyCode.E))
					{
						Destroy(hit.collider.gameObject);
					}
				}
			}
			else
			{
				info.color = new Color(255, 255, 255, 0);
			}



			if (Input.GetKeyDown(KeyCode.Mouse0))
			{
				Vector3 pos = playerCamera.transform.position + playerCamera.transform.forward * 2f;
				GameObject Knife = Instantiate(knife, pos, Quaternion.identity);
				Rigidbody krb = Knife.GetComponent<Rigidbody>();

				// ¯EBY DOBRZE LECIA£O
				Knife.transform.LookAt(pos + playerCamera.transform.forward);
				Knife.transform.Rotate(Vector3.up, 90f);

				// NAPIERDAAALAAAAAJ!!!!!!
				krb.AddForce(playerCamera.transform.forward * shootSpeed, ForceMode.Impulse);
				krb.drag = drag;
			}
		}
	}
}

