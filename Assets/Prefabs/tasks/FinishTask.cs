using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTask : MonoBehaviour
{
	public Tasks tasks;
	public int badScore = 50;
	public GameObject pariclePrefab;
	public GameObject badPariclePrefab;

	public int FinishedTasks = 0;
	public int BadTasks = 0;
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("item") && tasks.TaskList.Count > 0)
		{
			for(int i=0; i<tasks.TaskList.Count; i++)
			{
				if (other.GetComponent<ItemManager>().GetItemName() == tasks.TaskList[i].recipe.product.GetComponent<ItemManager>().GetItemName())
				{
					tasks.GetComponent<ScoreManager>().AddScore(tasks.TaskList[i].recipe.score);
					FinishedTasks++;
					tasks.DestroyTask(tasks.TaskList[i]);
					GameObject p = Instantiate(pariclePrefab, transform.position, Quaternion.identity);
					p.transform.position = other.transform.position;
					Destroy(p, 5);
					Destroy(other);
					return;
				}
			}
		}

		//gdy coœ nie tak
		tasks.GetComponent<ScoreManager>().AddScore(-badScore);
		BadTasks++;
		GameObject b = Instantiate(badPariclePrefab, transform.position, Quaternion.identity);
		b.transform.position = other.transform.position;
		Destroy(b, 5);
		Destroy(other);
		return;
	}
}
