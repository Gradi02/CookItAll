using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Recipe", menuName = "new Recipe", order = 1)]
public class Recipes : ScriptableObject
{
	public GameObject product;
	public List<ItemScriptableObject> ingredients = new();
	public float timeToCook;
}
