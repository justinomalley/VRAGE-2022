using UnityEngine;

public class AnimatableFloat : IAnimatableProperty<float> {
    public float Evaluate(float startValue, float endValue, float progress) {
        return Mathf.Lerp(startValue, endValue, progress);
    }
}
