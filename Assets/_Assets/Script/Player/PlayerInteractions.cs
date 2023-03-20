using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour, IIngredientParents
{
    public static PlayerInteractions Instance { get; private set; }

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    } 

    [SerializeField] private GameInput gameInput;
    [SerializeField] private Transform ingredientHoldPoint;

    private Vector3 lastDirection;

    private Ingredients ingredients;
    private BaseCounter selectedCounter;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("More than 1 player instance");
        }
        Instance = this;
    }

    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
    }

    private void GameInput_OnInteractAlternateAction(object sender, EventArgs e)
    {
        if (selectedCounter != null) selectedCounter.InteractAlternate(this);
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if (selectedCounter != null) selectedCounter.Interact(this);
    }

    private void Update()
    {
        Vector2 inputVector = gameInput.GetMovementVector();

        Vector3 movedirection = new Vector3(inputVector.x, 0f, inputVector.y);

        if (movedirection != Vector3.zero) lastDirection = movedirection;

        float InteractDistance = 2f;
        if (Physics.Raycast(transform.position, lastDirection, out RaycastHit raycastHit, InteractDistance))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                if (baseCounter != selectedCounter)
                {
                    SetSelectedCounter(baseCounter);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else 
        {
            SetSelectedCounter(null);
        }
    }

    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter
        });
    }

    public Transform GetIngredientFollowTransform()
    {
        return ingredientHoldPoint;
    }

    public void SetIngredient(Ingredients ingredients)
    {
        this.ingredients = ingredients;
    }

    public Ingredients GetIngredient()
    {
        return ingredients;
    }

    public void ClearIngredient()
    {
        ingredients = null;
    }

    public bool HasIngredient()
    {
        return ingredients != null;
    }
}
