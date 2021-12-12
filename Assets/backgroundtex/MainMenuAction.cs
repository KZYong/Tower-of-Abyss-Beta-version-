// GENERATED AUTOMATICALLY FROM 'Assets/backgroundtex/MainMenuAction.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine;

public class @MainMenuAction : MonoBehaviour
{
    public InputActionAsset asset { get; }
    public @MainMenuAction()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""MainMenuAction"",
    ""maps"": [
        {
            ""name"": ""MainMenu"",
            ""id"": ""19e3d0ac-14d6-4cce-b80b-342cd41a9fe3"",
            ""actions"": [
                {
                    ""name"": ""PressAnyKey"",
                    ""type"": ""Value"",
                    ""id"": ""c959443d-4375-488c-8bce-29894c646c77"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""44584445-8af8-41c6-b62b-e7b95c4620a5"",
                    ""path"": ""<Keyboard>/anyKey"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PressAnyKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f757a310-baf6-477c-b6a0-a2c4730dd388"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PressAnyKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // MainMenu
        m_MainMenu = asset.FindActionMap("MainMenu", throwIfNotFound: true);
        m_MainMenu_PressAnyKey = m_MainMenu.FindAction("PressAnyKey", throwIfNotFound: true);
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

 //   IEnumerator IEnumerable.GetEnumerator()
  //  {
   //     return GetEnumerator();
  //  }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // MainMenu
    private readonly InputActionMap m_MainMenu;
    private IMainMenuActions m_MainMenuActionsCallbackInterface;
    private readonly InputAction m_MainMenu_PressAnyKey;
    public struct MainMenuActions
    {
        private @MainMenuAction m_Wrapper;
        public MainMenuActions(@MainMenuAction wrapper) { m_Wrapper = wrapper; }
        public InputAction @PressAnyKey => m_Wrapper.m_MainMenu_PressAnyKey;
        public InputActionMap Get() { return m_Wrapper.m_MainMenu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MainMenuActions set) { return set.Get(); }
        public void SetCallbacks(IMainMenuActions instance)
        {
            if (m_Wrapper.m_MainMenuActionsCallbackInterface != null)
            {
                @PressAnyKey.started -= m_Wrapper.m_MainMenuActionsCallbackInterface.OnPressAnyKey;
                @PressAnyKey.performed -= m_Wrapper.m_MainMenuActionsCallbackInterface.OnPressAnyKey;
                @PressAnyKey.canceled -= m_Wrapper.m_MainMenuActionsCallbackInterface.OnPressAnyKey;
            }
            m_Wrapper.m_MainMenuActionsCallbackInterface = instance;
            if (instance != null)
            {
                @PressAnyKey.started += instance.OnPressAnyKey;
                @PressAnyKey.performed += instance.OnPressAnyKey;
                @PressAnyKey.canceled += instance.OnPressAnyKey;
            }
        }
    }
    public MainMenuActions @MainMenu => new MainMenuActions(this);
    public interface IMainMenuActions
    {
        void OnPressAnyKey(InputAction.CallbackContext context);
    }
}
