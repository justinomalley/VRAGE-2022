using UnityEngine;

[RequireComponent(typeof(AnimateAudioSourceVolume))]
public class AudioSourceVolumeFader : Fader<float> {
    [SerializeField]
    private float startVolume, endVolume;
    
    [SerializeField]
    private float fadeDuration;

    private AnimateAudioSourceVolume volumeAnimator;

    private readonly AnimatableFloat animProgressEvaluator = new AnimatableFloat();
    
    private void Start() {
        volumeAnimator = GetComponent<AnimateAudioSourceVolume>();
        Initialize(volumeAnimator, animProgressEvaluator, startVolume, endVolume, fadeDuration);
    }
}