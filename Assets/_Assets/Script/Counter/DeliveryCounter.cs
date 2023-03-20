using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public override void Interact(PlayerInteractions player)
    {
        if (player.HasIngredient())
        {
            if(player.GetIngredient().TryGetPlate(out PlateIngredient plateIngredient))
            {
                DeliveryManager.Instance.DeliverRecipe(plateIngredient);

                player.GetIngredient().DestroySelf();
            }
        }
    }
}
