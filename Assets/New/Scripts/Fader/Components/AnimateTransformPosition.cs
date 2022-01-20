using UnityEngine;

public class AnimateTransformPosition : MonoBehaviour, IAnimatableComponent<Vector3> {
    public void Set(Vector3 property) {
        transform.localPosition = property;
    }
}
