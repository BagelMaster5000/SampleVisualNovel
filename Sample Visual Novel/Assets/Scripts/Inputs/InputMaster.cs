// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Inputs/InputMaster.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputMaster : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputMaster()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMaster"",
    ""maps"": [
        {
            ""name"": ""InGame"",
            ""id"": ""c9d00ab2-cfac-448e-a0ec-30cff73aa042"",
            ""actions"": [
                {
                    ""name"": ""Advance Dialog"",
                    ""type"": ""Button"",
                    ""id"": ""5cde1b71-a998-4475-8ab7-ad8aa28e5474"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Fast Forward"",
                    ""type"": ""Button"",
                    ""id"": ""7026cfc8-2105-4a63-abbe-a5226643e7ef"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""76e32a76-95a6-43ad-aab0-1bbcbfc28b14"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse + Keyboard"",
                    ""action"": ""Advance Dialog"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""26791ff6-025d-4b52-86a1-c36290c1ab6e"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse + Keyboard"",
                    ""action"": ""Advance Dialog"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9afc3c75-c953-42d7-81b2-dde4302b547e"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fast Forward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e036f585-1305-4954-8216-f0bfec550c51"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fast Forward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cbb034ee-f62c-4304-9285-c2d573905e28"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fast Forward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Mouse + Keyboard"",
            ""bindingGroup"": ""Mouse + Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // InGame
        m_InGame = asset.FindActionMap("InGame", throwIfNotFound: true);
        m_InGame_AdvanceDialog = m_InGame.FindAction("Advance Dialog", throwIfNotFound: true);
        m_InGame_FastForward = m_InGame.FindAction("Fast Forward", throwIfNotFound: true);
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

    // InGame
    private readonly InputActionMap m_InGame;
    private IInGameActions m_InGameActionsCallbackInterface;
    private readonly InputAction m_InGame_AdvanceDialog;
    private readonly InputAction m_InGame_FastForward;
    public struct InGameActions
    {
        private @InputMaster m_Wrapper;
        public InGameActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @AdvanceDialog => m_Wrapper.m_InGame_AdvanceDialog;
        public InputAction @FastForward => m_Wrapper.m_InGame_FastForward;
        public InputActionMap Get() { return m_Wrapper.m_InGame; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InGameActions set) { return set.Get(); }
        public void SetCallbacks(IInGameActions instance)
        {
            if (m_Wrapper.m_InGameActionsCallbackInterface != null)
            {
                @AdvanceDialog.started -= m_Wrapper.m_InGameActionsCallbackInterface.OnAdvanceDialog;
                @AdvanceDialog.performed -= m_Wrapper.m_InGameActionsCallbackInterface.OnAdvanceDialog;
                @AdvanceDialog.canceled -= m_Wrapper.m_InGameActionsCallbackInterface.OnAdvanceDialog;
                @FastForward.started -= m_Wrapper.m_InGameActionsCallbackInterface.OnFastForward;
                @FastForward.performed -= m_Wrapper.m_InGameActionsCallbackInterface.OnFastForward;
                @FastForward.canceled -= m_Wrapper.m_InGameActionsCallbackInterface.OnFastForward;
            }
            m_Wrapper.m_InGameActionsCallbackInterface = instance;
            if (instance != null)
            {
                @AdvanceDialog.started += instance.OnAdvanceDialog;
                @AdvanceDialog.performed += instance.OnAdvanceDialog;
                @AdvanceDialog.canceled += instance.OnAdvanceDialog;
                @FastForward.started += instance.OnFastForward;
                @FastForward.performed += instance.OnFastForward;
                @FastForward.canceled += instance.OnFastForward;
            }
        }
    }
    public InGameActions @InGame => new InGameActions(this);
    private int m_MouseKeyboardSchemeIndex = -1;
    public InputControlScheme MouseKeyboardScheme
    {
        get
        {
            if (m_MouseKeyboardSchemeIndex == -1) m_MouseKeyboardSchemeIndex = asset.FindControlSchemeIndex("Mouse + Keyboard");
            return asset.controlSchemes[m_MouseKeyboardSchemeIndex];
        }
    }
    public interface IInGameActions
    {
        void OnAdvanceDialog(InputAction.CallbackContext context);
        void OnFastForward(InputAction.CallbackContext context);
    }
}
