using UnityEngine;

public class CMTeleportPad : TeleportPad {
    
    [SerializeField]
    private CMGallery.Artist artist;

    private CMGallery gallery;

    protected override void Awake() {
        base.Awake();
        gallery = GetComponentInParent<CMGallery>();
    }

    public override void SetCurrentPad() {
        base.SetCurrentPad();
        gallery.SetSound(artist);
        
        if (artist == CMGallery.Artist.Chelsee) {
            Elevator.ResetPositionAndRotation();
        } else {
            Elevator.RotateAndPositionForMakenna();
        }
    }
}
