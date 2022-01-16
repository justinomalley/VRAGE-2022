using UnityEngine;

public class GenericXRController : MonoBehaviour {

    private GenericXRButton trigger;
    private GenericThumbstickForwardTracker thumbstickTracker;
    
    private void Awake() {
        trigger = GetComponent<GenericXRButton>();
        trigger.AddButtonPressedListener(() => {
            Debug.Log("button pressed! " + gameObject.name);
        });
        trigger.AddButtonUnpressedListener(() => {
            Debug.Log("button unpressed! " + gameObject.name);
        });

        thumbstickTracker = GetComponent<GenericThumbstickForwardTracker>();
        thumbstickTracker.AddThumbstickForwardListener(() => {
            Debug.Log("thumbstick forward! " + gameObject.name);
        });
        thumbstickTracker.AddThumbstickReleasedListener(() => {
            Debug.Log("thumbstick released! " + gameObject.name);
        });
    }
}
