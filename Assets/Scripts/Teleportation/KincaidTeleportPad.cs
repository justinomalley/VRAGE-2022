using UnityEngine;

public class KincaidTeleportPad : TeleportPad {

    [SerializeField] 
    private Transform elevatorTarget;

    private bool sentAwayElevator;

    public override void SetCurrentPad() {
        base.SetCurrentPad();

        if (sentAwayElevator) {
            return;
        }

        Elevator.SendAway(elevatorTarget.position);
        sentAwayElevator = true;
    }
}
