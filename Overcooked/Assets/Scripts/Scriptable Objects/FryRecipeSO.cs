using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class FryRecipeSO : ScriptableObject
{
    public KitchenObjectSO input, output;
    public float fryTimeMax;
}
