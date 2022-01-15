using UnityEngine;

/// <summary>
/// Fader fades the targetMaterial from `origColor` to `destColor` over `fadeTime` seconds
/// when `fadeIn` is set to true (either through `FadeIn()` or by setting `fadeIn` to true
/// in the editor).
/// </summary>
public class ColorFader : MonoBehaviour {
    
    [SerializeField]
    private Color destColor;
    
    private Color origColor;
    
    [SerializeField]
    private bool fadeIn;

    [SerializeField]
    private Material targetMaterial;
    
    [SerializeField]
    private float fadeTime = 1f;
    
    private float timer, startTime;

    private void Awake() {
        origColor = targetMaterial.color;
        startTime = Time.time;
    }
    
	private void Update () {
        if (!fadeIn) {
            return;
        }

        if (timer < fadeTime) {
            timer = Time.time - startTime;
            targetMaterial.color = Color.Lerp(origColor, destColor, timer / fadeTime);
        } else {
            fadeIn = false;
            timer = 0;
        }
    }

    public void FadeIn() {
        startTime = Time.time;
        fadeIn = true;
    }

    private void ResetMaterial() {
        targetMaterial.color = origColor;
    }

    private void OnApplicationQuit() {
        ResetMaterial();
    }
}
