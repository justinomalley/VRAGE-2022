using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class RawXRInput : MonoBehaviour {
    private static UnityEvent leftTriggerPressedEvent { get; } = new UnityEvent();
    private static UnityEvent rightTriggerPressedEvent { get; } = new UnityEvent();
    
    private static UnityEvent leftTriggerUnpressedEvent { get; } = new UnityEvent();
    private static UnityEvent rightTriggerUnpressedEvent { get; } = new UnityEvent();
    
    
    private static UnityEvent leftThumbstickForwardEvent { get; } = new UnityEvent();
    private static UnityEvent rightThumbstickForwardEvent { get; } = new UnityEvent();
    
    private static UnityEvent leftThumbstickReleasedEvent { get; } = new UnityEvent();
    private static UnityEvent rightThumbstickReleasedEvent { get; } = new UnityEvent();
    
    private PlayerInput input;

    private void Awake() {
        
    }

    private void Update() {
        
    }

    public void Fire(InputAction.CallbackContext callback) {
        Debug.LogError("ayo");
    }
    

    // public void LeftTriggerPressed(InputAction.CallbackContext context) {
    //     if (!context.performed) {
    //         return;
    //     }
    //     
    //     Debug.Log("Left trigger pressed.");
    //     leftTriggerPressed;
    //     return;
    // }
    //
    // public void RightTriggerPressed(InputAction.CallbackContext context) {
    //     if (context.performed) {
    //         Debug.LogError("performed! " + Time.frameCount);
    //     } else if (context.started) {
    //         Debug.LogError("started! " + Time.frameCount);
    //     } else if (context.canceled){
    //         Debug.LogError("canceled! " + Time.frameCount);
    //     }
    //
    //     return;
    //     
    //     if (!context.performed) {
    //         return;
    //     }
    //     
    //     Debug.Log("Right trigger pressed.");
    //     rightTriggerPressed;
    //     return;
    // }
    //
    // public void LeftTriggerUnpressed(InputAction.CallbackContext context) {
    //     if (!context.performed) {
    //         return;
    //     }
    //     
    //     Debug.Log("Left trigger unpressed.");
    //     leftTriggerUnpressed;
    //     return;
    // }
    //
    // public void RightTriggerUnpressed(InputAction.CallbackContext context) {
    //     if (!context.performed) {
    //         return;
    //     }
    //     
    //     Debug.Log("Right trigger unpressed.");
    //     rightTriggerUnpressed;
    //     return;
    // }
}
