using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ScriptableBurned : ScriptableObject
{
    public ScriptableIngredients input;
    public ScriptableIngredients output;

    public float burningTimerMax;
}
