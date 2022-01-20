using UnityEngine;

/// <summary>
/// AppStarter triggers the app to start once the user has pressed both triggers.
/// </summary>
public class AppStarter : MonoBehaviour {
    // `leftObj` and `rightObj` should have scripts on them implementing IXRController.
    [SerializeField]
    private GameObject leftObj, rightObj;
    
    private IXRController left, right;
    
    // `debug` skips the need to press both triggers to begin
    [SerializeField]
    private bool debug;
    
    private MaterialAlphaFader backgroundFader;
    private TextAlphaFader textFader;
    
    // `triggered` is set to true once the app has been triggered to start.
    private bool triggered;
    
    private void Awake() {
        left = leftObj.GetComponent<IXRController>();
        right = rightObj.GetComponent<IXRController>();
        backgroundFader = transform.Find("Background").GetComponent<MaterialAlphaFader>();
        textFader = transform.Find("Text").GetComponent<TextAlphaFader>();
    }

    private void Start() {
        if (debug) {
            StartApp();
        }
    }

    private void Update() {
        if (triggered) {
            return;
        }

        if (left == null || right == null) {
            Debug.LogError("Left and right controllers have not been set via the Inspector.");
            return;
        }

        if (!left.TriggerPressed() || !right.TriggerPressed()) {
            return;
        }
        
        StartApp();
    }
    
    /// <summary>
    /// StartApp triggers the "Press both triggers to begin" message to fade out,
    /// then starts the app and activates the teleportation pads.
    /// </summary>
    private void StartApp() {
        triggered = true;

        // These two faders should be configured to have the same fade duration via the inspector.
        backgroundFader.Fade(() => {
            TeleportPadManager.Activate();
            Destroy(gameObject);
        });
        textFader.Fade();
    }
}