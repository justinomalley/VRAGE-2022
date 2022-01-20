using UnityEngine;

public class TeleportPointer : MonoBehaviour {
    private VRAGEController controller;
    
    private TeleportPad currentPad;

    private LineRenderer lineRenderer;

    private Vector3 forward;
    
    private bool activePointer;

    [SerializeField]
    private bool debug;
    
    private void Awake() {
        controller = GetComponent<VRAGEController>();
        lineRenderer = GetComponentInChildren<LineRenderer>();
        var positions = new Vector3[2];
        lineRenderer.GetPositions(positions);
        forward = positions[1].normalized;
    }
    
    private void Update() {
        if (!controller.ThumbstickForward()) {
            if (!activePointer) {
                return;
            }
            TeleportPadManager.TeleportControlReleased(controller.Hand() == Hand.Left);
            activePointer = false;
            return;
        }
        
        var fwd = transform.TransformDirection(forward.normalized);
        var inLeftHand = controller.Hand() == Hand.Left;

        if (debug) {
            Debug.DrawRay(transform.position, fwd * 50, Color.green);
        }

        if (!activePointer) {
            TeleportPadManager.TeleportControlForward(inLeftHand);
            activePointer = true;
        }

        if (Physics.Raycast(transform.position, fwd, out var objectHit, 50)) {
            if (objectHit.transform.gameObject.CompareTag("TeleportPad")) {
                currentPad = objectHit.transform.GetComponent<TeleportPad>();
                TeleportPadManager.HitPad(currentPad,inLeftHand);
            } else {
                TeleportPadManager.StoppedHittingPad(inLeftHand);
            }
            return;
        }

        TeleportPadManager.StoppedHittingPad(inLeftHand);
    }
}
