using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class BurnRecipeSO : ScriptableObject
{
    public KitchenObjectSO input, output;
    public float burnTimeMax;
}
