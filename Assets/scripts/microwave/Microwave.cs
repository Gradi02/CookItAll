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
	private List<GameObject> itemsImages = new();
	public Transform grid;
	public GameObject imagePref;

	public ParticleSystem PS;
	public ParticleSystem badPS1;
	public ParticleSystem badPS2;

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
			else if(ItemsList.Count > 0)
			{
				StartCoroutine(BadRecipe()); 
			}
		}
	}

    private void OnTriggerEnter(Collider item)
	{
		// Sprawdzamy, czy obiekt, który wszed³ w obszar wyzwalacza, ma tag "Player".
		if (item.CompareTag("item") && usable == true && ItemsList.Count < 10)
		{
			ItemManager itemManager = item.GetComponent<ItemManager>();
			if (itemManager != null && itemManager.GetItem().cookable)
			{
				gameObject.GetComponent<Animator>().Play("microwave_product");
				ItemScriptableObject itm = itemManager.GetItem();
				ItemsList.Add(itm);

				GameObject i = Instantiate(imagePref, transform.position, Quaternion.identity, grid);
				itemsImages.Add(i);
				i.GetComponent<ItemMicro>().SetItem(itm);

				Destroy(item.gameObject);
			}
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
		DestroyCanvaItems();
		itemsImages.Clear();
		gameObject.GetComponent<Animator>().Play("microwave_open");
		gameObject.GetComponent<BoxCollider>().enabled = false;
		cooking = false;
	}

	private IEnumerator BadRecipe()
    {
		cooking = true;
		gameObject.GetComponent<Animator>().SetBool("cooking", true);
		PS.gameObject.SetActive(true);
		yield return new WaitForSeconds(0.5f);
		PS.gameObject.SetActive(false);
		badPS1.Play();
		badPS2.Play();

		ItemsList.Clear();
		DestroyCanvaItems();
		itemsImages.Clear();
		gameObject.GetComponent<Animator>().SetBool("cooking", false);
		gameObject.GetComponent<Animator>().Play("microwave_product");
		cooking = false;
	}

	void DestroyCanvaItems()
    {
		foreach(GameObject t in itemsImages)
        {
			Destroy(t);
        }
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
