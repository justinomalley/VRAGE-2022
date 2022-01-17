using UnityEngine;

[RequireComponent(typeof(AnimateMaterialAlpha))]
public class MaterialAlphaFader : Fader<float> {
    [SerializeField] 
    private float startAlpha, endAlpha;
    
    [SerializeField]
    private float fadeDuration;

    private AnimateMaterialAlpha materialAnimator;

    private readonly AnimatableFloat animProgressEvaluator = new AnimatableFloat();
    
    private void Start() {
        materialAnimator = GetComponent<AnimateMaterialAlpha>();
        Initialize(materialAnimator, animProgressEvaluator, startAlpha, endAlpha, fadeDuration);
    }
}
