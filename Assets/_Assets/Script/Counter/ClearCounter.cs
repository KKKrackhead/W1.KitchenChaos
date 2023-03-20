using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private ScriptableFried ScriptableIngredients;

    public override void Interact(PlayerInteractions player)
    {
        if (!HasIngredient())
        {
            if (player.HasIngredient()) player.GetIngredient().SetIngredientParent(this);
            else{}
        }
        else
        {
            if (player.HasIngredient()) 
            {
                if(player.GetIngredient().TryGetPlate(out PlateIngredient plateIngredient))
                {
                    if (plateIngredient.TryAddIngredients(GetIngredient().GetScriptableIngredients()))
                    {
                        GetIngredient().DestroySelf();
                    }
                }
                else
                {
                    if (GetIngredient().TryGetPlate(out  plateIngredient))
                    {
                        if (plateIngredient.TryAddIngredients(player.GetIngredient().GetScriptableIngredients()))
                        {
                            player.GetIngredient().DestroySelf();

                        }
                    }
                }
            }
            else GetIngredient().SetIngredientParent(player);
        }
    }
}
