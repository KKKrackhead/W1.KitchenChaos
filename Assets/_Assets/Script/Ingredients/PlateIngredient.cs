using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIngredient : Ingredients
{
    public event EventHandler<OnIngredientAddedeventArgs> OnIngredientAdded;
    public class OnIngredientAddedeventArgs : EventArgs
    {
        public ScriptableIngredients scriptableIngredients;
    }

    [SerializeField] private List<ScriptableIngredients> validScriptableIngredients;

    private List<ScriptableIngredients> scriptableIngredientsList;

    private void Awake()
    {
        scriptableIngredientsList = new List<ScriptableIngredients>();
    }

    public bool TryAddIngredients(ScriptableIngredients scriptableIngredients)
    {
        if (!validScriptableIngredients.Contains(scriptableIngredients))
        {
            return false;
        }

        if (scriptableIngredientsList.Contains(scriptableIngredients))
        {
            return false;
        }
        else
        {
            scriptableIngredientsList.Add(scriptableIngredients);

            OnIngredientAdded?.Invoke(this, new OnIngredientAddedeventArgs
            {
                scriptableIngredients = scriptableIngredients
            }); ;

            return true;
        }
    }

    public List<ScriptableIngredients> GetScriptableIngredientsList()
    {
        return scriptableIngredientsList;
    }

}
