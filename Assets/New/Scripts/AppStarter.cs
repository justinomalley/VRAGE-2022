using System;
using UnityEngine;

public class AppStarter : MonoBehaviour {
    // These GameObjects should have scripts on them implementing IXRController.
    [SerializeField]
    private GameObject leftGameObject, rightGameObject;
    
    private IXRController left, right;

    private bool triggered;

    private MaterialAlphaFader backgroundFader;
    private TextAlphaFader textFader;

    [SerializeField]
    private bool debug;

    private void Awake() {
        left = leftGameObject.GetComponent<IXRController>();
        right = rightGameObject.GetComponent<IXRController>();
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

    private void StartApp() {
        triggered = true;

        backgroundFader.AddCallback(() => {
            TeleportPadManager.Activate();
            Destroy(gameObject);
        });

        // These two faders should be configured to have the same fade duration via the inspector.
        backgroundFader.Fade();
        textFader.Fade();
    }
}