using UnityEngine;

/// <summary>
/// Elevator manages the elevator that allows players to navigate between the various galleries.
/// This is a singleton, so anything that can be static is.
/// </summary>
public class Elevator : MonoBehaviour {
    
    //`teleportPad` is the teleport pad inside the elevator. When it is teleported to,
    // we know the user is in the elevator.
    private static ElevatorTeleportPad teleportPad;
    
    /* Reorientation */

    private static Quaternion originalRotation;

    // makennaPosition is the position the elevator should move to when in Makenna's gallery.
    private static Vector3 originalPosition, makennaPosition;
    
    [SerializeField]
    private Transform makennaTransform;
    
    // `goneAway` is set to true when the elevator disappears (only happens in certain galleries where it gets in the way).
    private static bool sentAway;
    
    // `fader` is used to animate the position of the elevator (to make it disappear).
    private static TransformPositionFader fader;
    
    // `curve` is applied to the elevator position animation to make it a bit smoother.
    [SerializeField]
    private AnimationCurve curve;
    
    /* Button panel and display */
    
    [SerializeField]
    private ElevatorButton[] floorButtons;
    
    // `floorSounds` are the sounds used when an elevator button is pressed and a gallery floor is selected.
    [SerializeField]
    private AudioClip[] floorSounds;
    
    private static AudioSource audioSource;
    
    private static ElevatorDisplay display;
    
    /* Singleton Instance */
    
    private static Elevator instance;

    private void Awake() {
        teleportPad = GetComponentInChildren<ElevatorTeleportPad>();
        
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        makennaPosition = makennaTransform.position;
        
        fader = GetComponent<TransformPositionFader>();
        fader.SetAnimationCurve(curve);
        audioSource = GetComponent<AudioSource>();
        display = GetComponentInChildren<ElevatorDisplay>();
        instance = this;
    }

    public static bool InElevator() {
        return teleportPad.IsCurrentPad();
    }

    /// <summary>
    /// ResetPositionAndRotation resets the elevator to its original position and rotation.
    /// The y-position is ignored so as not to interfere with the elevator rising and falling
    /// position if it is currently in progress.
    /// </summary>
    public static void ResetPositionAndRotation() {
        var t = instance.transform;
        t.rotation = originalRotation;
        t.position = new Vector3(originalPosition.x, t.position.y, originalPosition.z);
    }

    /// <summary>
    /// RotateforOS rotates the elevator for positioning in the OS gallery.
    /// </summary>
    public static void RotateForOS() {
        instance.transform.rotation = Quaternion.Euler(0, 180, 0);
    }
    
    /// <summary>
    /// RotateAndPositionForMakenna rotates and positions the elevator for Makenna's portion of the CM gallery.
    /// </summary>
    public static void RotateAndPositionForMakenna() {
        var t = instance.transform;
        t.rotation = Quaternion.Euler(0, 270, 0);
        t.position = new Vector3(makennaPosition.x, t.position.y, makennaPosition.z);
    }
    
    public static void SelectFloor(GalleryLoader.Room room) {
        if (GalleryLoader.GetSelectedRoom() == room || display.IsLoading()) {
            return;
        }
        
        // Play the audio clip for the selected floor.
        audioSource.Stop();
        audioSource.clip = instance.floorSounds[(int) room];
        audioSource.Play();
        
        // Unhighlight all buttons except for the currently selected one.
        for (var i = 0; i < instance.floorButtons.Length; i++) {
            var button = instance.floorButtons[i];
            if (button.GetRoom() == room) {
                instance.floorButtons[i].Select();
            } else {
                instance.floorButtons[i].Deselect();
            }
        }
        
        // Update the screen above the control panel in the elevator
        display.SetRoom(room);
        
        // Load the next room.
        GalleryLoader.SetRoom(room);
    }

    public static Transform GetTransform() {
        return instance.transform;
    }
    
    /// <summary>
    /// SendAway sends the elevator to a predetermined `target` position to hide it in certain gallery rooms.
    /// </summary>
    public static void SendAway(Vector3 target) {
        if (sentAway) {
            return;
        }
        sentAway = true;
        fader.Fade(target);
    }
    
    /// <summary>
    /// CallBack calls the elevator back if it has been sent away.
    /// </summary>
    public static void CallBack() {
        if (!sentAway || instance == null) {
            return;
        }
        sentAway = false;
        fader.Fade(originalPosition, ElevatorDoors.OpenStatic);
        
        // This call will be ignored if we aren't in the CM gallery.
        // It just deactivates the teleportation pads in the adjacent room in the CM
        // gallery to avoid the user being able to teleport where the elevator is about to rise.
        CMGallery.DeactivateTeleportPadsOverElevator();
    }
}
