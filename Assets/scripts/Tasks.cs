using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class Tasks : MonoBehaviour
{
	public GameObject TaskObj;
	public GameObject TaskListCanva;

	public List<TaskInfo> TaskList = new();
	private void Start()
	{
		GiveTask();
	}

	private void GiveTask()
	{
		GameObject Task = Instantiate(TaskObj, TaskListCanva.transform.position, Quaternion.identity, TaskListCanva.transform);
		Task.transform.localScale = Vector3.zero;
		TaskInfo T = Task.GetComponent<TaskInfo>();

		T.DishName.text= "MURZYNEK";											//TUTAJ USTAW NAZWE DANIA!
		T.DishIcon.GetComponent<Image>().sprite = null;							//TUTAJ USTAW IKONKE Z DANIA!

		int TimeToCook = 25;                                                    //TUTAJ USTAW CZAS TRWANIA TASKA!
		T.TimeSlider.GetComponent<Slider>().maxValue = TimeToCook;
		T.TimeSlider.GetComponent<Slider>().value = TimeToCook;

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
			t.TimeSlider.GetComponent<Slider>().value = remainingTime;
			yield return new WaitForSeconds(0.05f);
			remainingTime -= 0.05f;
		}
		TaskList.Remove(t);
		Debug.Log(TaskList.Count);

		Destroy(t.gameObject);

	}
}
