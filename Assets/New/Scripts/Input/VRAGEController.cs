using System;
using UnityEngine;

public class VRAGEController : MonoBehaviour {
    private IXRController controller;

    private InteractableObject touchedObject;

    private void Awake() {
        controller = GetComponent<IXRController>();
    }

    public Hand ControllerOrientation() {
        return controller.Hand();
    }

    public void StartTouching(InteractableObject obj) {
        if (touchedObject != null) {
            touchedObject.StopTouching(controller.Hand());
        }
        touchedObject = obj;
    }

    public void StopTouching(InteractableObject obj) {
        obj.StopTouching(controller.Hand());
        if (touchedObject == obj) {
            touchedObject = null;
        }
    }

    // Use this bool so we don't interact with the object every frame.
    private bool triggerPressed;

    private void Update() {
        if (!controller.TriggerPressed()) {
            triggerPressed = false;
        }
        
        if (touchedObject == null || triggerPressed) {
            return;
        }

        if (!controller.TriggerPressed()) {
            return;
        }
        
        triggerPressed = true;
        touchedObject.Interact();
    }
}
