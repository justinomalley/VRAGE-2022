using UnityEngine;

public class GalleryRoom : MonoBehaviour {
    
    private AudioSource audioSource;

    private AudioSourceVolumeFader fader;

    private bool initialized;
    
    protected virtual void Awake() {
        InitializeIfNecessary();
    }

    private void InitializeIfNecessary() {
        if (initialized) {
            return;
        }
        
        initialized = true;
        audioSource = GetComponent<AudioSource>();
        fader = GetComponent<AudioSourceVolumeFader>();
    }

    public void FadeAudioIn() {
        InitializeIfNecessary();
        fader.SetDuration(ElevatorDoors.doorOpenAnimationDuration);
        fader.Fade(1);
        audioSource.Play();
    }

    public void FadeAudioOut() {
        InitializeIfNecessary();
        fader.SetDuration(ElevatorDoors.doorCloseAnimationDuration);
        fader.Fade(0, () => {
            audioSource.Stop();
        });
    }

    public void CancelFadeOut() {
        InitializeIfNecessary();
        fader.CancelFade(1);
        if (!audioSource.isPlaying) {
            audioSource.Play();
        }
    }
}
