public class DismissButton : InteractableObject {
    public override void Interact() {
        Tutorial.DismissElevatorCallMessage();
    }
}
