using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ScriptableCutIngredients : ScriptableObject
{
    public ScriptableIngredients input;
    public ScriptableIngredients output;

    public int cuttingProgressMax;
}
