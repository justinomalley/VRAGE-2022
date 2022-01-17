using UnityEngine;

public class AnimateRendererAlpha : MonoBehaviour, IAnimatableComponent<float> {
    
    // targetRenderer allows us to use a renderer instead of a material so we can change material properties
    // without duplicating the associated material. Used in cases where multiple objects that share the same
    // material need different color values.
    private Renderer targetRenderer;
    
    // _propBlock is used to change properties of material in renderer without changing other instances or duplicating.
    private MaterialPropertyBlock _propBlock;
    
    // ColorProperty is used to access material property in MaterialPropertyBlock
    private static readonly int ColorProperty = Shader.PropertyToID("_Color");

    private void Start() {
        targetRenderer = GetComponent<Renderer>();
        if (targetRenderer == null) {
            Debug.LogError("Attempting to animate renderer, but no renderer selected!");
        }
        
        _propBlock = new MaterialPropertyBlock();
        // Get the current value of the material instance for this renderer.
        targetRenderer.GetPropertyBlock(_propBlock);
    }

    public void Set(float property) {
        var currColor = _propBlock.GetColor(ColorProperty);
        _propBlock.SetColor(ColorProperty, new Color(currColor.r, currColor.g, currColor.b, property));
        targetRenderer.SetPropertyBlock(_propBlock);
    }
}
