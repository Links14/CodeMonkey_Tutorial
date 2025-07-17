using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; } // Singleton instance of Player

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged; // Event to notify when the selected counter changes
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public ClearCounter selectedCounter; // The currently selected ClearCounter
    }


    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotateSpeed = 10f;
    // max distance for interaction raycast
    [SerializeField] private float interactDistance = 2f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask; // LayerMask to filter raycast hits to only ClearCounter objects


    private bool isWalking = false;
    private Vector3 lastInteractDir;
    private ClearCounter selectedCounter; // reference to the ClearCounter component



    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogError("Multiple instances of Player detected! Destroying duplicate instance."); // log error if multiple Player instances are found
            Destroy(gameObject); // Ensure only one instance of Player exists
            return;
        }
        Instance = this; // Set the singleton instance
    }



    // Update is called once per frame
    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction; // subscribe to the interact action event

        if (countersLayerMask.value == 0)
        {
            Debug.LogError("Counters LayerMask is not set! Please assign a valid LayerMask for countersLayerMask in the Player script."); // error if countersLayerMask is not set
        }
    }

    private void GameInput_OnInteractAction(object _sender, EventArgs _e)
    {
        if (selectedCounter != null) // if a ClearCounter is selected
        {
            selectedCounter.Interact(); // call the Interact method on the selected ClearCounter
        }
    }

    public bool IsWalking()
    {
        return isWalking; // returns true if the player is moving
    }

    private void HandleInteractions()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized(); // get the normalized movement vector from GameInput

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y); // convert the 2D input vector to a 3D vector for movement

        if (moveDir != Vector3.zero) // if there is movement input
        {
            lastInteractDir = moveDir; // save the last interaction direction
        }
        else
        {
            moveDir = lastInteractDir; // use the last interaction direction if no movement input
        }

        // Data saved in variable raycastHit can be used to get info about what was hit
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))  // try to get the ClearCounter component from the object hit by the raycast
            {
                // has Clear counter
                if (clearCounter != selectedCounter)
                {
                    SetSelectedCounter(clearCounter); // set the selected ClearCounter and notify subscribers
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

    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized(); // get the normalized movement vector from GameInput

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y); // convert the 2D input vector to a 3D vector for movement

        float moveDistance = moveSpeed * Time.deltaTime; // distance to move this frame
        float playerRadius = .6f; // radius of the player for raycasting to avoid walls
        float playerHeight = 2f; // height of the player for raycasting to avoid walls
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        if (!canMove)
        {
            // Cannot move towards moveDir direction

            // Attempt only X movement
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = moveDir.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                // Cannot move on only X

                // Attempt only Z movement
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = moveDir.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);
                if (canMove)
                {
                    //Can move only on the X axis
                    moveDir = moveDirZ;
                }
                else
                {
                    // Cannot move in any direction
                }
            }
        }

        if (canMove)
        {
            transform.position += moveDir * moveSpeed * Time.deltaTime; // inputVector becomes a normalize vector reflecting the movement direction
        }

        // animator var
        isWalking = moveDir != Vector3.zero; // set isWalking to true if there is movement input

        if (isWalking) // only change rotation if there is movement input
        {
            transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed); // make the player face the direction of movement
        }
    }

    private void SetSelectedCounter(ClearCounter _selectedCounter)
    {
        this.selectedCounter = _selectedCounter; // set the selected ClearCounter

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs { selectedCounter = _selectedCounter }); // invoke the event to notify subscribers about the change
    }
}