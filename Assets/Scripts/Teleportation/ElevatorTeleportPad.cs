using UnityEngine;

public class ElevatorTeleportPad : TeleportPad {
    [SerializeField]
    private ElevatorDoors doors;

    public void EnteredElevator() {
        doors.Close();
    }
    
    public void ExitedElevator() {
        doors.Close();
        Tutorial.StepComplete(Tutorial.TutorialStep.EnterFirstGallery);
    }
}