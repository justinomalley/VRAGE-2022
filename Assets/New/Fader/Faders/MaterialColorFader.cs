using UnityEngine;

[RequireComponent(typeof(AnimateMaterialAlpha))]
public class MaterialColorFader : Fader<Color> {
    [SerializeField] 
    private Color startAlpha, endAlpha;
    
    [SerializeField]
    private float fadeDuration;

    private AnimateMaterialColor materialAnimator;

    private readonly AnimatableColor animProgressEvaluator = new AnimatableColor();
    
    private void Start() {
        materialAnimator = GetComponent<AnimateMaterialColor>();
        Initialize(materialAnimator, animProgressEvaluator, startAlpha, endAlpha, fadeDuration);
    }
}
