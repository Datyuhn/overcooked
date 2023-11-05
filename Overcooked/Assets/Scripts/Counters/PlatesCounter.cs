using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlatesCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;
    private float spawnPlateTimer;
    private float spawnPlateTimerMax = 4f;
    private int spawnAmount;
    private int spawnAmountMax = 5;

    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            if (spawnAmount > 0)
            {
                spawnAmount--;
                KitchenObjects.SpawnKitchenObject(plateKitchenObjectSO, player);
                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }
    public override void InteractAlternate(Player player)
    {

    }
    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;
        if (spawnPlateTimer > spawnPlateTimerMax )
        {
            //KitchenObjects.SpawnKitchenObject(plateKitchenObjectSO, this);
            spawnPlateTimer = 0f;
            if (spawnAmount < spawnAmountMax )
            {
                spawnAmount++;
                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
