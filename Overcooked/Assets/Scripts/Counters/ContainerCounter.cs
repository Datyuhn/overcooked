using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    public event EventHandler OnPlayerGrabbedObject;

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            KitchenObjects.SpawnKitchenObject(kitchenObjectSO, player);

            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
    }
    public override void InteractAlternate(Player player)
    {

    }
}
