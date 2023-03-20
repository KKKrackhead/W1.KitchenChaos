using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IIngredientParents
{
    [SerializeField] private Transform counterTopPoint;

    private Ingredients ingredients;

    public virtual void Interact(PlayerInteractions player)
    {
        Debug.LogError("BaseCounter.Interact();");
    }

    public virtual void InteractAlternate(PlayerInteractions player)
    {
        //Debug.LogError("BaseCounter.InteractAlternate();");
    }

    public Transform GetIngredientFollowTransform()
    {
        return counterTopPoint;
    }

    public void SetIngredient(Ingredients ingredients)
    {
        this.ingredients = ingredients;
    }

    public Ingredients GetIngredient()
    {
        return ingredients;
    }

    public void ClearIngredient()
    {
        ingredients = null;
    }

    public bool HasIngredient()
    {
        return ingredients != null;
    }
}
