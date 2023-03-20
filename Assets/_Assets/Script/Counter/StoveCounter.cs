using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;

    public class OnStateChangedEventArgs : EventArgs
    {
        public State state;
    }

    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned,
    }

    [SerializeField] private ScriptableFried[] scriptableFriedArray;
    [SerializeField] private ScriptableBurned[] scriptableBurnedArray;

    private float fryingTimer;
    private float burningTimer;
    private ScriptableFried scriptableFried;
    private ScriptableBurned scriptableBurned;
    private State state;


    private void Start()
    {
        state = State.Idle;
    }

    private void Update()
    {
        if (HasIngredient())
        {
            switch (state)
            {
                case State.Idle:
                    break;

                case State.Frying:
                    fryingTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = fryingTimer / scriptableFried.fryingTimerMax
                    });

                    if (fryingTimer > scriptableFried.fryingTimerMax)
                    {
                        GetIngredient().DestroySelf();
                        Ingredients.SpawnIngredients(scriptableFried.output, this);

                        state = State.Fried;
                        burningTimer = 0f;
                        scriptableBurned = GetBurnedWithInput(GetIngredient().GetScriptableIngredients());

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = state
                        });
                    }
                    break;

                case State.Fried:
                    burningTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = burningTimer / scriptableBurned.burningTimerMax
                    });

                    if (burningTimer > scriptableBurned.burningTimerMax)
                    {
                        GetIngredient().DestroySelf();
                        Ingredients.SpawnIngredients(scriptableBurned.output, this);

                        state = State.Burned;

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = state
                        });

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = 0f
                        });
                    }
                    break;

                case State.Burned:
                    break;
            }
        }
    }

    public override void Interact(PlayerInteractions player)
    {
        if (!HasIngredient())
        {
            if (player.HasIngredient())
            {
                if (HasRecipeWithInput(player.GetIngredient().GetScriptableIngredients()))
                {
                    player.GetIngredient().SetIngredientParent(this);
                    scriptableFried = GetFriedWithInput(GetIngredient().GetScriptableIngredients());

                    state = State.Frying;
                    fryingTimer = 0f;

                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        state = state
                    });

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = fryingTimer / scriptableFried.fryingTimerMax
                    });
                }
            }
            else { }
        }
        else
        {
            if (player.HasIngredient()) 
            {
                if (player.GetIngredient().TryGetPlate(out PlateIngredient plateIngredient))
                {
                    if (plateIngredient.TryAddIngredients(GetIngredient().GetScriptableIngredients()))
                    {
                        GetIngredient().DestroySelf();
                    }
                }

                state = State.Idle;

                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                {
                    state = state
                });

                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = 0f
                });

            }
            else
            {
                GetIngredient().SetIngredientParent(player);
                state = State.Idle;

                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                {
                    state = state
                });

                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = 0f
                });
            }
        }
    }

    private bool HasRecipeWithInput(ScriptableIngredients inputScriptableIngredients)
    {
        ScriptableFried scriptablefried = GetFriedWithInput(inputScriptableIngredients);
        return scriptablefried != null;
    }

    private ScriptableIngredients GetOutputForInput(ScriptableIngredients inputScriptableIngredients)
    {
        ScriptableFried scriptableFried = GetFriedWithInput(inputScriptableIngredients);

        if (scriptableFried != null) return scriptableFried.output;
        else return null;
    }

    private ScriptableFried GetFriedWithInput(ScriptableIngredients inputScriptableIngredients)
    {
        foreach (ScriptableFried scriptableFried in scriptableFriedArray)
        {
            if (scriptableFried.input == inputScriptableIngredients) return scriptableFried;
        }
        return null;
    }

    private ScriptableBurned GetBurnedWithInput(ScriptableIngredients inputScriptableIngredients)
    {
        foreach (ScriptableBurned scriptableBurned in scriptableBurnedArray)
        {
            if (scriptableBurned.input == inputScriptableIngredients) return scriptableBurned;
        }
        return null;
    }
}
