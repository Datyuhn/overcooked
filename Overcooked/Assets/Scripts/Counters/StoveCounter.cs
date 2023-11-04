using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CuttingCounter;

public class StoveCounter : BaseCounter
{
    [SerializeField] private FryRecipeSO[] fryRecipeSOArray;
    private float fryTime;
    private FryRecipeSO fryRecipeSO;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                //if player bring something that can be fried
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    fryTime = 0;

                    fryRecipeSO = GetFryRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                }
            }
        }
        else
        {
            if (!player.HasKitchenObject())
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
    private KitchenObjectSpawn GetOutputFromInput(KitchenObjectSpawn inputKitchenObjectSpawn)
    {
        FryRecipeSO fryRecipeSO = GetFryRecipeSOWithInput(inputKitchenObjectSpawn);
        if (fryRecipeSO != null)
        {
            return fryRecipeSO.output;
        }
        else return null;
    }
    private bool HasRecipeWithInput(KitchenObjectSpawn inputKitchenObjectSpawn)
    {
        FryRecipeSO fryRecipeSO = GetFryRecipeSOWithInput(inputKitchenObjectSpawn);
        return fryRecipeSO != null;
    }
    private FryRecipeSO GetFryRecipeSOWithInput(KitchenObjectSpawn inputKitchenObjectSpawn)
    {
        foreach (FryRecipeSO item in fryRecipeSOArray)
        {
            if (item.input == inputKitchenObjectSpawn)
            {
                return item;
            }
        }
        return null;
    }
    private void Update()
    {
        if (HasKitchenObject())
        {
            fryTime += Time.deltaTime;
            fryRecipeSO = GetFryRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
            if (fryTime > fryRecipeSO.fryTimeMax)
            {
                fryTime = 0f;
                GetKitchenObject().DestroySelf();
                KitchenObjects.SpawnKitchenObject(fryRecipeSO.output, this);
            }
            else { }
            Debug.Log(fryTime);
        }
    }
}
