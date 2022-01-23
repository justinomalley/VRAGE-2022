using UnityEngine;

public class Elevator : MonoBehaviour {
    
    private ElevatorTeleportPad teleportPad;

    private static Quaternion originalRotation;

    private static Vector3 originalPosition, makennaPosition;

    [SerializeField]
    private Transform makennaTransform;
    
    [SerializeField]
    private AnimationCurve curve;

    [SerializeField]
    private ElevatorButton[] floorButtons;

    private TransformPositionFader fader;

    private AudioSource audioSource;

    [SerializeField]
    private AudioClip[] floorSounds;

    private ElevatorDisplay display;
    
    private static bool goneAway;
    
    private static Elevator instance;

    private void Awake() {
        originalRotation = transform.rotation;
        originalPosition = transform.position;
        makennaPosition = makennaTransform.position;
        teleportPad = GetComponentInChildren<ElevatorTeleportPad>();
        fader = GetComponent<TransformPositionFader>();
        fader.SetAnimationCurve(curve);
        audioSource = GetComponent<AudioSource>();
        display = GetComponentInChildren<ElevatorDisplay>();
        instance = this;
    }

    public static bool InElevator() {
        return instance.teleportPad.IsCurrentPad();
    }

    public static void ResetPositionAndRotation() {
        instance.transform.rotation = originalRotation;
        instance.transform.position = new Vector3(originalPosition.x, instance.transform.position.y, originalPosition.z);
    }

    public static void RotateForOS() {
        instance.transform.rotation = Quaternion.Euler(0, 180, 0);
    }
    
    public static void RotateAndPositionForMakenna() {
        instance.transform.rotation = Quaternion.Euler(0, 270, 0);
        instance.transform.position = new Vector3(makennaPosition.x, instance.transform.position.y, makennaPosition.z);
    }

    public static void SelectFloor(GalleryLoader.Room room) {
        if (GalleryLoader.GetSelectedRoom() == room || instance.display.IsLoading()) {
            return;
        }
        
        instance.audioSource.Stop();
        instance.audioSource.clip = instance.floorSounds[(int) room];
        instance.audioSource.Play();
        
        for (var i = 0; i < instance.floorButtons.Length; i++) {
            var button = instance.floorButtons[i];
            if (button.GetRoom() == room) {
                instance.floorButtons[i].Select();
            } else {
                instance.floorButtons[i].Deselect();
            }
        }
        
        instance.display.SetRoom(room);
        GalleryLoader.SetRoom(room);
    }

    public static Transform GetTransform() {
        return instance.transform;
    }

    public static void GoAway(Vector3 target) {
        if (goneAway) {
            return;
        }
        goneAway = true;
        instance.fader.Fade(target, ElevatorDoors.OpenStatic);
    }

    public static void ComeBack() {
        if (!goneAway || instance == null) {
            return;
        }
        goneAway = false;
        instance.fader.Fade(originalPosition);
        
        // This call will be ignored if we aren't in the CM gallery.
        CMGallery.DeactivateTeleportPadsOverElevator();
    }
}
