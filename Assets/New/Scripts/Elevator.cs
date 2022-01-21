using UnityEngine;

public class Elevator : MonoBehaviour {
    
    private ElevatorTeleportPad teleportPad;

    private static Quaternion originalRotation;

    private static Vector3 originalPosition, makennaPosition;

    [SerializeField]
    private Transform makennaTransform;
    
    private static Elevator instance;

    private void Awake() {
        originalRotation = transform.rotation;
        originalPosition = transform.position;
        makennaPosition = makennaTransform.position;
        teleportPad = GetComponentInChildren<ElevatorTeleportPad>();
        instance = this;
    }

    public static bool InElevator() {
        return instance.teleportPad.IsCurrentPad();
    }

    public static void ResetPositionAndRotation() {
        instance.transform.rotation = originalRotation;
        instance.transform.position = originalPosition;
    }

    public static void RotateForOS() {
        instance.transform.rotation = Quaternion.Euler(0, 180, 0);
    }
    
    public static void RotateAndPositionForMakenna() {
        instance.transform.rotation = Quaternion.Euler(0, 270, 0);
        instance.transform.position = makennaPosition;
    }

    public static Transform GetTransform() {
        return instance.transform;
    }
}
