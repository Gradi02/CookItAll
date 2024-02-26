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
	private float shootSpeed =60f;
	public GameObject[] weaponsBasicSet;
	//===================================
	private GameObject currentWeapon = null;
	public Transform knifeSpawnPos;
	public Transform handTransform;
	private GameObject knife = null;
	private float nextShoot = 0;
	private float shotCol = 0.3f;
	public float knifeDamage = 6;

	//REKA
	private bool handSelected = true;
	public GameObject hand;
	public GameObject handPivot;


	//AMMO
	private int ammo = 15;
	private int Maxammo = 15;
	public TextMeshProUGUI ammotxt;
	public TextMeshProUGUI ammoWarning;
	public Slider ammoSlider;
	private bool IsReloading = false;

	private void Start()
	{
		ammoWarning.color = new Color(255, 255, 255, 0);
		ammoSlider.gameObject.SetActive(false);
		playerCamera = Camera.main;
	}

	void Update()
	{
		//Ammo manager
		ammotxt.text = ammo.ToString() + " / " + Maxammo.ToString();

		if (IsReloading == true)
		{
			ammoWarning.text = "RELOADING";
			ammoWarning.color = new Color(255, 255, 255, 1);
			if(knife != null)
				Destroy(knife);
		}

		if (ammo != 0)
		{
			ammoWarning.color = new Color(255, 255, 255, 0);
		}





		//Shooting manager
		if(currentWeapon == null)
        {
			int rand = Random.Range(0, weaponsBasicSet.Length);
			currentWeapon = weaponsBasicSet[rand];
		}

		if(knife != null)
        {
			knife.transform.localPosition = Vector3.zero;
        }


		if (playerCamera != null && handSelected)
		{
			if (Input.GetKeyDown(KeyCode.Mouse0) && !IsReloading && knife != null && ammo > 0)
			{
				if (Time.time > nextShoot)
				{
					nextShoot = Time.time + shotCol;
					ammo--;
					//
					LeanTween.rotateAround(handPivot, new Vector3(0, 0, 1), 360, shotCol);
					//
                    knife.transform.parent = null;
					knife.GetComponent<Rigidbody>().useGravity = true;
					knife.GetComponent<BoxCollider>().enabled = true;
					knife.GetComponent<knifeManager>().enabled = true;
					knife.GetComponent<Rigidbody>().AddForce(playerCamera.transform.forward * shootSpeed, ForceMode.Impulse);
					currentWeapon = null;
					knife = null;
				}
			}

			if(Input.GetKeyDown(KeyCode.R) && ammo < Maxammo)
            {
				if (IsReloading == false)
				{
					StartCoroutine(Reload());
				}
			}

			if (ammo <= 0)
			{
				if (IsReloading == false)
				{
					StartCoroutine(Reload());
				}
			}
		}
	}

	private IEnumerator Reload()
	{
		ammoSlider.gameObject.SetActive(true);
		IsReloading = true;
		ammoSlider.maxValue = Maxammo;
		ammoSlider.value = ammo;

		for (int i = ammo; i < Maxammo; i++)
		{
			yield return new WaitForSeconds(0.3f);
			ammoSlider.value += 1;
		}
		ammo = Maxammo;
		ammoSlider.gameObject.SetActive(false);
		IsReloading = false;
	}


	public void SetSelection(bool sin)
    {
		handSelected = sin;
		hand.SetActive(sin);

		if(sin)
        {
			if (knife == null && !IsReloading)
			{
				if (currentWeapon != null)
				{
					knife = Instantiate(currentWeapon, knifeSpawnPos.position, Quaternion.identity, handTransform);
					knife.GetComponent<Rigidbody>().useGravity = false;
					knife.GetComponent<BoxCollider>().enabled = false;

					Vector3 direction = playerCamera.transform.forward;
					Quaternion targetRotation = Quaternion.LookRotation(direction);
					knife.transform.rotation = targetRotation;
					knife.transform.Rotate(Vector3.up, 180f);
				}
				else
				{
					int rand = Random.Range(0, weaponsBasicSet.Length);
					currentWeapon = weaponsBasicSet[rand];

					knife = Instantiate(currentWeapon, knifeSpawnPos.position, Quaternion.identity, handTransform);
					knife.GetComponent<Rigidbody>().useGravity = false;
					knife.GetComponent<BoxCollider>().enabled = false;

					Vector3 direction = playerCamera.transform.forward;
					Quaternion targetRotation = Quaternion.LookRotation(direction);
					knife.transform.rotation = targetRotation;
					knife.transform.Rotate(Vector3.up, 180f);
				}
			}
		}
		else
        {
			Destroy(knife);
        }
    }
}

