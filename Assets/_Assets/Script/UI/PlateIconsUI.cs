using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconsUI : MonoBehaviour
{
    [SerializeField] private PlateIngredient plateIngredient;
    [SerializeField] private Transform iconTemplate;

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        plateIngredient.OnIngredientAdded += PlateIngredient_OnIngredientAdded;
    }

    private void PlateIngredient_OnIngredientAdded(object sender, PlateIngredient.OnIngredientAddedeventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach(Transform child in transform)
        {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach(ScriptableIngredients scriptableIngredients in plateIngredient.GetScriptableIngredientsList())
        {
            Transform iconTransform = Instantiate(iconTemplate, transform);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<PlateIconSingleUI>().SetScriptableIngredient(scriptableIngredients);
        }
    }
}
