using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounter : BaseCounter
{
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;


    [SerializeField] private ScriptableIngredients plateScriptableIngredients;

    private float spawnPlateTimer;
    private float spawnPlateTimerMax = 4f;
    private int plateSpawnedAmount;
    private int plateSpawnedAmountMax = 4;

    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;
        if (spawnPlateTimer > spawnPlateTimerMax)
        {
            spawnPlateTimer = 0f;
            if (plateSpawnedAmount < plateSpawnedAmountMax)
            {
                plateSpawnedAmount++;
                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(PlayerInteractions player)
    {
        if (!player.HasIngredient())
        {
            if(plateSpawnedAmount > 0)
            {
                plateSpawnedAmount--;

                Ingredients.SpawnIngredients(plateScriptableIngredients, player);

                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
