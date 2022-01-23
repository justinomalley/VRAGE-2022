using UnityEngine;

[RequireComponent(typeof(AnimateTransformScale))]
public class TransformScaleFader : Fader<Vector3> {
    [SerializeField]
    private Vector3 startVector, endVector;
    
    [SerializeField]
    private float fadeDuration;

    private AnimateTransformScale scaleAnimator;

    private readonly AnimatableVector3 animProgressEvaluator = new AnimatableVector3();
    
    private void Start() {
        currentValue = transform.localScale;
        scaleAnimator = GetComponent<AnimateTransformScale>();
        Initialize(scaleAnimator, animProgressEvaluator, startVector, endVector, fadeDuration);
    }
}