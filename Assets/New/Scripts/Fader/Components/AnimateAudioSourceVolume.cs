using UnityEngine;

public class AnimateAudioSourceVolume : MonoBehaviour, IAnimatableComponent<float> {
    private AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
        if (audioSource != null) {
            return;
        }
        
        Debug.LogError("Attempting to animate audio source, but no AudioSource component selected!");
    }

    public void Set(float property) {
        audioSource.volume = property;
    }
}
