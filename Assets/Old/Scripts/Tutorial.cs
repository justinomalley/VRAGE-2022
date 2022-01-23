using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {
    [SerializeField]
    private AudioClip[] instructionSound;
    
    [SerializeField]
    private Sprite[] instructionSprites;

    [SerializeField]
    private Transform tutorialAnchor;

    private AudioSource audioSource;

    private Image image;

    private static TutorialStep currentStep;

    private static Tutorial instance;

    public enum TutorialStep {
        ActivatePointer,
        FirstTeleport,
        InteractCube,
        TeleportToElevator,
        GoInsideElevator,
        EnterFirstGallery,
    }

    private void Awake() {
        transform.SetPositionAndRotation(tutorialAnchor.position, tutorialAnchor.rotation);
        transform.SetParent(tutorialAnchor);
        audioSource = GetComponent<AudioSource>();
        image = GetComponentInChildren<Image>();
        instance = this;
        
        gameObject.SetActive(false);
    }

    public static void StartTutorial() {
        instance.gameObject.SetActive(true);
        instance.StartStep(TutorialStep.ActivatePointer);
    }

    public static void StepComplete(TutorialStep step) {
        if (instance == null || step < currentStep) {
            return;
        }

        if (step >= TutorialStep.EnterFirstGallery) {
            instance.audioSource.Stop();
            Destroy(instance.gameObject);
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
}
