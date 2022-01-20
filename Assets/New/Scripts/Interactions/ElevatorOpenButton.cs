using TMPro;
using UnityEngine;

/// <summary>
/// ElevatorOpenButton is a button that manages the elevator doors. By extension, this script
/// triggers the loading of galleries in `GalleryLoader` when opening the elevator door from
/// the inside after selecting a different floor.
/// </summary>
public class ElevatorOpenButton : InteractableObject {
    [SerializeField]
    private ElevatorDoors doors;
    
    [SerializeField]
    private ElevatorOpenButton other;

    [SerializeField]
    private bool isInsideButton;

    private TextMeshPro tmp;
    
    private bool doorsMoving, doorsOpen;

    protected override void Awake() {
        base.Awake();
        doors.doorsOpenedEvent.AddListener(DoorsOpened);
        doors.doorsClosedEvent.AddListener(DoorsClosed);
        tmp = GetComponentInChildren<TextMeshPro>();
    }

    protected override void Touch() {
        if (highlighted || doorsMoving) {
            return;
        }
        
        Highlight();
    }

    protected override void Untouch() {
        if (!highlighted || doorsMoving) {
            return;
        }
        
        Unhighlight();
    }
    
    public override void Interact() {
        if (doorsMoving) {
            return;
        }
        
        tmp.gameObject.SetActive(false);
        
        other.OpenedElsewhere();

        if (doorsOpen) {
            CloseDoors();
            return;
        }

        if (isInsideButton) {
            GalleryLoader.LoadGallery(OpenDoors);
        } else {
            OpenDoors();
        }
        
    }

    /// <summary>
    /// OpenedElsewhere highlights this elevator button if the other one was pressed.
    /// </summary>
    private void OpenedElsewhere() {
        doorsMoving = true;
        Highlight();
    }

    private void OpenDoors() {
        doorsMoving = true;
        doors.Open();
    }

    private void CloseDoors() {
        doorsMoving = true;
        doors.Close();
    }
    
    /// <summary>
    /// DoorsOpened is triggered by an event in ElevatorDoors when the doors are fully opened.
    /// </summary>
    private void DoorsOpened() {
        tmp.text = "CLOSE";
        tmp.fontSize = 0.16f;
        if (!IsTouched()) {
            Unhighlight();
        }
        
        doorsMoving = false;
        doorsOpen = true;
    }
    
    /// <summary>
    /// DoorsClosed is triggered by an event in ElevatorDoors when the doors are fully closed.
    /// </summary>
    private void DoorsClosed() {
        tmp.text = "OPEN";
        tmp.fontSize = 0.2f;
        if (!IsTouched()) {
            Unhighlight();
        }
        
        doorsMoving = false;
        doorsOpen = false;
    }
    
    protected override void Highlight() {
        base.Highlight();
        // Deactivate "OPEN" text when highlighting button
        tmp.gameObject.SetActive(false);
    }

    protected override void Unhighlight() {
        base.Unhighlight();
        // Activate "OPEN" text when unhighlighting button
        tmp.gameObject.SetActive(true);
    }
    
    private void OnApplicationQuit() {
        Unhighlight();
    }
}