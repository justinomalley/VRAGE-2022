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

    private void Awake() {
        teleportPads = GetComponentsInChildren<TeleportPad>();
        foreach (var teleportPad in teleportPads) {
            var _propBlock = new MaterialPropertyBlock();
            _propBlock.SetColor(ColorProperty, new Color(padColor.r, padColor.g, padColor.b, 0f));
            teleportPad.GetComponent<Renderer>().SetPropertyBlock(_propBlock);
            teleportPad.Initialize();
            teleportPad.gameObject.SetActive(false);
        }

        controllers = new IXRController[controllersObjs.Length];
        for (var i = 0; i < controllers.Length; i++) {
            controllers[i] = controllersObjs[i].GetComponent<IXRController>();
        }

        instance = this;
    }
    
    private void Update() {
        if (!active) {
            return;
        }

        var show = false;

        foreach (var controller in controllers) {
            if (controller.ThumbstickForward()) {
                show = true;
            }
        }

        switch (show) {
            case true when !showing:
                EnableTeleportPads();
                break;
            case false when showing:
                DisableTeleportPads();
                break;
        }
    }

    public static void Activate() {
        active = true;
    }

    private static void EnableTeleportPads() {
        showing = true;
        foreach (var teleportPad in teleportPads) {
            teleportPad.gameObject.SetActive(true);
            teleportPad.FadeIn(_padColor.a);
        }
    }

    private static void DisableTeleportPads() {
        showing = false;
        foreach (var teleportPad in teleportPads) {
            teleportPad.FadeOut(() => {
                teleportPad.gameObject.SetActive(false);
            });
        }
    }
}
