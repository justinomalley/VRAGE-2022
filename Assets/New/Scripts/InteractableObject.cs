using System;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour {
    
    private bool leftTouching, rightTouching;

    [SerializeField]
    protected bool grabbable, interactable;

    // Use virtual methods instead of abstract so subclasses can just implement what they need.
    
    protected virtual void Touch() { }

    protected virtual void Untouch() { }
    
    public virtual void Interact() { }
    
    private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("Controller")) {
            return;
        }
        
        var controller = other.GetComponentInParent<VRAGEController>();

        if (controller == null) {
            Debug.LogError("Controller doesn't have ObjectInteractor component attached.");
            return;
        }
        
        controller.StartTouching(this);
        
        switch (controller.ControllerOrientation()) {
            case Hand.Left:
                leftTouching = true;
                break;
            case Hand.Right:
                rightTouching = true;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        if (leftTouching || rightTouching) {
            Touch();
        }
    }

    private void OnTriggerExit(Collider other) {
        if (!other.CompareTag("Controller")) {
            return;
        }
        
        var controller = other.GetComponentInParent<VRAGEController>();
        
        if (controller == null) {
            Debug.LogError("Controller doesn't have ObjectInteractor component attached.");
            return;
        }

        controller.StopTouching(this);
    }

    // Called by VRAGEController when we stop touching this object.
    // Also used if we're touching this object, but we also just started touching another.
    // Only one object can be touched at a time per hand for now.
    public void StopTouching(Hand hand) {
        switch (hand) {
            case Hand.Left:
                leftTouching = false;
                break;
            case Hand.Right:
                rightTouching = false;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        if (!leftTouching && !rightTouching) {
            Untouch();
        }
    }
}
