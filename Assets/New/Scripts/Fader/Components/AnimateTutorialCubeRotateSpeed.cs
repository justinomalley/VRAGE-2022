using UnityEngine;

public class AnimateTutorialCubeRotateSpeed : MonoBehaviour, IAnimatableComponent<float> {
    private TutorialCube cube;

    private void Awake() {
        cube = GetComponentInParent<TutorialCube>();
    }

    public void Set(float property) {
        cube.SetRotateSpeed(property);
    }
}
