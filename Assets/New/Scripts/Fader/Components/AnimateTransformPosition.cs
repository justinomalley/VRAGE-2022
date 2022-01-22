using UnityEngine;

public class AnimateTransformPosition : MonoBehaviour, IAnimatableComponent<Vector3> {

    private bool lockToY;

    public void LockToY() {
        lockToY = true;
    }
    
    public void Set(Vector3 property) {
        var t = transform;
        var pos = t.localPosition;
        var dest = lockToY ? new Vector3(pos.x, property.y, pos.z) : property;
        t.localPosition = dest;
    }
}
