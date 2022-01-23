public class NateContentWarning : InteractableObject {
    public override void Interact() {
        Destroy(transform.parent.gameObject);
    }
}
