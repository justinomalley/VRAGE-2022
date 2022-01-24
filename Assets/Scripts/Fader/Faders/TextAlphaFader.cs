using UnityEngine;

[RequireComponent(typeof(AnimateTextAlpha))]
public class TextAlphaFader : Fader<float> {
    [SerializeField]
    private float startAlpha, endAlpha;
    
    [SerializeField]
    private float fadeDuration;

    private AnimateTextAlpha textAnimator;

    private readonly AnimatableFloat animProgressEvaluator = new AnimatableFloat();
    
    private void Start() {
        textAnimator = GetComponent<AnimateTextAlpha>();
        Initialize(textAnimator, animProgressEvaluator, startAlpha, endAlpha, fadeDuration);
    }
}