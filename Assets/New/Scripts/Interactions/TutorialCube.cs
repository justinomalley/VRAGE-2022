using UnityEngine;

public class TutorialCube : InteractableObject {

    [SerializeField] 
    private float rotateSpeed = 100f;

    [SerializeField]
    private AnimationCurve rotateCurve;
    
    [SerializeField]
    private Color highlightedColor;

    private bool highlighted, triggered;

    private Material material;

    private TransformScaleFader scaleFader;

    private TutorialCubeRotateSpeedFader rotateFader;
    
    private void Awake() {
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
        highlighted = true;
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
        scaleFader.AddCallback(OpenLobby);
        scaleFader.Fade();
        rotateFader.Fade();
    }

    private void OpenLobby() {
        Destroy(gameObject);
        App.EnterLobby();
    }
    
    private void Highlight() {
        material.color = highlightedColor;
    }

    private void Unhighlight() {
        material.color = Color.white;
    }

    public float GetRotateSpeed() {
        return rotateSpeed;
    }

    public void SetRotateSpeed(float speed) {
        rotateSpeed = speed;
    }
}