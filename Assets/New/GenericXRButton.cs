using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

/// <summary>
/// GenericXRButton tracks the pressed/unpressed status of a button using the new Unity Input System.
/// </summary>
public class GenericXRButton : MonoBehaviour {
    [SerializeField]
    private InputActionReference m_ActionReference;
    
    private InputActionReference actionReference { get => m_ActionReference ; set => m_ActionReference = value; }

    private bool pressed, pressedLastFrame;
    
    private Type lastActiveType;
    
    private UnityEvent buttonPressedEvent { get; } = new UnityEvent();
    private UnityEvent buttonUnpressedEvent { get; } = new UnityEvent();

    private void Update() {
        if (actionReference == null 
            || actionReference.action == null 
            || !actionReference.action.enabled 
            || actionReference.action.controls.Count <= 0) {
            return;
        }
        
        var typeToUse = actionReference.action.activeControl != null 
            ? actionReference.action.activeControl.valueType : lastActiveType;

        if (typeToUse == typeof(bool)) {
            lastActiveType = typeof(bool);
            pressed = actionReference.action.ReadValue<bool>();
        } else if (typeToUse == typeof(float)) {
            lastActiveType = typeof(float);
            var value = actionReference.action.ReadValue<float>();
            pressed = value > 0.5;
        }

        switch (pressed) {
            // If button was pressed this frame...
            case true when !pressedLastFrame:
                ButtonPressed();
                break;
            // If button was released this frame...
            case false when pressedLastFrame:
                ButtonUnpressed();
                break;
        }

        pressedLastFrame = pressed;
    }

    private void ButtonPressed() {
        buttonPressedEvent.Invoke();
    }
    
    private void ButtonUnpressed() {
        buttonUnpressedEvent.Invoke();
    }

    public void AddButtonPressedListener(UnityAction action) {
        buttonPressedEvent.AddListener(action);
    }

    public void RemoveButtonPressedListener(UnityAction action) {
        buttonPressedEvent.RemoveListener(action);
    }

    public void AddButtonUnpressedListener(UnityAction action) {
        buttonUnpressedEvent.AddListener(action);
    }
    
    public void RemoveButtonUnpressedListener(UnityAction action) {
        buttonUnpressedEvent.RemoveListener(action);
    }

    public bool IsPressed() {
        return pressed;
    }
}
