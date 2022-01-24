using UnityEngine;

/// <summary>
/// CMTeleportPad is a script representing teleport pads in the CM gallery.
/// This implementation allows us to reposition the elevator when the user switches between rooms.
/// </summary>
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
        gallery.SetArtist(artist);

        if (sender != null) {
            sender.Send();
        }
        
        // Reposition elevator for the appropriate room.
        if (artist == CMGallery.Artist.Chelsee) {
            Elevator.ResetPositionAndRotation();
        } else {
            Elevator.RotateAndPositionForMakenna();
        }
    }

    public CMGallery.Artist GetArtist() {
        return artist;
    }
}
