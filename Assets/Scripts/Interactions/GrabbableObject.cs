using UnityEngine;

/// <summary>
/// In progress.
/// </summary>
public class GrabbableObject : InteractableObject {

    public virtual void Grabbed() { }

    public virtual void Dropped() { }

    protected override void Awake() {
        material = GetComponent<Renderer>().material;
    }
}