using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO; // Reference to the ScriptableObject that holds data about this kitchen object

    private ClearCounter clearCounter;

    public KitchenObjectSO GetKitchenObjectSO() // Method to get the ScriptableObject associated with this kitchen object
    {
        return kitchenObjectSO;
    }

    public void SetClearCounter(ClearCounter _clearCounter)
    {
        if (this.clearCounter != null)
        {
            this.clearCounter.ClearKitchenObject(); // If this kitchen object already has a ClearCounter, clear its reference
        }

        this.clearCounter = _clearCounter; // Method to set the ClearCounter that this kitchen object is associated with

        if (clearCounter.HasKitchenObject())
        {
            Debug.LogError("ClearCounter already has a KitchenObject!"); // Log an error if the ClearCounter already has a kitchen object
        }
        _clearCounter.SetKitchenObject(this); // Set this kitchen object in the ClearCounter

        transform.parent = _clearCounter.GetKitchenObjectFollowTransform(); // Set the parent of this kitchen object to the ClearCounter's follow transform
        transform.localPosition = Vector3.zero; // Reset the local position to zero so it appears at the counter top
    }

    public ClearCounter GetClearCounter()
    {
        return clearCounter; // Method to get the ClearCounter associated with this kitchen object
    }
}
