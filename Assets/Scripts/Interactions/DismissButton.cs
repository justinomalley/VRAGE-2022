/// <summary>
/// DismissButton is used to dismiss the message instructing the user on how to call the elevator back.
/// This message needs to be manually dismissed to increase the chance that the user will actually
/// get this information, since they're likely to miss it and not know how to call the elevator back.
/// </summary>
public class DismissButton : InteractableObject {
    public override void Interact() {
        Tutorial.DismissElevatorCallMessage();
    }
}
