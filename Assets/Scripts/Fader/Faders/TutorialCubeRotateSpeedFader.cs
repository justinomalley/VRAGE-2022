using UnityEngine;

[RequireComponent(typeof(AnimateTutorialCubeRotateSpeed))]
public class TutorialCubeRotateSpeedFader : Fader<float> {
    [SerializeField]
    private float startFloat, endFloat;
    
    [SerializeField]
    private float fadeDuration;

    private TutorialCube cube;

    private AnimateTutorialCubeRotateSpeed scaleAnimator;

    private readonly AnimatableFloat animProgressEvaluator = new AnimatableFloat();
    
    private void Start() {
        cube = GetComponentInParent<TutorialCube>();
        currentValue = cube.GetRotateSpeed();
        scaleAnimator = GetComponent<AnimateTutorialCubeRotateSpeed>();
        Initialize(scaleAnimator, animProgressEvaluator, startFloat, endFloat, fadeDuration);
    }
}