using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Vector3 = UnityEngine.Vector3;

/// <summary>
/// GenericXRButton tracks the pressed/unpressed status of a button using the new Unity Input System.
/// </summary>
public class DebugControls : MonoBehaviour {
    [SerializeField]
    private InputActionReference forwardActionRef, backwardActionRef, leftActionRef, rightActionRef;

    [SerializeField]
    private float moveSpeed = 10f;

    private Type lastActiveType;

    private void Update() {
        var forward = GetAction(forwardActionRef);
        var backward = GetAction(backwardActionRef);
        var left = GetAction(leftActionRef);
        var right = GetAction(rightActionRef);

        var builder = new StringBuilder();
        builder.Append(forward ? "W" : "");
        builder.Append(left ? "A" : "");
        builder.Append(backward ? "S" : "");
        builder.Append(right ? "D" : "");

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

    private bool GetAction(InputActionReference actionReference) {
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
