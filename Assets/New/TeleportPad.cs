using UnityEngine;
using UnityEngine.Events;

public class TeleportPad : MonoBehaviour {

    [SerializeField] 
    private bool isCurrentPad;

    private RendererAlphaFader fader;

    public void Initialize() {
        fader = GetComponent<RendererAlphaFader>();
        // if (isStartPad) {
        //     gameObject.SetActive(false);
        // }
    }

    public void FadeIn(float alpha) {
        if (isCurrentPad) {
            return;
        }
        fader.Fade(alpha);
    }

    // `action` will be executed when (and if) the fade is complete.
    public void FadeOut(UnityAction action) {
        if (isCurrentPad) {
            return;
        }
        fader.AddCallback(action);
        fader.Fade(0);
    }
}
