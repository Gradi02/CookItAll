using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering;

public class Shooting : MonoBehaviour
{
	//STRZELANIE
	private Camera playerCamera;
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

