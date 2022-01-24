using UnityEngine;

/// <summary>
/// TeleportPointer gets input from the controllers to activate the teleportation pointer when necessary,
/// and gets collision info from `BezierPointer` to allow teleportation.
/// </summary>
public class TeleportPointer : MonoBehaviour {
    private VRAGEController controller;
    
    private BezierPointer curvedPointer;

    [SerializeField]
    private Color noHitColor, hitColor;

    private bool activePointer;

    private void Awake() {
        controller = GetComponentInParent<VRAGEController>();
        curvedPointer = GetComponent<BezierPointer>();
    }
    
    private void Update() {
        if (!controller.TeleportButtonPressed()) {
            if (!activePointer) {
                return;
            }
            curvedPointer.SetActive(false);
            TeleportPadManager.TeleportControlReleased(controller.Hand() == Hand.Left);
            activePointer = false;
            return;
        }
        
        var inLeftHand = controller.Hand() == Hand.Left;
        
        curvedPointer.SetColor(noHitColor);

        if (!activePointer) {
            curvedPointer.SetActive(true);
            TeleportPadManager.TeleportControlForward(inLeftHand);
            activePointer = true;
        }

        if (curvedPointer.IsHittingTarget(out var go)) {
            if (go.CompareTag("TeleportPad")) {
                curvedPointer.SetColor(hitColor);
                var currentPad = go.GetComponent<TeleportPad>();
                TeleportPadManager.HitPad(currentPad,inLeftHand);
            } else {
                TeleportPadManager.StoppedHittingPad(inLeftHand);
            }
            return;
        }

        TeleportPadManager.StoppedHittingPad(inLeftHand);
    }
}
