using UnityEngine;

public class GrabbableObject : InteractableObject {

    protected override void Touch() {
        Highlight();
    }
    
    protected override void Untouch() {
        Unhighlight();
    }
    
    public virtual void Grabbed() { }

    public virtual void Dropped() { }

    protected override void Awake() {
        material = GetComponent<Renderer>().material;
    }
}