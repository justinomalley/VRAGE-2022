using UnityEngine;

public class AnimateTransformScale : MonoBehaviour, IAnimatableComponent<Vector3> {
    public void Set(Vector3 property) {
        transform.localScale = property;
    }
}
