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

	public bool levelEnd = false;
	public int MintimeToNextTask = 5;
	public int MaxtimeToNextTask = 10;

	private void Start()
	{
		levelEnd = false;
		StartCoroutine(TaskLoop());
	}
	private IEnumerator TaskLoop()
	{
		//first task - game start
		yield return new WaitForSeconds(1f);
		StartCoroutine(GiveTask());


		//loop
		while (!levelEnd)
		{
			yield return new WaitForSeconds(1f);
			if (TaskList.Count < 5 && !levelEnd)
			{
				int cooldown = Random.Range(MintimeToNextTask, MaxtimeToNextTask);
				yield return new WaitForSeconds(cooldown);

				if(!levelEnd)
					StartCoroutine(GiveTask());
			}
		}
	}

	private IEnumerator GiveTask()
	{
		//WYLOSUJ RECEPTURE
		int RandomRecipe = Random.Range(0, LevelRecipes.Count);
		Recipes recipe = LevelRecipes[RandomRecipe];
		//-----------------



		//RECEPTURA NA CANVE
		float tick = 0.3f;
		GameObject taskListCanva = GameObject.Find("tasks");
		LeanTween.moveY(taskListCanva, taskListCanva.transform.position.y + 200f, tick);
		yield return new WaitForSeconds(tick);
		GameObject Task = Instantiate(TaskObj, TaskListCanva.transform.position, Quaternion.identity, TaskListCanva.transform);
		LeanTween.moveY(taskListCanva, taskListCanva.transform.position.y - 200f, tick);
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

	public void EndLevel()
    {
		for(int i=0; i<TaskList.Count; i++)
		{
            DestroyTask(TaskList[i]);
        }
    }
}
