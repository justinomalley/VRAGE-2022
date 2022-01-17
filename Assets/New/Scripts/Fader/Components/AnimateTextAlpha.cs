using TMPro;
using UnityEngine;

public class AnimateTextAlpha : MonoBehaviour, IAnimatableComponent<float> {
    
    private TextMeshPro targetText;

    private void Awake() {
        if (targetText != null) {
            return;
        }
        
        targetText = GetComponent<TextMeshPro>();
        if (targetText != null) {
            return;
        }
        
        Debug.LogError("Attempting to animate text, but no TextMeshPro component selected!");
    }

    public void Set(float property) {
        var currColor = targetText.color;
        targetText.color = new Color(currColor.r, currColor.g, currColor.b, property);
    }
}
