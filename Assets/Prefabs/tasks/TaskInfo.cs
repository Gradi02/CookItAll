using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TaskInfo : MonoBehaviour
{
    public TextMeshProUGUI DishName;
    public UnityEngine.UI.Image DishIcon;
    public UnityEngine.UI.Slider TimeSlider;
	public UnityEngine.UI.Image fill;
	public Recipes recipe;
	private bool end = false;


    private void Start()
    {
		DishIcon.sprite = recipe.product.GetComponent<ItemManager>().GetIcon();
		DishName.text = recipe.product.GetComponent<ItemManager>().GetItemName();
		StartCoroutine(TimeToDestroy(recipe.timeToCook));
    }
	private IEnumerator TimeToDestroy(float Timer)
	{
		float remainingTime = Timer;
		TimeSlider.maxValue = remainingTime;
		while (remainingTime > 0)
		{
			TimeSlider.value = remainingTime;
			yield return new WaitForSeconds(0.05f);
			remainingTime -= 0.05f;

			if (remainingTime < 5f && end == false)
			{
				Debug.Log("KONIEC TASKA NEDLUGO");
				end = true;
				StartCoroutine(EndingTask());
			}
		}

		GameObject.FindGameObjectWithTag("manager").GetComponent<Tasks>().TaskList.Remove(this);
		LeanTween.moveY(this.gameObject, (this.gameObject.transform.position.y+250), 0.5f).setEase(LeanTweenType.easeInExpo);
		Destroy(gameObject, 0.6f);
	}

	private void Update()
	{
		GameObject taskListCanva = GameObject.Find("tasks");
		Debug.Log(taskListCanva.transform.localPosition.y);
	}

	private IEnumerator EndingTask()
	{
		while (true && this.gameObject != null)
		{
			float tick = 0.05f;
			LeanTween.rotateZ(this.gameObject, 2f, tick);
			yield return new WaitForSeconds(tick);
			LeanTween.rotateZ(this.gameObject, 0f, tick);
			yield return new WaitForSeconds(tick);
			LeanTween.rotateZ(this.gameObject, -2f, tick);
			yield return new WaitForSeconds(tick);
			LeanTween.rotateZ(this.gameObject, 0, tick);
			yield return new WaitForSeconds(tick);
		}
	}
}
