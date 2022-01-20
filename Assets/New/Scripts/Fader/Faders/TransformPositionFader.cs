using UnityEngine;

[RequireComponent(typeof(AnimateTransformPosition))]
public class TransformPositionFader : Fader<Vector3> {
    [SerializeField]
    private Vector3 startVector, endVector;
    
    [SerializeField]
    private float fadeDuration;

    private AnimateTransformPosition scaleAnimator;

    private readonly AnimatableVector3 animProgressEvaluator = new AnimatableVector3();
    
    private void Start() {
        currentValue = transform.localPosition;
        scaleAnimator = GetComponent<AnimateTransformPosition>();
        Initialize(scaleAnimator, animProgressEvaluator, startVector, endVector, fadeDuration);
    }
}