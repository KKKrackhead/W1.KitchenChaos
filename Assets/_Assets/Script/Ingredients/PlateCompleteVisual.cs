using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct ScriptableIngredients_GameObject
    {
        public ScriptableIngredients scriptableIngredients;
        public GameObject gameObject;
    }

    [SerializeField] private PlateIngredient plateIngredient;
    [SerializeField] private List<ScriptableIngredients_GameObject> scriptableIngredientsGameObjectList;

    private void Start()
    {
        plateIngredient.OnIngredientAdded += PlateIngredient_OnIngredientAdded;

        foreach (ScriptableIngredients_GameObject scriptableIngredientsGameObject in scriptableIngredientsGameObjectList)
        {
            scriptableIngredientsGameObject.gameObject.SetActive(false);
        }

    }

    private void PlateIngredient_OnIngredientAdded(object sender, PlateIngredient.OnIngredientAddedeventArgs e)
    {
        foreach (ScriptableIngredients_GameObject scriptableIngredientsGameObject in scriptableIngredientsGameObjectList)
        {
            if(scriptableIngredientsGameObject.scriptableIngredients == e.scriptableIngredients) {
                scriptableIngredientsGameObject.gameObject.SetActive(true);
            }
        }
    }
}
