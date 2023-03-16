using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private ScriptableIngredients ScriptableIngredients;

    public override void Interact(PlayerInteractions player)
    {
        if (!HasIngredient())
        {
            if (player.HasIngredient()) player.GetIngredient().SetIngredientParent(this);
            else{}
        }
        else
        {
            if (player.HasIngredient()) { }
            else GetIngredient().SetIngredientParent(player);
        }
    }
}
