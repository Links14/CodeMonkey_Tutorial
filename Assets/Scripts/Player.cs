using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player : MonoBehaviour
{
    Vector3 inputVector = new Vector3(0, 0, 0);
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotateSpeed = 10f;
    private bool isWalking = false;

    // Update is called once per frame
    private void Update()
    {
        inputVector = new Vector3(0, 0, 0); // reset inputVector each frame

        if (Input.GetKey(KeyCode.W)) // move up
        {
            inputVector.z = 1;
        }
        if (Input.GetKey(KeyCode.S)) // move down
        {
            inputVector.z = -1;
        }
        if (Input.GetKey(KeyCode.A)) // move left
        {
            inputVector.x = -1;
        }
        if (Input.GetKey(KeyCode.D)) // move right
        {
            inputVector.x = 1;
        }
        inputVector = inputVector.normalized * moveSpeed * Time.deltaTime; // inputVector becomes a normalize vector reflecting the movement direction

        transform.position += inputVector; // move the player in the direction of inputVector
        if (inputVector != Vector3.zero) // only change rotation if there is movement input
        {
            transform.forward = Vector3.Slerp(transform.forward, inputVector, Time.deltaTime * rotateSpeed); // make the player face the direction of movement
        }

        // animator var
        isWalking = inputVector != Vector3.zero; // set isWalking to true if there is movement input
    }

    public bool IsWalking()
    {
        return isWalking; // returns true if the player is moving
    }
}
