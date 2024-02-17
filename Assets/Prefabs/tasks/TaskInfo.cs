using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskInfo : MonoBehaviour
{
    public TextMeshProUGUI DishName;
    public Image DishIcon;
    public Slider TimeSlider;
    public Recipes recipe;


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

		LeanTween.scale(this.gameObject, new Vector3(1, 1, 1), 1f);
		LeanTween.rotate(this.gameObject, new Vector3(180, 180, 180), 1f);

		yield return new WaitForSeconds(1f);

		while (remainingTime > 0)
		{
			TimeSlider.value = remainingTime;
			yield return new WaitForSeconds(0.05f);
			remainingTime -= 0.05f;
		}
		GameObject.FindGameObjectWithTag("manager").GetComponent<Tasks>().TaskList.Remove(this);

		Destroy(gameObject);
	}
}
