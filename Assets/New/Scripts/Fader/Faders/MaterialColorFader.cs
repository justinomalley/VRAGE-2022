using System;
using UnityEngine;

[RequireComponent(typeof(AnimateMaterialColor))]
public class MaterialColorFader : Fader<Color> {
    [SerializeField] 
    private Color startColor, endColor;
    
    [SerializeField]
    private float fadeDuration;

    private AnimateMaterialColor materialAnimator;

    private readonly AnimatableColor animProgressEvaluator = new AnimatableColor();
    
    private void Start() {
        materialAnimator = GetComponent<AnimateMaterialColor>();
        Initialize(materialAnimator, animProgressEvaluator, startColor, endColor, fadeDuration);
    }
}
