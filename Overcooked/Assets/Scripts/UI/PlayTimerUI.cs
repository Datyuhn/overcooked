using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayTimerUI : MonoBehaviour
{
    [SerializeField] private Image timerImage;
    [SerializeField] private PlayTimerUI playTimerUI;
    private void Start()
    {
        Hide();
        KitchenGameManager.Instance.OnStateChanged += PlayTimer_OnStateChanged;
    }

    private void PlayTimer_OnStateChanged(object sender, System.EventArgs e)
    {
        if (KitchenGameManager.Instance.IsGamePlaying())
        {
            Show();
        } else
        {
            Hide();
        }
    }
    private void Update()
    {
        timerImage.fillAmount = KitchenGameManager.Instance.GetGamePlayingTimerNormalized();
    }
    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
