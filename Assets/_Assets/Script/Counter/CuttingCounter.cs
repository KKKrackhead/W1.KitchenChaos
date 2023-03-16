using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    public class OnProgressChangedEventArgs : EventArgs
    {
        public float progressNormalized;
    }

    [SerializeField] private ScriptableCutIngredients[] scriptableCutIngredientsArray;

    private int cuttingProgress;

    public override void Interact(PlayerInteractions player)
    {
        if (!HasIngredient())
        {
            if (player.HasIngredient())
            {
                if (HasRecipeWithInput(player.GetIngredient().GetScriptableIngredients()))
                {
                    player.GetIngredient().SetIngredientParent(this);
                    cuttingProgress = 0;

                    ScriptableCutIngredients scriptableCutIngredients = GetCutIngredientsWithInput(GetIngredient().GetScriptableIngredients());

                    OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs { 
                        progressNormalized = (float)cuttingProgress / scriptableCutIngredients.cuttingProgressMax 
                    });
                }
            }
            else { }
        }
        else
        {
            if (player.HasIngredient()) { }
            else GetIngredient().SetIngredientParent(player);
        }
    }

    public override void InteractAlternate(PlayerInteractions player)
    {
        if (HasIngredient() && HasRecipeWithInput(GetIngredient().GetScriptableIngredients()))
        {
            cuttingProgress++;

            ScriptableCutIngredients scriptableCutIngredients = GetCutIngredientsWithInput(GetIngredient().GetScriptableIngredients());

            OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
            {
                progressNormalized = (float)cuttingProgress / scriptableCutIngredients.cuttingProgressMax
            });

            if ( cuttingProgress >= scriptableCutIngredients.cuttingProgressMax)
            {
                ScriptableIngredients outputScriptableIngredient = GetOutputForInput(GetIngredient().GetScriptableIngredients());
                GetIngredient().DestroySelf();

                Ingredients.SpawnIngredients(outputScriptableIngredient, this);
            }
            
        }
    }

    private bool HasRecipeWithInput(ScriptableIngredients inputScriptableIngredients)
    {
        ScriptableCutIngredients scriptableCutIngredients = GetCutIngredientsWithInput(inputScriptableIngredients);
        return scriptableCutIngredients != null;
    }

    private ScriptableIngredients GetOutputForInput(ScriptableIngredients inputScriptableIngredients)
    {
        ScriptableCutIngredients scriptableCutIngredients = GetCutIngredientsWithInput(inputScriptableIngredients);

        if (scriptableCutIngredients != null) return scriptableCutIngredients.output;
        else return null;
    }

    private ScriptableCutIngredients GetCutIngredientsWithInput(ScriptableIngredients inputScriptableIngredients)
    {
        foreach (ScriptableCutIngredients scriptableCutIngredients in scriptableCutIngredientsArray)
        {
            if (scriptableCutIngredients.input == inputScriptableIngredients) return scriptableCutIngredients;
        }
        return null;
    }
}
