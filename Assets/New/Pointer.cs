using UnityEngine;

public class Pointer : MonoBehaviour {
    private TeleportPad currentPad;

    private LineRenderer lineRenderer;

    private Vector3 forward;

    // left if true, right if false
    [SerializeField]
    private bool left;

    private void Awake() {
        lineRenderer = GetComponentInChildren<LineRenderer>();
        var positions = new Vector3[2];
        lineRenderer.GetPositions(positions);
        forward = positions[1].normalized;
    }
    
    private void Update() {
        var fwd = transform.TransformDirection(forward.normalized);
        Debug.DrawRay(transform.position, fwd * 50, Color.green);
        if (Physics.Raycast(transform.position, fwd, out var objectHit, 50)) {
            if (objectHit.transform.gameObject.CompareTag("TeleportPad")) {
                currentPad = objectHit.transform.GetComponent<TeleportPad>();
                TeleportPadManager.HitPad(currentPad, left);
            } else {
                TeleportPadManager.StoppedHittingPad(left);
            }
        } else {
            TeleportPadManager.StoppedHittingPad(left);
        }
    }
}
