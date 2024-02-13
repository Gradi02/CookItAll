using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipesManager : MonoBehaviour
{
    [SerializeField] private List<Recipes> allRecipes = new List<Recipes>();

    public List<Recipes> GetAllRecipes()
    {
        return allRecipes;
    }


    public Recipes CheckForRecipes(List<ItemScriptableObject> ing)
    {
        // Tworzymy s³ownik, aby zliczyæ wyst¹pienia ka¿dego sk³adnika w liœcie
        Dictionary<ItemScriptableObject, int> ingredientCount = new Dictionary<ItemScriptableObject, int>();
        foreach (ItemScriptableObject item in ing)
        {
            if (ingredientCount.ContainsKey(item))
            {
                ingredientCount[item]++;
            }
            else
            {
                ingredientCount[item] = 1;
            }
        }

        // Iterujemy przez wszystkie przepisy
        foreach (Recipes recipe in allRecipes)
        {
            if (ing.Count == recipe.ingredients.Count)
            {
                Dictionary<ItemScriptableObject, int> recipeIngredientCount = new Dictionary<ItemScriptableObject, int>();
                foreach (ItemScriptableObject item in recipe.ingredients)
                {
                    if (recipeIngredientCount.ContainsKey(item))
                    {
                        recipeIngredientCount[item]++;
                    }
                    else
                    {
                        recipeIngredientCount[item] = 1;
                    }
                }

                // Sprawdzamy, czy ka¿dy sk³adnik przepisu wystêpuje wystarczaj¹c¹ liczbê razy w liœcie sk³adników
                bool ingredientsMatch = true;
                foreach (var kvp in recipeIngredientCount)
                {
                    if (!ingredientCount.ContainsKey(kvp.Key) || ingredientCount[kvp.Key] < kvp.Value)
                    {
                        ingredientsMatch = false;
                        break;
                    }
                }

                // Jeœli wszystkie sk³adniki zgadzaj¹ siê, zwracamy przepis
                if (ingredientsMatch)
                {
                    return recipe;
                }
            }
        }

        // Jeœli nie znaleziono pasuj¹cego przepisu, zwracamy null
        return null;
    }
}
