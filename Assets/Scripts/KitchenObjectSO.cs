using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class KitchenObjectSO : ScriptableObject
{
    public Transform prefab; // The prefab for the kitchen object
    public Sprite sprite; // The sprite representing the kitchen object
    public string objectName; // The name of the kitchen object

}
