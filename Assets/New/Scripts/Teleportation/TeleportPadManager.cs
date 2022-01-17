using UnityEngine;

public class TeleportPadManager : MonoBehaviour {

    private static TeleportPad[] teleportPads;

    [SerializeField]
    private Color padColor;

    private static Color _padColor => instance.padColor;

    [SerializeField]
    private float highlightedAlpha, unhighlightedAlpha;

    private static readonly int ColorProperty = Shader.PropertyToID("_Color");

    [SerializeField]
    private Transform cameraRigTransform;
    
    private static Transform _cameraRigTransform => instance.cameraRigTransform;
    
    private static TeleportPadManager instance;
    
    private static bool hittingPad, showingPads;
    private static bool pointerActive, teleportPadsActive;
    private static bool pointerInLeftHand;
    private static TeleportPad currentPad, hitPad;


    private void Awake() {
        teleportPads = GetComponentsInChildren<TeleportPad>();
        foreach (var teleportPad in teleportPads) {
            var _propBlock = new MaterialPropertyBlock();
            _propBlock.SetColor(ColorProperty, new Color(padColor.r, padColor.g, padColor.b, 0f));
            teleportPad.GetComponent<Renderer>().SetPropertyBlock(_propBlock);

            if (teleportPad.IsCurrentPad()) {
                currentPad = teleportPad;
            }
            
            teleportPad.Initialize(highlightedAlpha, unhighlightedAlpha);
            teleportPad.gameObject.SetActive(false);
        }

        instance = this;
    }

    public static void Activate() {
        teleportPadsActive = true;
    }

    private static void EnableTeleportPads() {
        foreach (var teleportPad in teleportPads) {
            if (teleportPad.IsCurrentPad()) {
                continue;
            }
            teleportPad.gameObject.SetActive(true);
            teleportPad.FadeIn(instance.unhighlightedAlpha);
        }
    }

    private static void DisableTeleportPads(bool fade) {
        foreach (var teleportPad in teleportPads) {
            if (teleportPad.IsCurrentPad()) {
                continue;
            }

            if (fade) {
                teleportPad.FadeOutAndDisable();
            } else {
                teleportPad.Disable();
            }
        }
    }
    
    public static void HitPad(TeleportPad pad, bool isLeftHand) {
        if (hittingPad && hitPad == pad || pointerInLeftHand != isLeftHand) {
            return;
        }
        
        pointerInLeftHand = isLeftHand;
        hittingPad = true;
        hitPad = pad;
        hitPad.Highlight();
    }

    public static void StoppedHittingPad(bool isLeftHand) {
        if (!hittingPad || pointerInLeftHand != isLeftHand) {
            return;
        }
        
        hitPad.Unhighlight();
        
        hitPad = null;
        hittingPad = false;
    }
    
    public static void TeleportControlForward(bool isLeftHand) {
        if (pointerActive || !teleportPadsActive) {
            return;
        }

        if (!showingPads) {
            showingPads = true;
            EnableTeleportPads();
        }

        pointerActive = true;
        pointerInLeftHand = isLeftHand;
    }

    public static void TeleportControlReleased(bool isLeftHand) {
        if (!pointerActive || pointerInLeftHand != isLeftHand || !teleportPadsActive) {
            return;
        }

        pointerActive = false;
        showingPads = false;
        
        if (!hittingPad) {
            DisableTeleportPads(true);
            return;
        }
        
        DisableTeleportPads(false);
        
        _cameraRigTransform.position = hitPad.transform.position;
        currentPad.UnsetCurrentPad();
        hitPad.SetCurrentPad();
        currentPad = hitPad;
    }
}
