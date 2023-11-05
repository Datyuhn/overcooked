using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static IHasProgress;
//using static CuttingCounter;

public class StoveCounter : BaseCounter, IHasProgress
{
    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned
    }

    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs : EventArgs
    {
        public State state;
    }

    private State state;
    [SerializeField] private FryRecipeSO[] fryRecipeSOArray;
    [SerializeField] private BurnRecipeSO[] burnRecipeSOArray;
    private float fryTime, burnTime;
    private FryRecipeSO fryRecipeSO;
    private BurnRecipeSO burnRecipeSO;

    public override void InteractAlternate(Player player)
    {

    }
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
                    fryRecipeSO = GetFryRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    state = State.Frying;
                    fryTime = 0f;

                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        state = state
                    });
                    ProgressBarAnimation(fryTime / fryRecipeSO.fryTimeMax);
                }
            }
        }
        else //If it has object
        {
            if (player.HasKitchenObject()) // is player caring something
            {
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) // is player holding the plate
                {
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                        state = State.Idle;

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = state
                        });

                        ProgressBarAnimation(0f);
                    }
                }
            }
            //If player don't bring anything
            else 
            {
                GetKitchenObject().SetKitchenObjectParent(player);
                state = State.Idle;

                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                {
                    state = state
                });

                ProgressBarAnimation(0f);
            }
        }
    }
    //private KitchenObjectSO GetOutputFromInput(KitchenObjectSO inputKitchenObjectSO)
    //{
    //    FryRecipeSO fryRecipeSO = GetFryRecipeSOWithInput(inputKitchenObjectSO);
    //    if (fryRecipeSO != null)
    //    {
    //        return fryRecipeSO.output;
    //    }
    //    else return null;
    //}
    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryRecipeSO fryRecipeSO = GetFryRecipeSOWithInput(inputKitchenObjectSO);
        return fryRecipeSO != null;
    }
    private FryRecipeSO GetFryRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (FryRecipeSO item in fryRecipeSOArray)
        {
            if (item.input == inputKitchenObjectSO)
            {
                return item;
            }
        }
        return null;
    }
    private BurnRecipeSO GetBurnRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (BurnRecipeSO item in burnRecipeSOArray)
        {
            if (item.input == inputKitchenObjectSO)
            {
                return item;
            }
        }
        return null;
    }
    private void ProgressBarAnimation(float timer)
    {
        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
        {
            progressNormalized = timer
        });
    }
    private void Start()
    {
        state = State.Idle;
    }
    private void Update()
    {
        if (HasKitchenObject())
        {
            switch (state)
            {
                case State.Idle:
                    break;
                case State.Frying:
                    fryTime += Time.deltaTime;
                    ProgressBarAnimation(fryTime / fryRecipeSO.fryTimeMax);

                    if (fryTime > fryRecipeSO.fryTimeMax)
                    {
                        GetKitchenObject().DestroySelf();
                        KitchenObjects.SpawnKitchenObject(fryRecipeSO.output, this);
                        burnRecipeSO = GetBurnRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                        burnTime = 0f;
                        state = State.Fried;

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = state
                        });
                    }
                    
                    break;
                case State.Fried:
                    burnTime += Time.deltaTime;
                    ProgressBarAnimation(burnTime / burnRecipeSO.burnTimeMax);

                    if (burnTime > burnRecipeSO.burnTimeMax)
                    {
                        GetKitchenObject().DestroySelf();
                        KitchenObjects.SpawnKitchenObject(burnRecipeSO.output, this);
                        state = State.Burned;

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = state
                        });
                        ProgressBarAnimation(0f);
                    }
                    break;
                case State.Burned:
                    break;
                default:
                    break;
            }
        }
    }
}
