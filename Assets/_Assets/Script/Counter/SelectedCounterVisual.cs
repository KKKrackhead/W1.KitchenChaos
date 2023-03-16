using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualGameObjectArray;

    private void Start()
    {
        PlayerInteractions.Instance.OnSelectedCounterChanged += PlayerInteractions_OnSelectedCounterChanged;
    }

    private void PlayerInteractions_OnSelectedCounterChanged(object sender, PlayerInteractions.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == baseCounter) show();
        else hide();
    }

    private void show()
    {
        foreach (GameObject visualGameObject in visualGameObjectArray)
        visualGameObject.SetActive(true);
    }

    private void hide()
    {
        foreach (GameObject visualGameObject in visualGameObjectArray)
        visualGameObject.SetActive(false);
    }
}

    
