using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredients : MonoBehaviour
{
    [SerializeField] private ScriptableIngredients scriptableIngredients;

    private IIngredientParents ingredientParent;
    
    public ScriptableIngredients GetScriptableIngredients()
    {
        return scriptableIngredients;
    }

    public void SetIngredientParent(IIngredientParents ingredientParent)
    {
        if(this.ingredientParent != null)
        {
            this.ingredientParent.ClearIngredient();
        }

        this.ingredientParent = ingredientParent;

        if (ingredientParent.HasIngredient())
        {
            Debug.LogError("Parent Already has Ingredient");
        }

        ingredientParent.SetIngredient(this);

        transform.parent = ingredientParent.GetIngredientFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public IIngredientParents GetIngredientParent()
    {
        return ingredientParent;
    }

    public void DestroySelf()
    {
        ingredientParent.ClearIngredient();
        Destroy(gameObject);
    }

    public bool TryGetPlate(out PlateIngredient plateIngredient)
    {
        if (this is PlateIngredient)
        {
            plateIngredient = this as PlateIngredient;
            return true;
        }
        else
        {
            plateIngredient = null;
            return false;
        }
    }

    public static Ingredients SpawnIngredients(ScriptableIngredients scriptableIngredients, IIngredientParents ingredientParents)
    {
        Transform ingredientsTransform = Instantiate(scriptableIngredients.prefab);
        Ingredients ingredients = ingredientsTransform.GetComponent<Ingredients>(); 
        ingredients.SetIngredientParent(ingredientParents);

        return ingredients;
    }
}