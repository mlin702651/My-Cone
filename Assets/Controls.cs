// GENERATED AUTOMATICALLY FROM 'Assets/Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Controls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""player"",
            ""id"": ""338eac3e-3edf-4228-b5e2-dca94b820967"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""af7f7283-ea21-4b8c-a08b-50309bf94fce"",
                    ""expectedControlType"": ""Stick"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""HandMove"",
                    ""type"": ""Value"",
                    ""id"": ""3bd03aed-91bf-43dc-b3ba-28baded7baa6"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""46db0bec-0aed-4889-8074-65db44edb337"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SwitchWeaponPlus"",
                    ""type"": ""Button"",
                    ""id"": ""80a79cd6-26a1-41e4-ac12-e2a2e1ade392"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SwitchWeaponLess"",
                    ""type"": ""Button"",
                    ""id"": ""9529684a-170c-4901-b52e-6cb816008a0d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""4e184ad7-87af-4302-b8a5-470842f961f2"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HandMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bb196511-a76d-4669-bf85-c9d031721788"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fc1a9dda-0f90-46da-bcb1-f0c7984bb1e0"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchWeaponPlus"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""620975d3-2c71-47f4-b4bf-a852df5f85e3"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0b7a1ba0-145e-4641-a459-384a85bb2c22"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchWeaponLess"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // player
        m_player = asset.FindActionMap("player", throwIfNotFound: true);
        m_player_Move = m_player.FindAction("Move", throwIfNotFound: true);
        m_player_HandMove = m_player.FindAction("HandMove", throwIfNotFound: true);
        m_player_Shoot = m_player.FindAction("Shoot", throwIfNotFound: true);
        m_player_SwitchWeaponPlus = m_player.FindAction("SwitchWeaponPlus", throwIfNotFound: true);
        m_player_SwitchWeaponLess = m_player.FindAction("SwitchWeaponLess", throwIfNotFound: true);
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

    // player
    private readonly InputActionMap m_player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_player_Move;
    private readonly InputAction m_player_HandMove;
    private readonly InputAction m_player_Shoot;
    private readonly InputAction m_player_SwitchWeaponPlus;
    private readonly InputAction m_player_SwitchWeaponLess;
    public struct PlayerActions
    {
        private @Controls m_Wrapper;
        public PlayerActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_player_Move;
        public InputAction @HandMove => m_Wrapper.m_player_HandMove;
        public InputAction @Shoot => m_Wrapper.m_player_Shoot;
        public InputAction @SwitchWeaponPlus => m_Wrapper.m_player_SwitchWeaponPlus;
        public InputAction @SwitchWeaponLess => m_Wrapper.m_player_SwitchWeaponLess;
        public InputActionMap Get() { return m_Wrapper.m_player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @HandMove.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHandMove;
                @HandMove.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHandMove;
                @HandMove.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHandMove;
                @Shoot.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShoot;
                @SwitchWeaponPlus.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwitchWeaponPlus;
                @SwitchWeaponPlus.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwitchWeaponPlus;
                @SwitchWeaponPlus.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwitchWeaponPlus;
                @SwitchWeaponLess.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwitchWeaponLess;
                @SwitchWeaponLess.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwitchWeaponLess;
                @SwitchWeaponLess.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwitchWeaponLess;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @HandMove.started += instance.OnHandMove;
                @HandMove.performed += instance.OnHandMove;
                @HandMove.canceled += instance.OnHandMove;
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
                @SwitchWeaponPlus.started += instance.OnSwitchWeaponPlus;
                @SwitchWeaponPlus.performed += instance.OnSwitchWeaponPlus;
                @SwitchWeaponPlus.canceled += instance.OnSwitchWeaponPlus;
                @SwitchWeaponLess.started += instance.OnSwitchWeaponLess;
                @SwitchWeaponLess.performed += instance.OnSwitchWeaponLess;
                @SwitchWeaponLess.canceled += instance.OnSwitchWeaponLess;
            }
        }
    }
    public PlayerActions @player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnHandMove(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
        void OnSwitchWeaponPlus(InputAction.CallbackContext context);
        void OnSwitchWeaponLess(InputAction.CallbackContext context);
    }
}
