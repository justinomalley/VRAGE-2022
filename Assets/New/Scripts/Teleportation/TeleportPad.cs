using UnityEngine;

public class TeleportPad : MonoBehaviour {

    [SerializeField] 
    private bool isCurrentPad;

    private RendererAlphaFader fader;

    private float highlightedAlpha, unhighlightedAlpha;

    public void Initialize(float highlighted, float unhighlighted) {
        highlightedAlpha = highlighted;
        unhighlightedAlpha = unhighlighted;
        fader = GetComponent<RendererAlphaFader>();
    }

    public void FadeIn(float alpha) {
        fader.Fade(alpha);
    }

    // `action` will be executed when (and if) the fade is complete.
    public void FadeOutAndDisable() {
        fader.AddCallback(() => {
            gameObject.SetActive(false);
        });
        fader.Fade(0);
    }

    public void Disable() {
        fader.CancelFade(0);
        gameObject.SetActive(false);
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

    public void Highlight() {
        // Cancel fade here just in case we're pointing at the panel before it has faded in
        fader.CancelFade(highlightedAlpha);
    }

    public void Unhighlight() {
        fader.SetValue(unhighlightedAlpha);
    }
}
