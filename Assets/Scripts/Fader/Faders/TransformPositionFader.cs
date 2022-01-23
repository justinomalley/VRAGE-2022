using UnityEngine;

[RequireComponent(typeof(AnimateTransformPosition))]
public class TransformPositionFader : Fader<Vector3> {
    [SerializeField]
    private Vector3 startVector, endVector;
    
    [SerializeField]
    private float fadeDuration;

    [SerializeField]
    private bool lockToYOnly;

    private AnimateTransformPosition positionAnimator;

    private readonly AnimatableVector3 animProgressEvaluator = new AnimatableVector3();
    
    private void Start() {
        currentValue = transform.localPosition;
        positionAnimator = GetComponent<AnimateTransformPosition>();
        if (lockToYOnly) {
            positionAnimator.LockToY();
        }
        Initialize(positionAnimator, animProgressEvaluator, startVector, endVector, fadeDuration);
    }
}