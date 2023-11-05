using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class TrashCounter : BaseCounter
{
    public static event EventHandler OnAnyObjectTrashed;

    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            player.GetKitchenObject().DestroySelf();
            OnAnyObjectTrashed?.Invoke(this, EventArgs.Empty);
        }
    }
    public override void InteractAlternate(Player player)
    {

    }
}
