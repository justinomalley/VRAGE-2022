using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

/// <summary>
/// GenericThumbstickForwardTracker tracks whether a thumbstick is pushed forward,
/// firing an event when it is pressed forward or released.
/// </summary>
public class GenericThumbstickForwardTracker : MonoBehaviour {
    [SerializeField]
    private InputActionReference actionReference;

    private UnityEvent thumbstickForwardEvent { get; } = new UnityEvent();
    
    private UnityEvent thumbstickReleasedEvent { get; } = new UnityEvent();

    private bool thumbstickForward, lastFrameThumbstickForward;

    private const float ForwardThreshold = 0.75f;

    private void Update() {
        if (actionReference == null || actionReference.action == null) {
            return;
        }
        
        var value = actionReference.action.ReadValue<Vector2>().y;
        thumbstickForward = value >= ForwardThreshold;

        switch (thumbstickForward) {
            // If thumbstick was pushed forward this frame...
            case true when !lastFrameThumbstickForward:
                ThumbstickForward();
                break;
            // If thumbstick was released this frame...
            case false when lastFrameThumbstickForward:
                ThumbstickReleased();
                break;
        }

        lastFrameThumbstickForward = thumbstickForward;
    }
    
    private void ThumbstickForward() {
        thumbstickForwardEvent.Invoke();
    }
    
    private void ThumbstickReleased() {
        thumbstickReleasedEvent.Invoke();
    }

    public void AddThumbstickForwardListener(UnityAction action) {
        thumbstickForwardEvent.AddListener(action);
    }

    public void RemoveThumbstickForwardListener(UnityAction action) {
        thumbstickForwardEvent.RemoveListener(action);
    }

    public void AddThumbstickReleasedListener(UnityAction action) {
        thumbstickReleasedEvent.AddListener(action);
    }
    
    public void RemoveThumbstickReleasedListener(UnityAction action) {
        thumbstickReleasedEvent.RemoveListener(action);
    }
}