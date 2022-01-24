using UnityEngine;

/// <summary>
/// IXRController is a wrapper for VR controllers to get the input we need to run VRAGE.
/// It exists to make it easier to add support for platforms that can't be used with the
/// generic XR bindings in the future if necessary.
/// </summary>
public interface IXRController {
    public bool TriggerPressed();

    public bool ThumbstickForward();
    public Vector3 Position();
    public Quaternion Rotation();

    public Hand Hand();
}

public enum Hand {
    Left,
    Right
}