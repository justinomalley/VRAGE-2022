/// <summary>
/// IXRController exists to make it easier to add support for platforms that can't be used with the
/// generic XR bindings in the future if necessary.
/// </summary>
public interface IXRController {
    public bool TriggerPressed();

    public bool ThumbstickForward();
}
