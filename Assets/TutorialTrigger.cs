using UnityEngine;

public class TutorialTrigger : MonoBehaviour {
    [SerializeField]
    private GenericXRController left, right;

    private bool triggered;

    private void Update() {
        if (triggered) {
            return;
        }

        if (left == null || right == null) {
            Debug.LogError("Left and right controllers have not been set via the Inspector.");
            return;
        }

        if (!left.TriggerPressed() || !right.TriggerPressed()) {
            return;
        }
        
        Debug.LogError("Time to begin the tutorial!");
        triggered = true;
    }
}