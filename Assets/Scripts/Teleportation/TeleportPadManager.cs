using System;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPadManager : MonoBehaviour {
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

        try {
            foreach (var pad in teleportPads) {
                if (pad.IsCurrentPad() || !pad.IsActive()) {
                    continue;
                }

                pad.gameObject.SetActive(true);
                pad.FadeIn(instance.unhighlightedAlpha, () => {
                    Tutorial.StepComplete(Tutorial.TutorialStep.ActivatePointer);
                });
            }
        } catch (InvalidOperationException e) {
            Debug.LogError(e);
            // Just in case the collection was modified 
            EnableTeleportPads();
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

        if (hittingPad) {
            StoppedHittingPad(isLeftHand);
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
        
        hitPad?.Unhighlight();
        
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

        TeleportToPad();
    }

    private static void TeleportToPad(bool forced = false) {
        // We have to wait to trigger ExitElevator until we've set the new pad to the current
        // one, otherwise other things will think we're entering the elevator when we're
        // actually leaving it.
        ElevatorTeleportPad elePad = null;
        if (currentPad is ElevatorTeleportPad elePad1) {
            elePad = elePad1;
        }
        
        _cameraRigTransform.position = hitPad.transform.position;

        if (currentPad != null) {
            currentPad.UnsetCurrentPad();
        }
        
        hitPad.SetCurrentPad();
        currentPad = hitPad;
        hitPad = null;

        if (!forced) {
            Tutorial.StepComplete(Tutorial.TutorialStep.FirstTeleport);
        }

        // Set parent so we can rotate elevator while inside and keep playspace oriented correctly.
        if (currentPad is ElevatorTeleportPad elePad2) {
            Tutorial.StepComplete(Tutorial.TutorialStep.GoInsideElevator);
            _cameraRigTransform.parent = Elevator.GetTransform();
            elePad2.EnteredElevator();
        } else {
            _cameraRigTransform.SetParent(null);
        }

        if (elePad != null) {
            elePad.ExitedElevator();
        }
    }

    public static void AddTeleportPad(TeleportPad pad) {
        InitializeIfNecessary();

        teleportPads.Add(pad);
        
        var _propBlock = new MaterialPropertyBlock();
        _propBlock.SetColor(ColorProperty, new Color(_padColor.r, _padColor.g, _padColor.b, 0f));
        pad.GetComponent<Renderer>().SetPropertyBlock(_propBlock);

        if (pad.IsCurrentPad()) {
            ForceTeleport(pad);
        }
            
        pad.SetHighlightValues(instance.highlightedAlpha, instance.unhighlightedAlpha);
        pad.gameObject.SetActive(false);
    }

    public static void ForceTeleport(TeleportPad pad) {
        hitPad = pad;
        TeleportToPad(true);
    }

    public static void DestroyAllPads() {
        foreach (var pad in teleportPads) {
            if (pad is ElevatorTeleportPad) {
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
        
        teleportPads.RemoveAll(pad => !(pad is ElevatorTeleportPad));
    }
}
