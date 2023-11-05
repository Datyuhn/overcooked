using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    [SerializeField] AudioClipRefsSO audioClipRefsSO;
    private float volume = 1.2f;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += Delivery_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += Delivery_OnRecipeFailed;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        BaseCounter.OnAnyObjectPlacedHere += BaseCounter_OnAnyObjectPlacedHere;
        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
        Player.Instance.OnPickUp += Player_OnPickUp;
    }

    private void TrashCounter_OnAnyObjectTrashed(object sender, System.EventArgs e)
    {
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(audioClipRefsSO.trash, trashCounter.transform.position);
    }

    private void BaseCounter_OnAnyObjectPlacedHere(object sender, System.EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(audioClipRefsSO.drop, baseCounter.transform.position);
    }

    private void Player_OnPickUp(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.pickup, Player.Instance.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(audioClipRefsSO.chop, cuttingCounter.transform.position);
    }

    private void Delivery_OnRecipeFailed(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefsSO.deliveryFailure, deliveryCounter.transform.position);
    }

    private void Delivery_OnRecipeSuccess(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefsSO.deliverySuccess, deliveryCounter.transform.position);
    }
    private void PlaySound(AudioClip[] audioClipArray, Vector3 position)
    {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], position);
    }
    private void PlaySound(AudioClip audioClip, Vector3 position)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }
    public void PlayFootstepSound(Vector3 position)
    {
        PlaySound(audioClipRefsSO.footstep, position);
    }
}
