using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
	//STRZELANIE
	private Camera playerCamera;
	private float shootSpeed =40f;
	private float drag = 2;

	public GameObject[] weaponsBasicSet;


	//REKA
	private bool handSelected = true;
	public GameObject hand;


	//AMMO
	private int ammo = 7;
	private int Maxammo = 7;
	public TextMeshProUGUI ammotxt;
	public TextMeshProUGUI ammoWarning;
	public Slider ammoSlider;
	private bool IsReloading = false;
	private bool ShootCooldown = false;

	private void Start()
	{
		ammoWarning.color = new Color(255, 255, 255, 0);
		ammoSlider.gameObject.SetActive(false);
		playerCamera = Camera.main;
	}

	void Update()
	{
		ammotxt.text = ammo.ToString() + " / " + Maxammo.ToString();

		if(ammo == 7)
		{
			
		}

		if (ammo == 0 && IsReloading==false)
		{
			ammoWarning.text = "LMB TO RELOAD!";
			ammoWarning.color = new Color(255, 255, 255, 1);
		}

		if (ammo == 0 && IsReloading == true)
		{
			ammoWarning.text = "RELOADING";
			ammoWarning.color = new Color(255, 255, 255, 1);
		}

		if (ammo != 0)
		{
			ammoWarning.color = new Color(255, 255, 255, 0);
		}



		if (playerCamera != null && handSelected)
		{
			if (Input.GetKeyDown(KeyCode.Mouse0))
			{
				if(ammo == 0)
				{
					if (IsReloading == false)
					{
						StartCoroutine(Reload());
					}
				}

				if (ammo > 0 && ShootCooldown == false)
				{
					StartCoroutine(Cooldown());
					ammo--;
					Vector3 pos = playerCamera.transform.position + playerCamera.transform.forward * 2f;
					GameObject Knife = Instantiate(weaponsBasicSet[Random.Range(0,weaponsBasicSet.Length)], pos, Quaternion.identity);
					Rigidbody krb = Knife.GetComponent<Rigidbody>();

					// ¯EBY DOBRZE LECIA£O
					Knife.transform.LookAt(pos + playerCamera.transform.forward);
					Knife.transform.Rotate(Vector3.up, 180f);

					// NAPIERDAAALAAAAAJ!!!!!!
					krb.AddForce(playerCamera.transform.forward * shootSpeed, ForceMode.Impulse);
					krb.drag = drag;
				}
			}
		}
	}
	private IEnumerator Cooldown()
	{
		ShootCooldown = true;
		yield return new WaitForSeconds(0.3f);
		ShootCooldown = false;
	}
	private IEnumerator Reload()
	{
		ammoSlider.gameObject.SetActive(true);
		IsReloading = true;
		ammoSlider.value = 0;
		ammoSlider.maxValue = Maxammo;

		for (int i = 0; i < Maxammo; i++)
		{
			yield return new WaitForSeconds(0.5f);
			ammoSlider.value += 1;
		}
		ammo = 7;
		ammoSlider.gameObject.SetActive(false);
		IsReloading = false;
	}


	public void SetSelection(bool sin)
    {
		handSelected = sin;
		hand.SetActive(sin);
    }
}

