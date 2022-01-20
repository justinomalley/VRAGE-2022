using UnityEngine;

public class TutorialCube : InteractableObject {

    [SerializeField] 
    private float rotateSpeed = 100f;

    [SerializeField]
    private AnimationCurve rotateCurve;

    private bool triggered;

    private TransformScaleFader scaleFader;

    private TutorialCubeRotateSpeedFader rotateFader;
    
    protected override void Awake() {
        base.Awake();
        material = GetComponentInChildren<Renderer>().material;
        scaleFader = GetComponentInChildren<TransformScaleFader>();
        rotateFader = GetComponentInChildren<TutorialCubeRotateSpeedFader>();
        rotateFader.SetAnimationCurve(rotateCurve);
    }

    private void Update() {
        transform.Rotate(new Vector3(0, rotateSpeed, 0) * Time.deltaTime);
    }

    protected override void Touch() {
        if (highlighted || triggered) {
            return;
        }
        Highlight();
    }

    protected override void Untouch() {
        if (!highlighted || triggered) {
            return;
        }
        highlighted = false;
        Unhighlight();
    }
    
    public override void Interact() {
        if (triggered) {
            return;
        }
        
        triggered = true;
        scaleFader.Fade(OpenLobby);
        rotateFader.Fade();
    }

    private void OpenLobby() {
        Destroy(gameObject);
        GalleryLoader.EnterLobby();
    }

    public float GetRotateSpeed() {
        return rotateSpeed;
    }

    public void SetRotateSpeed(float speed) {
        rotateSpeed = speed;
    }
}