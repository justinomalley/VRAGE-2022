using System;
using UnityEngine;

public class JamesButton : InteractableObject {
    private JamesGallery gallery;

    [SerializeField]
    private JamesAudio audioToUse;

    private Vector3 upPosition, downPosition;

    private bool playing;

    protected override void Awake() {
        base.Awake();
        gallery = GetComponentInParent<JamesGallery>();
        upPosition = transform.position;
        downPosition = transform.Find("Down").position;
    }

    protected override void Touch() {
        Highlight();
    }
    
    protected override void Untouch() {
        Unhighlight();
    }

    public override void Interact() {
        if (playing) {
            gallery.Stop();
            return;
        }
        
        base.Interact();
        switch (audioToUse) {
            case JamesAudio.Radio:
                gallery.PlayRadio();
                break;
            case JamesAudio.Tape:
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
