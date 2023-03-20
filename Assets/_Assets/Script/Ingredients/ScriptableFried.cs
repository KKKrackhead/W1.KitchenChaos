using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ScriptableFried : ScriptableObject
{
    public ScriptableIngredients input;
    public ScriptableIngredients output;

    public float fryingTimerMax;
}
