using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedVisualCounter : MonoBehaviour
{
    [SerializeField] private ClearCounter clearCounter;
    [SerializeField] private GameObject visualObject;
    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Instance_OnSelectedCounterChanged;
    }

    private void Instance_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedClearCounter == clearCounter) Show();
        else Hide();
    }

    private void Show()
    {
        visualObject.SetActive(true);
    }
    private void Hide()
    {
        visualObject.SetActive(false);
    }
}
