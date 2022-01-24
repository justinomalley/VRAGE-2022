// GENERATED AUTOMATICALLY FROM 'Assets/Debug/DebugActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @DebugActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @DebugActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""DebugActions"",
    ""maps"": [
        {
            ""name"": ""Keyboard"",
            ""id"": ""e3fc82a4-58db-45fb-b914-19d7cb01d913"",
            ""actions"": [
                {
                    ""name"": ""Forward"",
                    ""type"": ""Button"",
                    ""id"": ""b09321a6-936b-4ae3-b50e-e4dfcd3ca3d6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Backward"",
                    ""type"": ""Button"",
                    ""id"": ""b02d0ea0-c12f-492d-b61f-629d08ec1889"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Left"",
                    ""type"": ""Button"",
                    ""id"": ""0f120a61-43fd-46c8-815e-71f94f55884a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Right"",
                    ""type"": ""Button"",
                    ""id"": ""40f64a58-5a85-4b4f-a505-dd14faab0c58"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""7cd3838b-4cbe-446a-b6ff-1e6973e7acbb"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Forward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6b992947-d8e6-4a86-ac5e-977b84a9a040"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Backward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""df2f12f9-b8c9-457a-912f-92559d0915c5"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""eb557873-cbe1-4089-80f8-37b119639a62"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""XR Usage"",
            ""bindingGroup"": ""XR Usage"",
            ""devices"": [
                {
                    ""devicePath"": ""<XRController>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Keyboard
        m_Keyboard = asset.FindActionMap("Keyboard", throwIfNotFound: true);
        m_Keyboard_Forward = m_Keyboard.FindAction("Forward", throwIfNotFound: true);
        m_Keyboard_Backward = m_Keyboard.FindAction("Backward", throwIfNotFound: true);
        m_Keyboard_Left = m_Keyboard.FindAction("Left", throwIfNotFound: true);
        m_Keyboard_Right = m_Keyboard.FindAction("Right", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Keyboard
    private readonly InputActionMap m_Keyboard;
    private IKeyboardActions m_KeyboardActionsCallbackInterface;
    private readonly InputAction m_Keyboard_Forward;
    private readonly InputAction m_Keyboard_Backward;
    private readonly InputAction m_Keyboard_Left;
    private readonly InputAction m_Keyboard_Right;
    public struct KeyboardActions
    {
        private @DebugActions m_Wrapper;
        public KeyboardActions(@DebugActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Forward => m_Wrapper.m_Keyboard_Forward;
        public InputAction @Backward => m_Wrapper.m_Keyboard_Backward;
        public InputAction @Left => m_Wrapper.m_Keyboard_Left;
        public InputAction @Right => m_Wrapper.m_Keyboard_Right;
        public InputActionMap Get() { return m_Wrapper.m_Keyboard; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(KeyboardActions set) { return set.Get(); }
        public void SetCallbacks(IKeyboardActions instance)
        {
            if (m_Wrapper.m_KeyboardActionsCallbackInterface != null)
            {
                @Forward.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnForward;
                @Forward.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnForward;
                @Forward.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnForward;
                @Backward.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnBackward;
                @Backward.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnBackward;
                @Backward.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnBackward;
                @Left.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnLeft;
                @Left.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnLeft;
                @Left.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnLeft;
                @Right.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnRight;
                @Right.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnRight;
                @Right.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnRight;
            }
            m_Wrapper.m_KeyboardActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Forward.started += instance.OnForward;
                @Forward.performed += instance.OnForward;
                @Forward.canceled += instance.OnForward;
                @Backward.started += instance.OnBackward;
                @Backward.performed += instance.OnBackward;
                @Backward.canceled += instance.OnBackward;
                @Left.started += instance.OnLeft;
                @Left.performed += instance.OnLeft;
                @Left.canceled += instance.OnLeft;
                @Right.started += instance.OnRight;
                @Right.performed += instance.OnRight;
                @Right.canceled += instance.OnRight;
            }
        }
    }
    public KeyboardActions @Keyboard => new KeyboardActions(this);
    private int m_XRUsageSchemeIndex = -1;
    public InputControlScheme XRUsageScheme
    {
        get
        {
            if (m_XRUsageSchemeIndex == -1) m_XRUsageSchemeIndex = asset.FindControlSchemeIndex("XR Usage");
            return asset.controlSchemes[m_XRUsageSchemeIndex];
        }
    }
    public interface IKeyboardActions
    {
        void OnForward(InputAction.CallbackContext context);
        void OnBackward(InputAction.CallbackContext context);
        void OnLeft(InputAction.CallbackContext context);
        void OnRight(InputAction.CallbackContext context);
    }
}
