using UnityEngine;

[RequireComponent(typeof(AnimateRendererAlpha))]
public class RendererAlphaFader : Fader<float> {
    [SerializeField]
    private float fadeDuration;

    private AnimateRendererAlpha materialAnimator;

    private readonly AnimatableFloat animProgressEvaluator = new AnimatableFloat();
    
    private void Awake() {
        materialAnimator = GetComponent<AnimateRendererAlpha>();
        // Use default values of 0 and 1 for start and end alphas; values will be overridden by TeleportPad.
        Initialize(materialAnimator, animProgressEvaluator, 0, 1, fadeDuration);
    }
}
