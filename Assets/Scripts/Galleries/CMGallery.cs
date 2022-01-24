using UnityEngine;

/// <summary>
/// CMGallery is a GalleryRoom script for Chelsee & Makenna's joint gallery room.
/// </summary>
public class CMGallery : GalleryRoom {
    
    /* For tracking whose gallery room we are in. */

    public enum Artist {
        Chelsee,
        Makenna
    }

    private Artist currentArtist = Artist.Chelsee;

    [SerializeField]
    private AudioClip chelseeSound, makennaSound;

    private CMTeleportPad[] cmTeleportPads;

    private static CMGallery instance;

    protected override void Awake() {
        base.Awake();
        cmTeleportPads = transform.Find("TeleportationPads").GetComponentsInChildren<CMTeleportPad>();
        instance = this;
    }

    /// <summary>
    /// SetArtist informs us of which room the user is currently in, so we can set the sound accordingly.
    /// </summary>
    public void SetArtist(Artist artist) {
        if (currentArtist  == artist) {
            return;
        }
        
        currentArtist = artist;
        audioSource.clip = artist == Artist.Chelsee ? chelseeSound : makennaSound;
        audioSource.Play();
    }
    
    /// <summary>
    /// DeactivateTeleportPadsOverElevator is called when the elevator is called back after having disappeared.
    /// It disables the teleport pads in the adjacent room to keep the user from teleporting behind/inside of
    /// the elevator before it has arrived.
    /// </summary>
    public static void DeactivateTeleportPadsOverElevator() {
        if (instance == null) {
            return;
        }

        for (var i = 0; i < instance.cmTeleportPads.Length; i++) {
            var pad = instance.cmTeleportPads[i];
            if (instance.currentArtist != pad.GetArtist()) {
                pad.Deactivate();
            }
        }
    }
}
