using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Microwave : MonoBehaviour
{
	private List<ItemScriptableObject> ItemsList = new();

	public List<GameObject> dishes = new();
	public GameObject dish;
	private GameObject ready;
	private bool usable = true;
	private bool cooking = false;

	private void OnTriggerEnter(Collider item)
	{
		// Sprawdzamy, czy obiekt, kt�ry wszed� w obszar wyzwalacza, ma tag "Player".
		if (item.CompareTag("item") && usable == true)
		{
			gameObject.GetComponent<Animator>().Play("microwave_product");
			ItemsList.Add(item.GetComponent<ItemManager>().GetItem());
			Destroy(item.gameObject);

			Debug.Log(ItemsList.Count);
		}

		if (item.CompareTag("knife") && cooking == false)
		{
			StartCoroutine(CookingTime());
		}
	}
	private IEnumerator CookingTime()
	{
		cooking = true;
		usable = false;
		gameObject.GetComponent<Animator>().SetBool("cooking", true);
		yield return new WaitForSeconds(5f);
		gameObject.GetComponent<Animator>().SetBool("cooking", false);
		ready = Instantiate(dishes[1], dish.transform.position, Quaternion.identity);
		ItemsList.Clear();
		gameObject.GetComponent<Animator>().Play("microwave_open");
		gameObject.GetComponent<SphereCollider>().enabled = false;
		cooking = false;
	}

	private void Update()
	{
		if(ready == null && usable == false && cooking == false)
		{
			usable = true;
			gameObject.GetComponent<SphereCollider>().enabled = true;
			gameObject.GetComponent<Animator>().Play("microwave_close");
		}
	}

}
