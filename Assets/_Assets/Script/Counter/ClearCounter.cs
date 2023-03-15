using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{

    [SerializeField] private ScriptableIngredients ScriptableIngredients;
    [SerializeField] private Transform counterTopPoint;

    private Ingredients ingredients;

    public void Interact()
    {
        if (ingredients == null)
        {
            Transform ingredientsTransform = Instantiate(ScriptableIngredients.prefab, counterTopPoint);
            ingredientsTransform.localPosition = Vector3.zero;

            ingredients = ingredientsTransform.GetComponent<Ingredients>();
        }
        
    }
}
