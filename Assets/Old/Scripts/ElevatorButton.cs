using UnityEngine;

public class ElevatorButton : InteractableObject {
    [SerializeField]
    private GalleryLoader.Room room;
    
    private AudioSource src;

    private bool selected;

    protected override void Awake() {
        base.Awake();
        src = GetComponent<AudioSource>();

        if (room != GalleryLoader.Room.Lobby) {
            return;
        }
        
        // Highlight lobby button on Awake since we start there.
        selected = true;
        Highlight();
    }

    protected override void Touch() {
        if (selected) {
            return;
        }
        
        Highlight();
    }
    
    protected override void Untouch() {
        if (selected) {
            return;
        }
        
        Unhighlight();
    }

    public override void Interact() {
        if (src != null && src.clip != null) {
            src.Play();
        }
        
        GalleryLoader.SetGalleryRoom(room);
        selected = true;
    }

    public void Deselect() {
        selected = false;
        if (!leftTouching && !rightTouching) {
            Unhighlight();
        }
    }

    public GalleryLoader.Room GetRoom() {
        return room;
    }
}

