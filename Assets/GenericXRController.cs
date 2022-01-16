using UnityEngine;

public class GenericXRController : MonoBehaviour {

    private GenericXRButton trigger;
    private GenericThumbstickForwardTracker thumbstickTracker;

    private bool triggerPressed, thumbstickForward;
    
    private void Awake() {
        trigger = GetComponent<GenericXRButton>();
        trigger.AddButtonPressedListener(() => {
            triggerPressed = true;
        });
        trigger.AddButtonUnpressedListener(() => {
            triggerPressed = false;
        });

        thumbstickTracker = GetComponent<GenericThumbstickForwardTracker>();
        thumbstickTracker.AddThumbstickForwardListener(() => {
            thumbstickForward = true;
        });
        thumbstickTracker.AddThumbstickReleasedListener(() => {
            thumbstickForward = false;
        });
    }
    
    public bool TriggerPressed() {
        return triggerPressed;
    }

    public bool ThumbstickForward() {
        return thumbstickForward;
    }
}
