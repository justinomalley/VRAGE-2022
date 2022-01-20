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

    public UnityEvent doorsOpened { get; } = new UnityEvent();
    public UnityEvent doorsClosed { get; } = new UnityEvent();

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
        audioSource.Stop();
        audioSource.clip = openSound;
        audioSource.Play();
        
        positionFaders[0].SetDuration(openSound.length);
        positionFaders[1].SetDuration(openSound.length);
        
        positionFaders[0].Fade(doorOneOpen);
        positionFaders[1].Fade(doorTwoOpen);
        positionFaders[0].AddCallback(RunDoorOpenedCallbacks);
    }

    public void Close() {
        audioSource.Stop();
        audioSource.clip = closeSound;
        audioSource.Play();
        
        positionFaders[0].SetDuration(closeSound.length);
        positionFaders[1].SetDuration(closeSound.length);
        
        positionFaders[0].Fade(doorOneClosed);
        positionFaders[1].Fade(doorTwoClosed);
        positionFaders[0].AddCallback(RunDoorClosedCallbacks);
    }

    private void RunDoorOpenedCallbacks() {
        doorsOpened.Invoke();
    }
    
    private void RunDoorClosedCallbacks() {
        doorsClosed.Invoke();
    }
}
