using System;
using UnityEngine;

public class Elevator : MonoBehaviour {
    
    private ElevatorTeleportPad teleportPad;

    private static Quaternion originalRotation;

    private static Vector3 originalPosition, makennaPosition;

    [SerializeField]
    private Transform makennaTransform;

    [SerializeField]
    private ElevatorButton[] floorButtons;
    
    private static Elevator instance;

    [SerializeField]
    private AnimationCurve curve;

    private TransformPositionFader fader;
    
    private static bool goneAway;

    private void Awake() {
        originalRotation = transform.rotation;
        originalPosition = transform.position;
        makennaPosition = makennaTransform.position;
        teleportPad = GetComponentInChildren<ElevatorTeleportPad>();
        fader = GetComponent<TransformPositionFader>();
        fader.SetAnimationCurve(curve);
        instance = this;
    }

    public static bool InElevator() {
        return instance.teleportPad.IsCurrentPad();
    }

    public static void ResetPositionAndRotation() {
        instance.transform.rotation = originalRotation;
        instance.transform.position = new Vector3(originalPosition.x, instance.transform.position.y, originalPosition.z);
    }

    public static void RotateForOS() {
        instance.transform.rotation = Quaternion.Euler(0, 180, 0);
    }
    
    public static void RotateAndPositionForMakenna() {
        instance.transform.rotation = Quaternion.Euler(0, 270, 0);
        instance.transform.position = new Vector3(makennaPosition.x, instance.transform.position.y, makennaPosition.z);
    }

    public static void SelectFloor(GalleryLoader.Room room) {
        for (var i = 0; i < instance.floorButtons.Length; i++) {
            var button = instance.floorButtons[i];
            if (button.GetRoom() != room) {
                instance.floorButtons[i].Deselect();
            }
        }
    }

    public static Transform GetTransform() {
        return instance.transform;
    }

    public static void GoAway(Vector3 target) {
        if (goneAway) {
            return;
        }
        goneAway = true;
        instance.fader.Fade(target, ElevatorDoors.OpenStatic);
    }

    public static void ComeBack() {
        if (!goneAway || instance == null) {
            return;
        }
        goneAway = false;
        instance.fader.Fade(originalPosition);
    }
}
