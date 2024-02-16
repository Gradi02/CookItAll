using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Microwave : MonoBehaviour, IknifeInteraction
{
	private RecipesManager rpmanager;
	private List<ItemScriptableObject> ItemsList = new();

	public ParticleSystem PS;

	public Transform dish;
	private GameObject ready;
	private bool usable = true;
	private bool cooking = false;

    private void Start()
    {
		rpmanager = GameObject.FindGameObjectWithTag("manager").GetComponent<RecipesManager>();
    }

	public void knifeInteract()
    {
		if (cooking == false)
		{
			Recipes rp = rpmanager.CheckForRecipes(ItemsList);

			if (rp != null)
			{
				StartCoroutine(CookingTime(rp.product));
			}
			else
			{
				Debug.Log("brak przepisu");
			}
		}
	}

    private void OnTriggerEnter(Collider item)
	{
		// Sprawdzamy, czy obiekt, który wszed³ w obszar wyzwalacza, ma tag "Player".
		if (item.CompareTag("item") && usable == true)
		{
			gameObject.GetComponent<Animator>().Play("microwave_product");
			ItemsList.Add(item.GetComponent<ItemManager>().GetItem());
			Destroy(item.gameObject);

			Debug.Log(ItemsList.Count);
		}
	}
	private IEnumerator CookingTime(GameObject product)
	{
		cooking = true;
		usable = false;
		gameObject.GetComponent<Animator>().SetBool("cooking", true);
		PS.gameObject.SetActive(true);
		yield return new WaitForSeconds(5f);
		PS.gameObject.SetActive(false);
		gameObject.GetComponent<Animator>().SetBool("cooking", false);
		ready = Instantiate(product, dish.position, Quaternion.identity);
		ItemsList.Clear();
		gameObject.GetComponent<Animator>().Play("microwave_open");
		gameObject.GetComponent<BoxCollider>().enabled = false;
		cooking = false;
	}

	private void Update()
	{
		if(ready == null && usable == false && cooking == false)
		{
			usable = true;
			gameObject.GetComponent<BoxCollider>().enabled = true;
			gameObject.GetComponent<Animator>().Play("microwave_close");
		}
	}

}
