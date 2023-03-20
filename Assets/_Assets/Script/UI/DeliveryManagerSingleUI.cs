using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeNameText;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform iconTemplate;

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    public void SetScriptableRecipe(ScriptableRecipe scriptableRecipe)
    {
        recipeNameText.text = scriptableRecipe.recipeName;

        foreach(Transform child in iconContainer)
        {
            if (child == iconTemplate) continue;
            else Destroy(child.gameObject);
        }

        foreach (ScriptableIngredients scriptableIngredients in scriptableRecipe.scriptableIngredients)
        {
            Transform iconTransform = Instantiate(iconTemplate, iconContainer);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<Image>().sprite = scriptableIngredients.sprite;
        }
    }
}
