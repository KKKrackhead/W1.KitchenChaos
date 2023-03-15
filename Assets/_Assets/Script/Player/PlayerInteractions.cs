using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    public static PlayerInteractions Instance { get; private set; }

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public ClearCounter selectedCounter;
    } 

    [SerializeField] private GameInput gameInput;
    private Vector3 lastDirection;

    private ClearCounter selectedCounter;

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
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if (selectedCounter != null) selectedCounter.Interact();
    }

    private void Update()
    {
        Vector2 inputVector = gameInput.GetMovementVector();

        Vector3 movedirection = new Vector3(inputVector.x, 0f, inputVector.y);

        if (movedirection != Vector3.zero) lastDirection = movedirection;

        float InteractDistance = 2f;
        if (Physics.Raycast(transform.position, lastDirection, out RaycastHit raycastHit, InteractDistance))
        {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                if (clearCounter != selectedCounter)
                {
                    selectedCounter = clearCounter;

                    OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
                    {
                        selectedCounter = selectedCounter
                    });
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


    private void SetSelectedCounter(ClearCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter
        });
    }
}
