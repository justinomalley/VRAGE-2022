using UnityEngine;

public class Pointer : MonoBehaviour {
    private TeleportPad currentPad;

    private LineRenderer lineRenderer;

    private Vector3 forward;

    // left if true, right if false
    [SerializeField]
    private bool isLeftHand;

    private bool hold;

    [SerializeField]
    private bool debug;
    
    private IXRController controller;

    private void Awake() {
        controller = GetComponent<IXRController>();
        lineRenderer = GetComponentInChildren<LineRenderer>();
        var positions = new Vector3[2];
        lineRenderer.GetPositions(positions);
        forward = positions[1].normalized;
    }

    private bool activePointer;

    private void Update() {
        if (!controller.ThumbstickForward()) {
            if (!activePointer) {
                return;
            }
            TeleportPadManager.TeleportControlReleased(isLeftHand);
            activePointer = false;
            return;
        }
        
        var fwd = transform.TransformDirection(forward.normalized);

        if (debug) {
            Debug.DrawRay(transform.position, fwd * 50, Color.green);
        }

        if (!activePointer) {
            TeleportPadManager.TeleportControlForward(isLeftHand);
            activePointer = true;
        }

        if (Physics.Raycast(transform.position, fwd, out var objectHit, 50)) {
            if (objectHit.transform.gameObject.CompareTag("TeleportPad")) {
                currentPad = objectHit.transform.GetComponent<TeleportPad>();
                TeleportPadManager.HitPad(currentPad, isLeftHand);
            } else {
                TeleportPadManager.StoppedHittingPad(isLeftHand);
            }
        } else {
            TeleportPadManager.StoppedHittingPad(isLeftHand);
        }
    }
}
