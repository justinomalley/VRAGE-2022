using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Tutorial manages the tutorial.
/// </summary>
public class Tutorial : MonoBehaviour {
    [SerializeField]
    private AudioClip[] instructionSound;
    
    [SerializeField]
    private Sprite[] instructionSprites;

    [SerializeField]
    private Transform tutorialAnchor;

    private AudioSource audioSource;

    private Image image;

    private DismissButton dismissButton;

    private static TutorialStep currentStep;

    private static Tutorial instance;

    public enum TutorialStep {
        ActivatePointer,
        FirstTeleport,
        InteractCube,
        TeleportToElevator,
        GoInsideElevator,
        EnterFirstGallery,
        EnteredGalleryWithCallableElevator
    }

    private void Awake() {
        transform.SetPositionAndRotation(tutorialAnchor.position, tutorialAnchor.rotation);
        transform.SetParent(tutorialAnchor);
        audioSource = GetComponent<AudioSource>();
        image = GetComponentInChildren<Image>();
        dismissButton = transform.Find("DismissButton").GetComponent<DismissButton>();
        instance = this;
        
        gameObject.SetActive(false);
        dismissButton.gameObject.SetActive(false);
    }
    
    public static void StartTutorial() {
        instance.gameObject.SetActive(true);
        instance.StartStep(TutorialStep.ActivatePointer);
    }
    
    /// <summary>
    /// StepComplete is called when the action associated with `step` has been successfully executed.
    /// </summary>
    public static void StepComplete(TutorialStep step) {
        if (instance == null || step < currentStep) {
            return;
        }
        
        if (step >= TutorialStep.EnterFirstGallery) {
            instance.audioSource.Stop();
            instance.gameObject.SetActive(false);
            return;
        }

        instance.StartStep(step + 1);
    }

    private void StartStep(TutorialStep step) {
        var index = (int) step;
        
        audioSource.Stop();
        audioSource.clip = instructionSound[index];
        audioSource.Play();

        image.sprite = instructionSprites[index];
        currentStep = step;
    }
    
    /// <summary>
    /// DisplayElevatorCallMessage displays an extra tutorial step indicating how to call back the elevator
    /// in galleries where it disappears through the floor.
    /// </summary>
    public static void DisplayElevatorCallMessage() {
        instance.gameObject.SetActive(true);
        instance.StartStep(TutorialStep.EnteredGalleryWithCallableElevator);
        instance.dismissButton.gameObject.SetActive(true);
    }
    
    /// <summary>
    /// DismissElevatorCallMessage is called when the user presses the "OK!" button on the bottom of the
    /// callable elevator tutorial step. We require it to be manually closed to make sure the user gets
    /// a chance to digest that information (since they'll need it to leave the gallery they're in).
    /// </summary>
    public static void DismissElevatorCallMessage() {
        instance.gameObject.SetActive(false);
        instance.dismissButton.gameObject.SetActive(false);
    }
}
