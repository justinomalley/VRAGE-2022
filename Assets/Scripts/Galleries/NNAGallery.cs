using System;
using UnityEngine;

public class NNAGallery : GalleryRoom {
    
    public enum Artist {
        Nicole,
        Nate,
        Andrew
    }

    [SerializeField]
    private AudioClip andrewSound, nicoleProjectorSound, nateSound;

    private Artist currentArtist = Artist.Nicole;

    [SerializeField]
    private ProjectorControl projector;
    
    protected override void Awake() {
        base.Awake();
        SetSound(currentArtist);
    }

    public void SetSound(Artist artist) {
        if (currentArtist  == artist) {
            // If the projector is playing and we are in Nicole's gallery, continue..
            if (!projector.IsPlaying() || currentArtist != Artist.Nicole) {
                return;
            }
            
            // If the projector sound isn't playing, continue...
            if (audioSource.clip == nicoleProjectorSound && audioSource.isPlaying) {
                return;
            }
            
            // Set audio to projector sound.
            audioSource.clip = nicoleProjectorSound;
            audioSource.Play();
            return;
        }
        
        currentArtist = artist;
        audioSource.clip = artist switch {
            Artist.Nicole => nicoleProjectorSound,
            Artist.Nate => nateSound,
            Artist.Andrew => andrewSound,
            _ => throw new ArgumentOutOfRangeException(nameof(artist), artist, null)
        };

        if (artist != Artist.Nicole) {
            audioSource.Play();
        } else {
            // Only play Nicole's audio if the projector is playing.
            if (projector.IsPlaying()) {
                audioSource.Play();
            } else {
                audioSource.Stop();
            }
        }
    }
    
    public void StopProjectorSound() {
        if (audioSource.clip == nicoleProjectorSound && audioSource.isPlaying) {
            audioSource.Stop();
        }
    }
}
