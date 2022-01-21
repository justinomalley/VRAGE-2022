using UnityEngine;

public class CMGallery : GalleryRoom {

    public enum Artist {
        Chelsee,
        Makenna
    }

    private Artist currentArtist = Artist.Chelsee;

    [SerializeField]
    private AudioClip chelseeSound, makennaSound;

    public void SetSound(Artist artist) {
        if (currentArtist  == artist) {
            return;
        }
        
        currentArtist = artist;
        audioSource.clip = artist == Artist.Chelsee ? chelseeSound : makennaSound;
        audioSource.Play();
    }
}
