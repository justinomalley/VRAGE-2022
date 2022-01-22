using System;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour {
    
    private bool leftTouching, rightTouching;

    protected bool highlighted;

    protected Material material;
    
    [SerializeField]
    private Color highlightedColor;
    
    protected Color origColor;

    // Use virtual methods instead of abstract so subclasses can just implement what they need.

    protected virtual void Touch() {
        Highlight();
    }

    protected virtual void Untouch() {
        Unhighlight();
    }
    
    public virtual void Interact() { }

    protected virtual void Awake() {
        var renderer = GetComponent<Renderer>();

        if (renderer != null) {
            material = renderer.material;
        } else {
            renderer = GetComponentInChildren<Renderer>();
            if (renderer != null) {
                material = renderer.material;
            }
        }

        if (material == null) {
            Debug.LogError("No renderer found on this interactable object");
        }
        
        origColor = material.color;
    }

    private void OnCollisionEnter(Collision other) {
        CheckForTouch(other.gameObject);
    }

    private void OnTriggerEnter(Collider other) {
        CheckForTouch(other.gameObject);
    }

    private void CheckForTouch(GameObject other) {
        if (!other.CompareTag("Controller")) {
            return;
        }
        
        var controller = other.GetComponentInParent<VRAGEController>();

        if (controller == null) {
            Debug.LogError("Controller doesn't have ObjectInteractor component attached.");
            return;
        }
        
        controller.StartTouching(this);
        
        switch (controller.Hand()) {
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
        CheckForUntouch(other.gameObject);
    }

    private void OnCollisionExit(Collision other) {
        CheckForUntouch(other.gameObject);
    }

    private void CheckForUntouch(GameObject other) {
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
    
    protected virtual void Highlight() {
        highlighted = true;
        material.color = highlightedColor;
    }

    protected virtual void Unhighlight() {
        highlighted = false;
        material.color = origColor;
    }

    protected bool IsTouched() {
        return leftTouching || rightTouching;
    }
    
    private void OnApplicationQuit() {
        material.color = origColor;
    }
}
