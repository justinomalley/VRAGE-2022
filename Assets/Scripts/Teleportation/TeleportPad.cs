using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// TeleportPad is a script for the yellow teleportation pads throughout VRAGE.
/// They communicate with TeleportPadManager to allow the user to navigate through space.
/// We use these pads instead of a more generalized teleport to have more control over where
/// the user can go, to keep their playspace centered in ideal positions.
///
/// I think this was a good idea at the time since it was most of our user's first time
/// trying VR, but I think a more generalized teleport would be better for an audience
/// familiar with VR. - JO
/// </summary>
public class TeleportPad : MonoBehaviour {
    
    /* State */
    
    [SerializeField] 
    private bool isCurrentPad;
    
    private bool activated = true;
    
    /* Tutorial */

    // These variables can cause teleporting this pad to dismiss certain tutorial steps.
    [SerializeField]
    private bool inFrontOfElevatorTutorialTrigger,
        elevatorCallerTutorialTrigger;
    
    /* Animation */

    private RendererAlphaFader fader;

    private float highlightedAlpha, unhighlightedAlpha;
    
    protected virtual void Awake() {
        fader = GetComponent<RendererAlphaFader>();
        TeleportPadManager.AddTeleportPad(this);
    }

    public void SetHighlightValues(float highlighted, float unhighlighted) {
        highlightedAlpha = highlighted;
        unhighlightedAlpha = unhighlighted;
    }

    public void FadeIn(float alpha, UnityAction action = null) {
        fader.Fade(alpha, action);
    }
    
    public void FadeOutAndDisable() {
        fader.Fade(0, () => {
            gameObject.SetActive(false); 
        });
    }

    public void Disable() {
        fader.CancelFade(0);
        gameObject.SetActive(false);
    }

    public bool IsCurrentPad() {
        return isCurrentPad;
    }
    
    public virtual void SetCurrentPad() {
        isCurrentPad = true;
        gameObject.SetActive(false);

        if (inFrontOfElevatorTutorialTrigger) {
            Tutorial.StepComplete(Tutorial.TutorialStep.TeleportToElevator);
        }

        if (elevatorCallerTutorialTrigger) {
            Tutorial.DisplayElevatorCallMessage();
        }
    }

    public void UnsetCurrentPad() {
        isCurrentPad = false;
    }

    public void Highlight() {
        // Cancel fade here just in case we're pointing at the panel before it has faded in
        fader.CancelFade(highlightedAlpha);
    }

    public void Unhighlight() {
        fader.SetValue(unhighlightedAlpha);
    }
    
    public void Activate() {
        activated = true;
    }
    
    public void Deactivate() {
        activated = false;
    }

    public bool IsActive() {
        return activated;
    }
}
