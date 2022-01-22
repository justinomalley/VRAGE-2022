using UnityEngine;

public class CMTeleportPad : TeleportPad {
    
    [SerializeField]
    private CMGallery.Artist artist;

    private CMGallery gallery;

    private ElevatorSender sender;

    protected override void Awake() {
        base.Awake();
        gallery = GetComponentInParent<CMGallery>();
        sender = GetComponent<ElevatorSender>();
    }

    public override void SetCurrentPad() {
        base.SetCurrentPad();
        gallery.SetSound(artist);

        if (sender != null) {
            sender.Send();
        }
        
        if (artist == CMGallery.Artist.Chelsee) {
            Elevator.ResetPositionAndRotation();
        } else {
            Elevator.RotateAndPositionForMakenna();
        }
    }
}
