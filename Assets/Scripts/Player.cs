using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotateSpeed = 10f;

    [SerializeField] private GameInput gameInput;


    private bool isWalking = false;

    // Update is called once per frame
    private void Update()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized(); // get the normalized movement vector from GameInput

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y); // convert the 2D input vector to a 3D vector for movement
        transform.position += moveDir * moveSpeed * Time.deltaTime; // inputVector becomes a normalize vector reflecting the movement direction

        // animator var
        isWalking = moveDir != Vector3.zero; // set isWalking to true if there is movement input

        if (isWalking) // only change rotation if there is movement input
        {
            transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed); // make the player face the direction of movement
        }

    }

    public bool IsWalking()
    {
        return isWalking; // returns true if the player is moving
    }
}