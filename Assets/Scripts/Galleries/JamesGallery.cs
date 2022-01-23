using UnityEngine;

public class JamesGallery : GalleryRoom {

    [SerializeField]
    private AudioClip radioClip, tapeClip;

    [SerializeField]
    private JamesButton radioButton, tapeButton;

    public void PlayRadio() {
        radioButton.Press();
        tapeButton.Unpress();
        audioSource.Stop();
        audioSource.clip = radioClip;
        audioSource.Play();
    }

    public void PlayTape() {
        tapeButton.Press();
        radioButton.Unpress();
        audioSource.Stop();
        audioSource.clip = tapeClip;
        audioSource.Play();
    }

    public void Stop() {
        tapeButton.Unpress();
        radioButton.Unpress();
        audioSource.Stop();
    }
}

public enum JamesAudio {
    Radio,
    Tape
}
