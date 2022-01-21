using UnityEngine;

public class Elevator : MonoBehaviour {

    private static Elevator instance;

    private ElevatorTeleportPad teleportPad;

    private static Quaternion originalRotation;

    private void Awake() {
        originalRotation = transform.rotation;
        teleportPad = GetComponentInChildren<ElevatorTeleportPad>();
        instance = this;
    }

    public static bool InElevator() {
        return instance.teleportPad.IsCurrentPad();
    }

    public static void ResetRotation() {
        instance.transform.rotation = originalRotation;
    }

    public static void RotateForOSRoom() {
        instance.transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    public static Transform GetTransform() {
        return instance.transform;
    }
}
