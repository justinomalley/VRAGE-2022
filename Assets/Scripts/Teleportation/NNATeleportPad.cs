using UnityEngine;

/// <summary>
/// NNATeleportPad is a script representing teleport pads in the NNA gallery.
/// This implementation allows us to change the sound when the user teleports between rooms.
/// </summary>
public class NNATeleportPad : TeleportPad {

    [SerializeField]
    private NNAGallery.Artist artist;

    private NNAGallery gallery;

    protected override void Awake() {
        base.Awake();
        gallery = GetComponentInParent<NNAGallery>();
    }

    public override void SetCurrentPad() {
        base.SetCurrentPad();
        gallery.SetSound(artist);
    }
}
