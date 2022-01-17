using UnityEngine;

public class AnimateMaterialAlpha : MonoBehaviour, IAnimatableComponent<float> {
    
    private Material targetMaterial;

    private void Awake() {
        if (targetMaterial != null) {
            return;
        }
        
        targetMaterial = GetComponent<Renderer>()?.material;
        if (targetMaterial != null) {
            return;
        }
        
        Debug.LogError("Attempting to animate material, but no material selected!");
    }

    public void Set(float property) {
        var currColor = targetMaterial.color;
        targetMaterial.color = new Color(currColor.r, currColor.g, currColor.b, property);
    }
}
