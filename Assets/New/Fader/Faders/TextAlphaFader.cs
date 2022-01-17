using UnityEngine;

[RequireComponent(typeof(AnimateTextAlpha))]
public class TextAlphaFader : Fader<float> {
    [SerializeField]
    private float startAlpha, endAlpha;
    
    [SerializeField]
    private float fadeDuration;

    private AnimateTextAlpha materialAnimator;

    private readonly AnimatableFloat animProgressEvaluator = new AnimatableFloat();
    
    private void Start() {
        materialAnimator = GetComponent<AnimateTextAlpha>();
        Initialize(materialAnimator, animProgressEvaluator, startAlpha, endAlpha, fadeDuration);
    }
}