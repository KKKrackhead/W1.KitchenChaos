using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;

    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private ScriptableRecipeList scriptableRecipeList;

    private List<ScriptableRecipe> waitingScriptableRecipeList;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipeMax = 4;

    private void Awake()
    {
        Instance = this;

        waitingScriptableRecipeList = new List<ScriptableRecipe>();
    }

    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if(spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;

            if(waitingScriptableRecipeList.Count < waitingRecipeMax)
            {
                ScriptableRecipe scriptableWaitingRecipe = scriptableRecipeList.scriptableRecipesList[UnityEngine.Random.Range(0, scriptableRecipeList.scriptableRecipesList.Count)];
                waitingScriptableRecipeList.Add(scriptableWaitingRecipe);

                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void DeliverRecipe(PlateIngredient plateIngredient)
    {
        for (int i = 0; i < waitingScriptableRecipeList.Count; i++)
        {
            ScriptableRecipe scriptableWaitingRecipe = waitingScriptableRecipeList[i];

            if(scriptableWaitingRecipe.scriptableIngredients.Count == plateIngredient.GetScriptableIngredientsList().Count)
            {
                bool plateContentMatchesRecipe = true;
                foreach (ScriptableIngredients scriptableIngredientsRecipe in scriptableWaitingRecipe.scriptableIngredients)
                {
                    bool ingredientFound = false;

                    foreach (ScriptableIngredients scriptablePlateIngredient in plateIngredient.GetScriptableIngredientsList())
                    {
                        if(scriptablePlateIngredient == scriptableIngredientsRecipe)
                        {
                            ingredientFound = true;
                            break;
                        }
                    }
                    if (!ingredientFound)
                    {
                        plateContentMatchesRecipe = false;
                    }
                }
                if (plateContentMatchesRecipe)
                {
                    waitingScriptableRecipeList.RemoveAt(i);
                    
                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }
    }

    public List<ScriptableRecipe> GetWaitingScriptableRecipeList()
    {
        return waitingScriptableRecipeList;
    }
} 
