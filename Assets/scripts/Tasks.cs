using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class Tasks : MonoBehaviour
{
	public GameObject TaskObj;
	public GameObject TaskListCanva;

	public List<TaskInfo> TaskList = new();


	public List<Recipes> LevelRecipes = new();
	private void Start()
	{
		StartCoroutine(TaskLoop());
	}
	private IEnumerator TaskLoop()
	{
		//first task - game start
		yield return new WaitForSeconds(1f);
		GiveTask();


		//loop
		while (true)
		{
			yield return new WaitForSeconds(1f);
			if (TaskList.Count < 5)
			{
				int cooldown = Random.Range(15, 40);
				yield return new WaitForSeconds(cooldown);
				GiveTask();
			}
		}
	}

	private void GiveTask()
	{
		//WYLOSUJ RECEPTURE
		int RandomRecipe = Random.Range(0, LevelRecipes.Count);
		Recipes recipe = LevelRecipes[RandomRecipe];
		//-----------------
		string RecipeName = recipe.product.name;
		Sprite RecipeIcon = null;
		int TimeToCook = 25;
		//-----------------



		//RECEPTURA NA CANVE
		GameObject Task = Instantiate(TaskObj, TaskListCanva.transform.position, Quaternion.identity, TaskListCanva.transform);
		Task.transform.localScale = Vector3.zero;
		TaskInfo T = Task.GetComponent<TaskInfo>();

		if (RecipeName != null)
		{
			T.DishName.text = RecipeName;
			T.recipe = recipe;
		}

		if (RecipeIcon != null) 
		{ 
			T.DishIcon.GetComponent<Image>().sprite = RecipeIcon; 
		}

		if (TimeToCook > 0)
		{
			T.TimeSlider.GetComponent<Slider>().maxValue = TimeToCook;
			T.TimeSlider.GetComponent<Slider>().value = TimeToCook;
		}

		TaskList.Add(T);
		Debug.Log(TaskList.Count);

		StartCoroutine(TimeToDestroy(TimeToCook, T));
	}

	private IEnumerator TimeToDestroy(int Timer, TaskInfo t)
	{
		float remainingTime = Timer;

		LeanTween.scale(t.gameObject, new Vector3(1, 1, 1), 1f);
		LeanTween.rotate(t.gameObject, new Vector3(180,180,180), 1f);

		yield return new WaitForSeconds(1f);

		while (remainingTime > 0)
		{
			if (t != null)
			{
				t.TimeSlider.GetComponent<Slider>().value = remainingTime;
				yield return new WaitForSeconds(0.05f);
				remainingTime -= 0.05f;
			}
		}
		TaskList.Remove(t);

		Destroy(t.gameObject);

	}

	public void DestroyTask(TaskInfo TI)
	{
		StopCoroutine(TimeToDestroy(0, null));
		TaskList.Remove(TI);
		Destroy(TI.gameObject);
	}
}
