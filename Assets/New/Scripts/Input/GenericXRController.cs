using UnityEngine;

public class GenericXRController : MonoBehaviour, IXRController {

    private GenericXRButton trigger;
    private GenericThumbstickForwardTracker thumbstickTracker;

    private bool triggerPressed, thumbstickForward;

    [SerializeField]
    private Hand hand;
    
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

    public Vector3 Position() {
        return transform.position;
    }

    public Quaternion Rotation() {
        return transform.rotation;
    }

    public Hand Hand() {
        return hand;
    }
}
