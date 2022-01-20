using UnityEngine;

public class ElevatorTeleportPad : TeleportPad {
    [SerializeField]
    private ElevatorDoors doors;

    public void EnteredElevator() {
        doors.Close();
    }
    
    public void ExitedElevator() {
        doors.Close();
    }
}