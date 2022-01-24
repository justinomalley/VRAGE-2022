using UnityEngine;

/// <summary>
/// ElevatorCaller just watches for an input that signals the elevator to come back if it has disappeared
/// in the current gallery. If the elevator is already in it's original position, the call will be ignored.
/// </summary>
public class ElevatorCaller : MonoBehaviour {
    private VRAGEController[] controllers;

    private void Awake() {
        controllers = GetComponentsInChildren<VRAGEController>();
    }

    private void Update() {
        var callElevator = true;
        
        // Call the elevator back if both interact buttons are pressed during the same frame.
        for (var i = 0; i < controllers.Length; i++) {
            callElevator &= controllers[i].InteractButtonPressed();
        }

        if (callElevator) {
            Elevator.CallBack();
        }
    }
}
