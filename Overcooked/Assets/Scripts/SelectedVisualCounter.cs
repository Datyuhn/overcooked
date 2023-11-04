using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedVisualCounter : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualObject;
    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Instance_OnSelectedCounterChanged;
    }

    private void Instance_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedClearCounter == baseCounter) Show();
        else Hide();
    }

    private void Show()
    {
        foreach (GameObject item in visualObject)
        {
            item.SetActive(true);
        }
    }
    private void Hide()
    {
        foreach (GameObject item in visualObject)
        {
            item.SetActive(false);
        }
    }
}
