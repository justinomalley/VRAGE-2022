using UnityEngine;

public class AnimateMaterialAlpha : MonoBehaviour, IAnimatableComponent<float> {
    [SerializeField]
    private Material targetMaterial;

    private Color origColor;

    private void Awake() {
        if (targetMaterial != null) {
            origColor = targetMaterial.color;
            return;
        }
        
        targetMaterial = GetComponent<Renderer>()?.material;
        if (targetMaterial != null) {
            origColor = targetMaterial.color;
            return;
        }
        
        Debug.LogError("Attempting to animate material, but no material selected!");
    }

    public void Set(float property) {
        var currColor = targetMaterial.color;
        targetMaterial.color = new Color(currColor.r, currColor.g, currColor.b, property);
    }
    
    private void OnApplicationQuit() {
        if (targetMaterial != null) {
            targetMaterial.color = origColor;
        }
    }
}
