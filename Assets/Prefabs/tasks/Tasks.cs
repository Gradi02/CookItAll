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
				int cooldown = Random.Range(2, 8);
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



		//RECEPTURA NA CANVE
		GameObject Task = Instantiate(TaskObj, TaskListCanva.transform.position, Quaternion.identity, TaskListCanva.transform);
		TaskInfo T = Task.GetComponent<TaskInfo>();

		if (recipe != null)
		{
			T.recipe = recipe;
		}

		TaskList.Add(T);
		Debug.Log(TaskList.Count);

		//StartCoroutine(TimeToDestroy(TimeToCook, T));
	}


	public void DestroyTask(TaskInfo TI)
	{
		//StopCoroutine(TimeToDestroy(0, null));
		TaskList.Remove(TI);
		LeanTween.moveY(TI.gameObject, (TI.gameObject.transform.position.y + 250), 0.5f).setEase(LeanTweenType.easeInExpo);
		Destroy(TI.gameObject, 0.6f);
	}
}
