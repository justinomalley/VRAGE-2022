using UnityEngine;
using UnityEngine.Events;

public class TeleportPad : MonoBehaviour {

    [SerializeField] 
    private bool isCurrentPad;

    private RendererAlphaFader fader;

    public void Initialize() {
        fader = GetComponent<RendererAlphaFader>();
    }

    public void FadeIn(float alpha) {
        fader.Fade(alpha);
    }

    // `action` will be executed when (and if) the fade is complete.
    public void FadeOut(UnityAction action) {
        fader.AddCallback(action);
        fader.Fade(0);
    }

    public bool IsCurrentPad() {
        return isCurrentPad;
    }
    
    public void SetCurrentPad() {
        isCurrentPad = true;
        gameObject.SetActive(false);
    }

    public void UnsetCurrentPad() {
        isCurrentPad = false;
    }
}
