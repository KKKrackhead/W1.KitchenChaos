using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private ClearCounter clearCounter;
    [SerializeField] private GameObject visualGameObject;

    private void Start()
    {
        PlayerInteractions.Instance.OnSelectedCounterChanged += PlayerInteractions_OnSelectedCounterChanged;
    }

    private void PlayerInteractions_OnSelectedCounterChanged(object sender, PlayerInteractions.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == clearCounter) show();
        else hide();
    }

    private void show()
    {
        visualGameObject.SetActive(true);
    }

    private void hide()
    {
        visualGameObject.SetActive(false);
    }
}

    
