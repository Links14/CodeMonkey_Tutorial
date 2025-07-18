using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO; // Reference to the ScriptableObject that holds data about this kitchen object

    public KitchenObjectSO GetKitchenObjectSO() // Method to get the ScriptableObject associated with this kitchen object
    {
        return kitchenObjectSO;
    }
}
