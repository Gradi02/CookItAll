using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTask : MonoBehaviour
{
	private Tasks Tasks;
	private int temp = 0;

	private void Start()
	{
		Tasks = GameObject.FindGameObjectWithTag("manager").GetComponent<Tasks>();
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("item"))
		{
			Debug.Log(other.name);
			foreach( TaskInfo task in Tasks.TaskList)
			{
				if (other.GetComponent<ItemManager>().GetItemName() == task.recipe.product.GetComponent<ItemManager>().GetItemName())
				{
					Tasks.DestroyTask(task);
					temp++;
					Debug.Log(temp);
					Destroy(other);
				}
			}
		}
	}
}
