using UnityEngine;
using System;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    public static event EventHandler OnAnyObjectPlacedHere;
    [SerializeField] private Transform counterTopPoint;

    private KitchenObjects kitchenObject;
    public virtual void Interact(Player player)
    {
        Debug.LogError("BaseCounter.Interact()");
    }
    public virtual void InteractAlternate(Player player)
    {
        Debug.LogError("BaseCounter.InteractAlternate()");
    }
    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }
    public void SetKitchenObject(KitchenObjects kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        if (kitchenObject != null)
        {
            OnAnyObjectPlacedHere?.Invoke(this, EventArgs.Empty);
        }
    }
    public KitchenObjects GetKitchenObject()
    {
        return kitchenObject;
    }
    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }
    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
