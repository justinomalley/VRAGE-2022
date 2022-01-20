using UnityEngine;

public class ElevatorButton : InteractableObject {
    [SerializeField]
    private GalleryLoader.Gallery gallery;
    
    private AudioSource src;

    protected override void Awake() {
        base.Awake();
        src = GetComponent<AudioSource>();
    }

    protected override void Touch() {
        if (highlighted) {
            return;
        }
        
        Highlight();
    }
    
    protected override void Untouch() {
        if (!highlighted) {
            return;
        }
        
        Unhighlight();
    }

    public override void Interact() {
        if (src != null && src.clip != null) {
            src.Play();
        }
        
        GalleryLoader.SetGalleryRoom(gallery);
    }
}

