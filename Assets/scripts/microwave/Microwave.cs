using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Microwave : MonoBehaviour
{
	private List<GameObject> ItemsList = new();
	public GameObject dish;
	private bool usable = true;

	private void Start()
	{
		dish.SetActive(false);
	}
	private void OnTriggerEnter(Collider item)
	{
		// Sprawdzamy, czy obiekt, który wszed³ w obszar wyzwalacza, ma tag "Player".
		if (item.CompareTag("item") && usable == true)
		{
			gameObject.GetComponent<Animator>().Play("microwave_product");
			ItemsList.Add(item.gameObject);
			Debug.Log(ItemsList.Count);
			Debug.Log(ItemsList);
			Destroy(item.gameObject);
		}

		if (item.CompareTag("knife"))
		{
			StartCoroutine(CookingTime());
		}
	}


	private IEnumerator CookingTime()
	{
		usable = false;
		gameObject.GetComponent<Animator>().SetBool("cooking", true);
		yield return new WaitForSeconds(5f);
		gameObject.GetComponent<Animator>().SetBool("cooking", false);
		usable = true;
		dish.SetActive(true);
	}
}
