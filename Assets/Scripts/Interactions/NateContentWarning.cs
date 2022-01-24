/// <summary>
/// Nate content warning is just a button that the user needs to press to enter Nate's gallery room.
/// It contains some inappropriate content for younger viewers, so we added this to warn the user
/// beforehand.
/// </summary>
public class NateContentWarning : InteractableObject {
    public override void Interact() {
        Destroy(transform.parent.gameObject);
    }
}
