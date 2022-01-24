using UnityEngine;

/// <summary>
/// GalleryRoom represents the various gallery rooms that can be visited via the Elevator.
/// </summary>
public class GalleryRoom : MonoBehaviour {
    
    protected AudioSource audioSource;

    private AudioSourceVolumeFader fader;

    [SerializeField]
    private bool playOnAwake = true;

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

    public void FadeAudioInOnEntry() {
        InitializeIfNecessary();
        fader.SetDuration(ElevatorDoors.doorOpenAnimationDuration);
        fader.Fade(1);

        if (playOnAwake) {
            audioSource.Play();
        }
    }

    public void FadeAudioOutOnExit() {
        InitializeIfNecessary();

        if (!audioSource.isPlaying) {
            return;
        }
        
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
