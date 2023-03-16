using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IIngredientParents
{
    public Transform GetIngredientFollowTransform();

    public void SetIngredient(Ingredients ingredients);

    public Ingredients GetIngredient();

    public void ClearIngredient();

    public bool HasIngredient();
    
}
