using UnityEngine;

[RequireComponent(typeof(AnimateRendererAlpha))]
public class RendererAlphaFader : Fader<float> {
    [SerializeField]
    private float fadeDuration;

    private AnimateRendererAlpha rendererAnimator;

    private readonly AnimatableFloat animProgressEvaluator = new AnimatableFloat();
    
    private void Awake() {
        rendererAnimator = GetComponent<AnimateRendererAlpha>();
        // Use default values of 0 and 1 for start and end alphas; values will be overridden by TeleportPad.
        Initialize(rendererAnimator, animProgressEvaluator, 0, 1, fadeDuration);
    }
}
