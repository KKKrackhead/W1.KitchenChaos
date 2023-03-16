using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabObject;

    [SerializeField] private ScriptableIngredients scriptableIngredients;


    public override void Interact(PlayerInteractions player)
    {
        if (!player.HasIngredient())
        {
            Ingredients.SpawnIngredients(scriptableIngredients, player);

            OnPlayerGrabObject?.Invoke(this, EventArgs.Empty);
        }
            
    }
}
