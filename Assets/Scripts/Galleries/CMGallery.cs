using UnityEngine;

public class CMGallery : GalleryRoom {

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

    public void SetSound(Artist artist) {
        if (currentArtist  == artist) {
            return;
        }
        
        currentArtist = artist;
        audioSource.clip = artist == Artist.Chelsee ? chelseeSound : makennaSound;
        audioSource.Play();
    }

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
