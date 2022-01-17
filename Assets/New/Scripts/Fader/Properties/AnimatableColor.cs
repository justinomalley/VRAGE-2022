using UnityEngine;

public class AnimatableColor : IAnimatableProperty<Color> {
    public Color Evaluate(Color startValue, Color endValue, float progress) {
        return Color.Lerp(startValue, endValue, progress);
    }
}
