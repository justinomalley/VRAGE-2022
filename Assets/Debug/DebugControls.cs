using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector3 = UnityEngine.Vector3;

/// <summary>
/// DebugControls allows us to move around the playspace using WASD on the keyboard, so that
/// I don't have to jump all over the place during development :-)
/// </summary>
public class DebugControls : MonoBehaviour {
    [SerializeField]
    private InputActionReference forwardActionRef, backwardActionRef, leftActionRef, rightActionRef;

    [SerializeField]
    private float moveSpeed = 0.01f;

    [SerializeField]
    private bool debugControlsActive;

    private Type lastActiveType;

    private void Update() {
        if (!debugControlsActive) {
            return;
        }
        
        var forward = GetButtonDown(forwardActionRef);
        var backward = GetButtonDown(backwardActionRef);
        var left = GetButtonDown(leftActionRef);
        var right = GetButtonDown(rightActionRef);

        var movementVector = new Vector3();

        if (forward && !backward) {
            movementVector += Vector3.forward;
        }

        if (backward && !forward) {
            movementVector += Vector3.back;
        }

        if (left && !right) {
            movementVector += Vector3.left;
        }

        if (right && !left) {
            movementVector += Vector3.right;
        }
        
        transform.Translate(movementVector.normalized * moveSpeed);
    }

    private bool GetButtonDown(InputActionReference actionReference) {
        if (actionReference == null 
            || actionReference.action == null 
            || !actionReference.action.enabled 
            || actionReference.action.controls.Count <= 0) {
            return false;
        }
        
        var typeToUse = actionReference.action.activeControl != null 
            ? actionReference.action.activeControl.valueType : lastActiveType;

        if (typeToUse == typeof(bool)) {
            lastActiveType = typeof(bool);
            return actionReference.action.ReadValue<bool>();
        }

        if (typeToUse == typeof(float)) {
            lastActiveType = typeof(float);
            var value = actionReference.action.ReadValue<float>();
            return value > 0.5;
        }
        
        return false;
    }
}
