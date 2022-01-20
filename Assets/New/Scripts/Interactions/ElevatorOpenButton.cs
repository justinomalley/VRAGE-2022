using UnityEngine;

public class ElevatorOpenButton : InteractableObject {
    [SerializeField]
    private ElevatorDoors doors;

    private bool doorsMoving, doorsOpen;

    [SerializeField]
    private ElevatorOpenButton other;

    protected override void Awake() {
        base.Awake();
        doors.doorsOpenedEvent.AddListener(DoorsOpened);
        doors.doorsClosedEvent.AddListener(DoorsClosed);
    }

    protected override void Touch() {
        if (highlighted || doorsMoving) {
            return;
        }
        
        Highlight();
    }

    protected override void Untouch() {
        if (!highlighted || doorsMoving) {
            return;
        }
        
        Unhighlight();
    }
    
    public override void Interact() {
        if (doorsMoving) {
            return;
        }
        
        other.OpenedElsewhere();

        if (doorsOpen) {
            CloseDoors();
        } else {
            OpenDoors();
        }
    }

    public void OpenedElsewhere() {
        doorsMoving = true;
        Highlight();
    }

    private void OpenDoors() {
        doorsMoving = true;
        doors.Open();
    }

    private void CloseDoors() {
        doorsMoving = true;
        doors.Close();
    }

    private void DoorsOpened() {
        if (!IsTouched()) {
            Unhighlight();
        }
        
        doorsMoving = false;
        doorsOpen = true;
    }
    
    private void DoorsClosed() {
        if (!IsTouched()) {
            Unhighlight();
        }
        
        doorsMoving = false;
        doorsOpen = false;
    }
    
    private void OnApplicationQuit() {
        Unhighlight();
    }
}