using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredients : MonoBehaviour
{
    [SerializeField] private ScriptableIngredients scriptableIngredients;

    //private ClearCounter clearCounter;
    
    public ScriptableIngredients GetScriptableIngredients()
    {
        return scriptableIngredients;
    }

    /*public void SetClearCounter(ClearCounter clearCounter)
    {
        this.clearCounter = clearCounter;
    }

    public ClearCounter GetClearCounter()
    {
        return clearCounter;
    }*/
}