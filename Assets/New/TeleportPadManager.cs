using UnityEngine;

public class TeleportPadManager : MonoBehaviour {

    private static TeleportPad[] teleportPads;

    // Controller GameObjects should contain IXRController components.
    [SerializeField]
    private GameObject[] controllersObjs;
    
    private static IXRController[] controllers;

    private static bool active, showing;

    [SerializeField]
    private Color padColor;

    private static Color _padColor => instance.padColor;

    private static readonly int ColorProperty = Shader.PropertyToID("_Color");

    private static TeleportPadManager instance;

    private Transform cameraRig;

    private void Awake() {
        teleportPads = GetComponentsInChildren<TeleportPad>();
        foreach (var teleportPad in teleportPads) {
            var _propBlock = new MaterialPropertyBlock();
            _propBlock.SetColor(ColorProperty, new Color(padColor.r, padColor.g, padColor.b, 0f));
            teleportPad.GetComponent<Renderer>().SetPropertyBlock(_propBlock);

            if (teleportPad.IsCurrentPad()) {
                currentPad = teleportPad;
            }
            
            teleportPad.Initialize();
            teleportPad.gameObject.SetActive(false);
        }

        controllers = new IXRController[controllersObjs.Length];
        for (var i = 0; i < controllers.Length; i++) {
            controllers[i] = controllersObjs[i].GetComponent<IXRController>();
        }

        cameraRig = GameObject.Find("VRAGECameraRig").transform;

        instance = this;
    }
    
    private void Update() {
        if (!active) {
            return;
        }

        var show = false;
        var go = padIsHit;

        foreach (var controller in controllers) {
            show |= controller.ThumbstickForward();
            go &= !controller.ThumbstickForward();
        }

        switch (show) {
            case true when !showing:
                EnableTeleportPads();
                break;
            case false when showing:
                DisableTeleportPads();
                break;
        }

        if (go) {
            cameraRig.position = hitPad.transform.position;
            currentPad.UnsetCurrentPad();
            hitPad.SetCurrentPad();
            currentPad = hitPad;
        }
    }

    public static void Activate() {
        active = true;
    }

    private static void EnableTeleportPads() {
        showing = true;
        foreach (var teleportPad in teleportPads) {
            if (teleportPad.IsCurrentPad()) {
                continue;
            }
            teleportPad.gameObject.SetActive(true);
            teleportPad.FadeIn(_padColor.a);
        }
    }

    private static void DisableTeleportPads() {
        showing = false;
        foreach (var teleportPad in teleportPads) {
            if (teleportPad.IsCurrentPad()) {
                continue;
            }
            teleportPad.FadeOut(() => {
                teleportPad.gameObject.SetActive(false);
            });
        }
    }

    private static bool padIsHit;
    private static TeleportPad currentPad, hitPad;
    private static bool isLeft;

    public static void HitPad(TeleportPad pad, bool left) {
        if (pad == null || hitPad == pad) {
            return;
        }
        
        Debug.Log("Hit pad!");
        padIsHit = true;
        hitPad = pad;
    }

    public static void StoppedHittingPad(bool left) {
        if (!padIsHit || (padIsHit && isLeft == left)) {
            return;
        }
        Debug.Log("Stopped hitting pad.");
        hitPad = null;
        padIsHit = false;
    }
}
