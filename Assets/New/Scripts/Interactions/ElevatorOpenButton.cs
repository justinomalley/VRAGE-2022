public class ElevatorOpenButton : InteractableObject {

    private ElevatorDoors doors;

    private bool doorsMoving, doorsOpen;

    protected override void Awake() {
        base.Awake();
        doors = transform.parent.GetComponentInChildren<ElevatorDoors>();
        doors.doorsOpened.AddListener(DoorsOpened);
        doors.doorsClosed.AddListener(DoorsClosed);
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

        doorsMoving = true;

        if (doorsOpen) {
            CloseDoors();
        } else {
            OpenDoors();
        }
    }

    private void OpenDoors() {
        doors.Open();
    }

    private void CloseDoors() {
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