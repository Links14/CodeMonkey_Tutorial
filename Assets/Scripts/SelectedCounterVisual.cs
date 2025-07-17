using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private ClearCounter clearCounter;
    [SerializeField] private GameObject visualGameObject;

    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged; // Subscribe to the event when the selected counter changes
    }

    private void Player_OnSelectedCounterChanged(object _sender, Player.OnSelectedCounterChangedEventArgs _e)
    {
        // If a counter is selected, enable the visual; otherwise, disable it
        visualGameObject.SetActive(_e.selectedCounter != null);

        // If the selected counter matches this instance, show the visual
        if (_e.selectedCounter != null && _e.selectedCounter == clearCounter)
        {
            Show();
        }
        else
        {
            Hide(); // Hide if no counter is selected or is the wrong counter
        }
    }

    private void Show()
    {
        visualGameObject.SetActive(true); // Show the visual GameObject
    }
    private void Hide()
    {
        visualGameObject.SetActive(false); // Hide the visual GameObject
    }
}
