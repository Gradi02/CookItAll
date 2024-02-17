using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTask : MonoBehaviour
{
	public Tasks Tasks;
	private int temp = 0;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("item") && Tasks.TaskList.Count > 0)
		{
			Debug.Log(other.name);

			for(int i=0; i<Tasks.TaskList.Count; i++)
			{
				if (other.GetComponent<ItemManager>().GetItemName() == Tasks.TaskList[i].recipe.product.GetComponent<ItemManager>().GetItemName())
				{
					Tasks.DestroyTask(Tasks.TaskList[i]);
					temp++;
					Debug.Log("wynik: "+temp);
					Destroy(other);
					return;
				}
			}
		}
	}
}
