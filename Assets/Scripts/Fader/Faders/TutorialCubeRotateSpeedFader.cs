using UnityEngine;

[RequireComponent(typeof(AnimateTutorialCubeRotateSpeed))]
public class TutorialCubeRotateSpeedFader : Fader<float> {
    [SerializeField]
    private float startFloat, endFloat;
    
    [SerializeField]
    private float fadeDuration;

    private TutorialCube cube;

    private AnimateTutorialCubeRotateSpeed rotationAnimator;

    private readonly AnimatableFloat animProgressEvaluator = new AnimatableFloat();
    
    private void Start() {
        cube = GetComponentInParent<TutorialCube>();
        currentValue = cube.GetRotateSpeed();
        rotationAnimator = GetComponent<AnimateTutorialCubeRotateSpeed>();
        Initialize(rotationAnimator, animProgressEvaluator, startFloat, endFloat, fadeDuration);
    }
}