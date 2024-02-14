using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class posterManager : MonoBehaviour
{
    [SerializeField] private Recipes[] recipes;
    [SerializeField] private GameObject recipePrefab;
    [SerializeField] private GameObject ingrPrefab;
    private int maxRecipes = 6;
    [SerializeField] private Transform recipes_group;
    Dictionary<ItemScriptableObject, int> ingredientsList = new Dictionary<ItemScriptableObject, int>();

    void Start()
    {
        ingredientsList.Clear();
        for(int i=0; i<recipes.Length; i++)
        {
            if(i < maxRecipes)
            {
                GameObject r = Instantiate(recipePrefab, transform.position, transform.rotation, recipes_group);
                r.transform.Find("ProductImage").GetComponent<Image>().sprite = recipes[i].product.GetComponent<ItemManager>().GetItem().item_image;
                Transform ingr = r.transform.Find("ing").transform;
                SumUpIngredients(recipes[i]);

                foreach (KeyValuePair<ItemScriptableObject, int> entry in ingredientsList)
                {
                    GameObject ingr1 = Instantiate(ingrPrefab, transform.position, transform.rotation, ingr);
                    ingr1.transform.Find("ingrImage").GetComponent<Image>().sprite = entry.Key.item_image;
                    ingr1.transform.Find("text").GetComponent<TextMeshProUGUI>().text = "x" + entry.Value;
                }
            }
            else
            {
                Debug.Log("Przekroczono limit przepisów na plakat, pominiêto przepis: " + recipes[i]);
            }

            ingredientsList.Clear();
        }
    }

    private void SumUpIngredients(Recipes rp)
    {
        foreach (ItemScriptableObject item in rp.ingredients)
        {
            if (ingredientsList.ContainsKey(item))
            {
                ingredientsList[item]++;
            }
            else
            {
                ingredientsList[item] = 1;
            }
        }
    }
}
