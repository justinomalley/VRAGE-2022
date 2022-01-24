using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// ElevatorDoors manages the animation of the elevator doors and how they interact with ElevatorOpenButtons.
/// </summary>
public class ElevatorDoors : MonoBehaviour {

    /* Animation */

    //`positionFaders` is used to animate the positions of the doors.
    private TransformPositionFader[] positionFaders;

    // Vectors representing the positions of both doors while they are opened or closed.
    private Vector3 doorOneOpen, doorTwoOpen, doorOneClosed, doorTwoClosed;

    // `curve` is applied to the door opening animation to make it a bit smoother.
    [SerializeField]
    private AnimationCurve curve;
    
    /* Sound */
    
    private AudioSource audioSource;
    
    [SerializeField]
    private AudioClip openSound, closeSound;
    
    /* Events for various elevator door actions */

    public UnityEvent doorsOpenedEvent { get; } = new UnityEvent();
    public UnityEvent doorsOpeningEvent { get; } = new UnityEvent();
    public UnityEvent doorsClosedEvent { get; } = new UnityEvent();
    public UnityEvent doorsClosingEvent { get; } = new UnityEvent();
    
    /* Animation durations (determined by length of respective audio clips)*/
    
    public static float doorOpenAnimationDuration { get; private set; }
    public static float doorCloseAnimationDuration { get; private set; }
    
    /* State */
    
    private bool doorsOpening, doorsClosing;
    
    /* Singleton instance*/

    private static ElevatorDoors instance;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
        doorOpenAnimationDuration = openSound.length;
        doorCloseAnimationDuration = closeSound.length;
        
        positionFaders = GetComponentsInChildren<TransformPositionFader>();

        doorOneClosed = transform.Find("door1").localPosition;
        doorTwoClosed = transform.Find("door2").localPosition;
        doorOneOpen = transform.Find("doorOneOpen").localPosition;
        doorTwoOpen = transform.Find("doorTwoOpen").localPosition;
        
        foreach (var fader in positionFaders) {
            fader.SetAnimationCurve(curve);
        }
        
        GalleryLoader.InitializeSoundFadeForDoors(this);
        instance = this;
    }
    
    public static void OpenStatic() {
        instance.Open();
    }

    public void Open() {
        if (doorsOpening) {
            return;
        }
        
        audioSource.Stop();
        audioSource.clip = openSound;
        audioSource.Play();
        
        positionFaders[0].SetDuration(openSound.length);
        positionFaders[1].SetDuration(openSound.length);
        
        positionFaders[0].Fade(doorOneOpen);
        positionFaders[1].Fade(doorTwoOpen, () => {
            doorsOpening = false;
            doorsOpenedEvent.Invoke();
        });
        
        doorsOpening = true;
        doorsOpeningEvent.Invoke();
    }

    public void Close() {
        if (doorsClosing) {
            return;
        }
        
        audioSource.Stop();
        audioSource.clip = closeSound;
        audioSource.Play();
        
        positionFaders[0].SetDuration(closeSound.length);
        positionFaders[1].SetDuration(closeSound.length);
        
        positionFaders[0].Fade(doorOneClosed);
        positionFaders[1].Fade(doorTwoClosed, () => {
            doorsClosing = false;
            doorsClosedEvent.Invoke();
        });
        
        doorsOpening = false;
        doorsClosingEvent.Invoke();
    }
}
