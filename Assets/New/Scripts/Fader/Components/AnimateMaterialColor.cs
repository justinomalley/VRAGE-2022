using UnityEngine;

public class AnimateMaterialColor : MonoBehaviour, IAnimatableComponent<Color> {
    
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

    public void Set(Color property) {
        targetMaterial.color = property;
    }
}
