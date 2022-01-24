using UnityEngine;

public class AnimatableVector3 : IAnimatableProperty<Vector3> {
    public Vector3 Evaluate(Vector3 startValue, Vector3 endValue, float progress) {
        return Vector3.Lerp(startValue, endValue, progress);
    }
}
