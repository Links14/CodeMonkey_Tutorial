using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions playerInputActions;

    private void Awake() { playerInputActions = new PlayerInputActions(); }
    private void OnEnable() { playerInputActions.Player.Enable(); }
    private void OnDisable() { playerInputActions.Player.Disable(); }
    private void OnDestroy() { playerInputActions.Dispose(); }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>(); // Get 2D vector from inputs
        
        // Normalization can be done through processors within the InputActions Asset
        inputVector = inputVector.normalized; // normalize the vector to ensure consistent movement speed in all directions

        return inputVector; // return a normalized vector reflecting the movement direction
    }
}
