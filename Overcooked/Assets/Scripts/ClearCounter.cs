using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectSpawn kitchenObjectSpawn;
    [SerializeField] private Transform counterTopPoint;

    private KitchenObjects kitchenObject;
    [SerializeField] private ClearCounter secondClearCounter;
    public void Interact()
    {
        if (kitchenObject == null)
        {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSpawn.prefab, counterTopPoint);
            kitchenObjectTransform.GetComponent<KitchenObjects>().SetClearCounter(this);
        }
    }
    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }
    public void SetKitchenObject(KitchenObjects kitchenObject)
    {
        this.kitchenObject = kitchenObject;
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
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (kitchenObject != null)
            {
                kitchenObject.SetClearCounter(secondClearCounter);
            }
        }
    }
}
