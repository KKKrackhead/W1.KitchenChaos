using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ScriptableRecipe : ScriptableObject
{
    public List<ScriptableIngredients> scriptableIngredients;
    public string recipeName;
}
