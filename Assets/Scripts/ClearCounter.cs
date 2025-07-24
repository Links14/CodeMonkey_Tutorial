using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private ClearCounter secondClearCounter;
    [SerializeField] private bool testing;

    private KitchenObject kitchenObject;


    private void Update()
    {
        if (testing && Input.GetKeyDown(KeyCode.T))
        {
            if (kitchenObject != null)
            {
                kitchenObject.SetClearCounter(secondClearCounter); // Set the ClearCounter of the KitchenObject to the second ClearCounter
            }
        }
    }


    public void Interact()
    {
        if (kitchenObject == null)
        {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint); // Instantiate a tomato prefab at the counter top point
            kitchenObjectTransform.GetComponent<KitchenObject>().SetClearCounter(this); // Get the KitchenObject component from the instantiated prefab
        }
        else
        {
            Debug.Log(kitchenObject.GetClearCounter());
        }
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint; // Return the transform of the counter top point where the KitchenObject should follow
    }

    public void SetKitchenObject(KitchenObject _kitchenObject)
    {
        this.kitchenObject = _kitchenObject; // Set the KitchenObject associated with this ClearCounter
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject; // Return the KitchenObject associated with this ClearCounter
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null; // Clear the KitchenObject associated with this ClearCounter
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null; // Check if this ClearCounter has a KitchenObject associated with it
    }
}
