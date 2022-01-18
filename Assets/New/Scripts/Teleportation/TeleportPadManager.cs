using System.Collections.Generic;
using UnityEngine;

public class TeleportPadManager : MonoBehaviour {
    //
    private static List<TeleportPad> teleportPads = new List<TeleportPad>(20);

    [SerializeField]
    private Color padColor;

    private static Color _padColor => instance.padColor;

    [SerializeField]
    private float highlightedAlpha, unhighlightedAlpha;

    private static readonly int ColorProperty = Shader.PropertyToID("_Color");

    [SerializeField]
    private Transform cameraRigTransform;
    
    private static Transform _cameraRigTransform => instance.cameraRigTransform;

    private static bool hittingPad, showingPads;
    private static bool pointerActive, teleportPadsActive;
    private static bool pointerInLeftHand;
    private static TeleportPad currentPad, hitPad;
    
    private static TeleportPadManager instance;
    
    private void Awake() {
        instance = this;
    }

    private void Update() {
        if (currentPad == null) {
            // The current pad should never be null, so spam the console if it is!
            Debug.LogError("Not currently standing on a pad!");
        }
    }

    private static void InitializeIfNecessary() {
        if (instance != null) {
            return;
        }

        instance = GameObject.Find("TeleportPads").GetComponent<TeleportPadManager>();
    }

    public static void Activate() {
        teleportPadsActive = true;
    }

    private static void EnableTeleportPads() {
        if (!teleportPadsActive) {
            return;
        }
        foreach (var pad in teleportPads) {
            if (pad.IsCurrentPad()) {
                continue;
            }
            pad.gameObject.SetActive(true);
            pad.FadeIn(instance.unhighlightedAlpha);
        }
    }

    private static void DisableTeleportPads(bool fade) {
        foreach (var pad in teleportPads) {
            if (pad.IsCurrentPad()) {
                continue;
            }

            if (fade) {
                pad.FadeOutAndDisable();
            } else {
                pad.Disable();
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

    public static void AddTeleportPad(TeleportPad pad) {
        InitializeIfNecessary();
        
        teleportPads.Add(pad);
        
        var _propBlock = new MaterialPropertyBlock();
        _propBlock.SetColor(ColorProperty, new Color(_padColor.r, _padColor.g, _padColor.b, 0f));
        pad.GetComponent<Renderer>().SetPropertyBlock(_propBlock);

        if (pad.IsCurrentPad()) {
            currentPad = pad;
        }
            
        pad.SetHighlightValues(instance.highlightedAlpha, instance.unhighlightedAlpha);
        pad.transform.SetParent(instance.transform);
        pad.gameObject.SetActive(false);
    }

    public static void DestroyAllPads() {
        foreach (var pad in teleportPads) {
            if (pad.isElevatorPad) {
                continue;
            }

            if (currentPad == pad) {
                currentPad = null;
            }

            if (hitPad == pad) {
                hitPad = null;
                hittingPad = false;
            }
            
            Destroy(pad.gameObject);
        }
        
        teleportPads.Clear();
    }
}
