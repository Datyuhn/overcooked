using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    private Player player;
    private float footStep;
    [SerializeField] private float footStepMax = 0.3f;

    private void Awake()
    {
        player = GetComponent<Player>();
    }
    private void Update()
    {
        footStep -= Time.deltaTime;
        if (footStep < 0f)
        {
            footStep = footStepMax;
            if (player.IsWalking()) SoundManager.Instance.PlayFootstepSound(player.transform.position);
        }
    }
}
