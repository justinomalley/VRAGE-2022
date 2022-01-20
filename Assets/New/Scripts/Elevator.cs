using UnityEngine;

public class Elevator : MonoBehaviour {

    private static Elevator instance;

    private ElevatorTeleportPad teleportPad;

    private void Awake() {
        teleportPad = GetComponentInChildren<ElevatorTeleportPad>();
        instance = this;
    }

    public static bool InElevator() {
        return instance.teleportPad.IsCurrentPad();
    }
}
