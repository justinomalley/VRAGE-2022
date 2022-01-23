using UnityEngine;

public class ElevatorButton : InteractableObject {
    [SerializeField]
    private GalleryLoader.Room room;

    private bool selected;

    protected override void Awake() {
        base.Awake();

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
        Elevator.SelectFloor(room);
    }

    public void Select() {
        selected = true;
    }

    public void Deselect() {
        // OS button is disabled (as of 1/23/22), so material will be null (since Awake hasn't ran).
        // Return early to avoid null reference exception.
        if (!gameObject.activeSelf) {
            return;
        }
        
        selected = false;
        if (!leftTouching && !rightTouching) {
            Unhighlight();
        }
    }

    public GalleryLoader.Room GetRoom() {
        return room;
    }
}

