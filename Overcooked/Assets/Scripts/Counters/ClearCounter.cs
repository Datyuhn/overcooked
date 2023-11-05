using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void InteractAlternate(Player player)
    {

    }
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
        }
        else // If having kitchen object
        {
            if (player.HasKitchenObject()) // Is player caring something
            {
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) // Is player holding the plate
                {
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                } 
                else // Player not hold the plate
                {
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject)) // Is plate on the counter
                    {
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }   
}
