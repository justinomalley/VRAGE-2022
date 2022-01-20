using UnityEngine;
using UnityEngine.Events;

public class ElevatorDoors : MonoBehaviour {

    private AudioSource audioSource;

    private TransformPositionFader[] positionFaders;

    private Vector3 doorOneOpen, doorTwoOpen, doorOneClosed, doorTwoClosed;

    [SerializeField]
    private AnimationCurve curve;

    [SerializeField]
    private AudioClip openSound, closeSound;

    public UnityEvent doorsOpenedEvent { get; } = new UnityEvent();
    public UnityEvent doorsClosedEvent { get; } = new UnityEvent();

    private bool doorsOpening, doorsClosing;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
        positionFaders = GetComponentsInChildren<TransformPositionFader>();

        doorOneClosed = transform.Find("door1").localPosition;
        doorTwoClosed = transform.Find("door2").localPosition;
        doorOneOpen = transform.Find("doorOneOpen").localPosition;
        doorTwoOpen = transform.Find("doorTwoOpen").localPosition;
        
        foreach (var fader in positionFaders) {
            fader.SetAnimationCurve(curve);
        }
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
        positionFaders[1].Fade(doorTwoOpen);
        positionFaders[0].AddCallback(RunDoorOpenedCallbacks);

        doorsOpening = true;
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
        positionFaders[1].Fade(doorTwoClosed);
        positionFaders[0].AddCallback(RunDoorClosedCallbacks);

        doorsOpening = false;
    }

    private void RunDoorOpenedCallbacks() {
        doorsOpening = false;
        doorsOpenedEvent.Invoke();
    }
    
    private void RunDoorClosedCallbacks() {
        doorsClosing = false;
        doorsClosedEvent.Invoke();
    }
}
