using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObjects : MonoBehaviour
{
    [SerializeField] private KitchenObjectSpawn kitchenObjectSpawn;
    private ClearCounter clearCounter;
    public KitchenObjectSpawn GetKitchenObject()
    {
        return kitchenObjectSpawn;
    }
    public void SetClearCounter(ClearCounter clearCounter)
    {
        Debug.Log(this.clearCounter);
        if (this.clearCounter != null)
        {
            this.clearCounter.ClearKitchenObject();
        }
        this.clearCounter = clearCounter;
        if (clearCounter.HasKitchenObject())
        {
            Debug.LogError("Counter already has object");
        }
        
        clearCounter.SetKitchenObject(this);

        transform.parent = clearCounter.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;

        Debug.Log(this.clearCounter);
    }
    public ClearCounter GetClearCounter()
    {
        return clearCounter;
    }
}
