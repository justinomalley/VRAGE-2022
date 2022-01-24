using System;
using UnityEngine;

/// <summary>
/// JamesButton manages the buttons in James's gallery. There is one on the radio and one on the tape player.
/// Pressing one triggers some audio to play (and also triggers the other audio source to stop playing).
/// </summary>
public class JamesButton : InteractableObject {
    private JamesGallery gallery;

    [SerializeField]
    private JamesGallery.JamesAudio audioToUse;

    private Vector3 upPosition, downPosition;

    private bool playing;

    protected override void Awake() {
        base.Awake();
        gallery = GetComponentInParent<JamesGallery>();
        upPosition = transform.position;
        downPosition = transform.Find("Down").position;
    }

    public override void Interact() {
        if (playing) {
            gallery.Stop();
            return;
        }
        
        base.Interact();
        switch (audioToUse) {
            case JamesGallery.JamesAudio.Radio:
                gallery.PlayRadio();
                break;
            case JamesGallery.JamesAudio.Tape:
                gallery.PlayTape();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    public void Press() {
        playing = true;
        transform.position = downPosition;
    }

    public void Unpress() {
        playing = false;
        transform.position = upPosition;
    }
}
