using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class DeliveryCounter : BaseCounter
{
    public static DeliveryCounter Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject platteKitchenObject))
            {
                // Only accepts Plates
                DeliveryManager.Instance.DeliverRecipe(platteKitchenObject);
                player.GetKitchenObject().DestroySelf();
            }
        }
    }
    public override void InteractAlternate(Player player)
    {

    }
}