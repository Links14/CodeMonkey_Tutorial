using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player : MonoBehaviour
{
    Vector3 inputVector = new Vector3(0, 0, 0);
    [SerializeField] private float moveSpeed = 5f;

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
        inputVector = inputVector.normalized * moveSpeed * Time.deltaTime;

        transform.position += inputVector; // move the player in the direction of inputVector

        Debug.Log("DeltaTime: " + Time.deltaTime);
        Debug.Log(inputVector);
    }
}
