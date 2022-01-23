using System;
using UnityEngine;

public class AnimateMaterialColor : MonoBehaviour, IAnimatableComponent<Color> {
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

    public void Set(Color property) {
        targetMaterial.color = property;
    }

    private void OnDestroy() {
        if (targetMaterial != null) {
            targetMaterial.color = origColor;
        }
    }

    private void OnApplicationQuit() {
        if (targetMaterial != null) {
            targetMaterial.color = origColor;
        }
    }
}
