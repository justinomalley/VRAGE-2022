using UnityEngine;

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
