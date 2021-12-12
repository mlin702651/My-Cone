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
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""c8b37344-1901-41d0-95dc-d1b36cf74919"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Charge"",
                    ""type"": ""Button"",
                    ""id"": ""8ecf1c1a-9863-4381-889a-9cfbe33eeb7c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CameraMove"",
                    ""type"": ""Value"",
                    ""id"": ""eb3815d8-55f8-4cbe-8206-a3d4d55e81f1"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Aim"",
                    ""type"": ""Button"",
                    ""id"": ""7a6dfe32-bdfa-4007-8232-bc26b69b9559"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""08872972-001f-4a9b-bb5c-0fd44733a82d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Talk"",
                    ""type"": ""Button"",
                    ""id"": ""37255d8c-35c3-4979-9472-4feebece740b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""AimCameraMove"",
                    ""type"": ""Button"",
                    ""id"": ""3666357c-85b9-414a-bba7-6c24dbca9c66"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""bb196511-a76d-4669-bf85-c9d031721788"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""aaf3c89b-23ab-4d2f-af39-889b8a364af8"",
                    ""path"": ""<Mouse>/leftButton"",
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
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchWeaponPlus"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""da28ad30-addc-4b24-b859-6bd0eec91b3b"",
                    ""path"": ""<Keyboard>/e"",
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
                    ""name"": ""2D Vector"",
                    ""id"": ""28b712da-31ef-4ae0-9822-406cc2d60214"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""2ead5ccc-fc19-473a-8e4a-73f58e6801bf"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""ba06934c-30a7-4ed2-afbc-59bec8277bd0"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""a43fc19e-2747-4503-9a99-e6d73c007f3b"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""f29811aa-c69a-4aa7-a28d-8488fe59a3a3"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
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
                },
                {
                    ""name"": """",
                    ""id"": ""91e701dc-c8eb-4720-8853-fd15c290e7ca"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchWeaponLess"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c16e5c9b-fa80-4c89-a95f-82a05078c2f6"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d7e7291d-8f7c-4dae-be08-2b6e99984f08"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5c0e1c00-6f4a-42b6-a961-924ad11932b4"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Charge"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6fa19468-fe8e-49e8-ad29-8244ef6fd271"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": ""InvertVector2(invertY=false),ScaleVector2(x=0.45,y=0.4),StickDeadzone"",
                    ""groups"": """",
                    ""action"": ""CameraMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d9c83e9d-ef33-4465-a4fd-2b8286624078"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": ""InvertVector2(invertY=false),ScaleVector2(x=0.1,y=0.01)"",
                    ""groups"": """",
                    ""action"": ""CameraMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""96ec73c1-e58c-4e31-878e-d18d0e6dacc2"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""82a3074d-87ce-465a-a7da-56ec19405c1f"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8e5ca3d6-7835-405e-bdef-47b95bc65645"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d885e44e-4b53-4a94-a46a-d9d839e14507"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Talk"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1e432623-b3cd-4d6d-83c2-f327ca827eee"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Talk"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""be25ba11-c74d-48a1-ac49-1a776c98efd8"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": ""InvertVector2(invertY=false)"",
                    ""groups"": """",
                    ""action"": ""AimCameraMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0ef52e69-070f-4205-be47-b246ebfa866d"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": ""InvertVector2(invertY=false),ScaleVector2(x=0.2,y=0.2)"",
                    ""groups"": """",
                    ""action"": ""AimCameraMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Menu"",
            ""id"": ""01d69666-7ae1-4530-b088-8fadd380130b"",
            ""actions"": [
                {
                    ""name"": ""MenuSelectRight"",
                    ""type"": ""Button"",
                    ""id"": ""c5b9df8c-0b14-4964-8d64-5693d45851e6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MenuSelectLeft"",
                    ""type"": ""Button"",
                    ""id"": ""9078dd27-a21c-4d8e-a0af-44d8006fde3e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MenuSelectDown"",
                    ""type"": ""Button"",
                    ""id"": ""18b9946c-6a80-43e9-93da-b83f943260d0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MenuSelectUp"",
                    ""type"": ""Button"",
                    ""id"": ""648f0d69-b41f-4c08-bc17-9633ac24c2ad"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Menu"",
                    ""type"": ""Button"",
                    ""id"": ""308f1451-6744-44f0-83bc-31c97b7ac465"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MenuConfirm"",
                    ""type"": ""Button"",
                    ""id"": ""bc2b980b-4945-4730-a62d-496b065a4caa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""8ee1cba9-e4dd-43e8-b591-673f8e78f2dd"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""aca13d94-ac25-4509-921d-b1c39cdc4605"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MenuSelectUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""86156e6b-8bf1-4e0b-8431-3277e15d8a8e"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MenuSelectDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""df86ac09-c822-4b49-962f-690b675a4b90"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MenuSelectLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ac29558a-ac83-495c-909b-a0d846066930"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MenuSelectRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""06fa19e8-598f-47f2-aa26-6c28611eb67d"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MenuConfirm"",
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
        m_player_Shoot = m_player.FindAction("Shoot", throwIfNotFound: true);
        m_player_SwitchWeaponPlus = m_player.FindAction("SwitchWeaponPlus", throwIfNotFound: true);
        m_player_SwitchWeaponLess = m_player.FindAction("SwitchWeaponLess", throwIfNotFound: true);
        m_player_Jump = m_player.FindAction("Jump", throwIfNotFound: true);
        m_player_Charge = m_player.FindAction("Charge", throwIfNotFound: true);
        m_player_CameraMove = m_player.FindAction("CameraMove", throwIfNotFound: true);
        m_player_Aim = m_player.FindAction("Aim", throwIfNotFound: true);
        m_player_Dash = m_player.FindAction("Dash", throwIfNotFound: true);
        m_player_Talk = m_player.FindAction("Talk", throwIfNotFound: true);
        m_player_AimCameraMove = m_player.FindAction("AimCameraMove", throwIfNotFound: true);
        // Menu
        m_Menu = asset.FindActionMap("Menu", throwIfNotFound: true);
        m_Menu_MenuSelectRight = m_Menu.FindAction("MenuSelectRight", throwIfNotFound: true);
        m_Menu_MenuSelectLeft = m_Menu.FindAction("MenuSelectLeft", throwIfNotFound: true);
        m_Menu_MenuSelectDown = m_Menu.FindAction("MenuSelectDown", throwIfNotFound: true);
        m_Menu_MenuSelectUp = m_Menu.FindAction("MenuSelectUp", throwIfNotFound: true);
        m_Menu_Menu = m_Menu.FindAction("Menu", throwIfNotFound: true);
        m_Menu_MenuConfirm = m_Menu.FindAction("MenuConfirm", throwIfNotFound: true);
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
    private readonly InputAction m_player_Shoot;
    private readonly InputAction m_player_SwitchWeaponPlus;
    private readonly InputAction m_player_SwitchWeaponLess;
    private readonly InputAction m_player_Jump;
    private readonly InputAction m_player_Charge;
    private readonly InputAction m_player_CameraMove;
    private readonly InputAction m_player_Aim;
    private readonly InputAction m_player_Dash;
    private readonly InputAction m_player_Talk;
    private readonly InputAction m_player_AimCameraMove;
    public struct PlayerActions
    {
        private @Controls m_Wrapper;
        public PlayerActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_player_Move;
        public InputAction @Shoot => m_Wrapper.m_player_Shoot;
        public InputAction @SwitchWeaponPlus => m_Wrapper.m_player_SwitchWeaponPlus;
        public InputAction @SwitchWeaponLess => m_Wrapper.m_player_SwitchWeaponLess;
        public InputAction @Jump => m_Wrapper.m_player_Jump;
        public InputAction @Charge => m_Wrapper.m_player_Charge;
        public InputAction @CameraMove => m_Wrapper.m_player_CameraMove;
        public InputAction @Aim => m_Wrapper.m_player_Aim;
        public InputAction @Dash => m_Wrapper.m_player_Dash;
        public InputAction @Talk => m_Wrapper.m_player_Talk;
        public InputAction @AimCameraMove => m_Wrapper.m_player_AimCameraMove;
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
                @Shoot.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShoot;
                @SwitchWeaponPlus.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwitchWeaponPlus;
                @SwitchWeaponPlus.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwitchWeaponPlus;
                @SwitchWeaponPlus.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwitchWeaponPlus;
                @SwitchWeaponLess.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwitchWeaponLess;
                @SwitchWeaponLess.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwitchWeaponLess;
                @SwitchWeaponLess.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwitchWeaponLess;
                @Jump.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Charge.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCharge;
                @Charge.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCharge;
                @Charge.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCharge;
                @CameraMove.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCameraMove;
                @CameraMove.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCameraMove;
                @CameraMove.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCameraMove;
                @Aim.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAim;
                @Aim.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAim;
                @Aim.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAim;
                @Dash.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDash;
                @Dash.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDash;
                @Dash.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDash;
                @Talk.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTalk;
                @Talk.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTalk;
                @Talk.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTalk;
                @AimCameraMove.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAimCameraMove;
                @AimCameraMove.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAimCameraMove;
                @AimCameraMove.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAimCameraMove;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
                @SwitchWeaponPlus.started += instance.OnSwitchWeaponPlus;
                @SwitchWeaponPlus.performed += instance.OnSwitchWeaponPlus;
                @SwitchWeaponPlus.canceled += instance.OnSwitchWeaponPlus;
                @SwitchWeaponLess.started += instance.OnSwitchWeaponLess;
                @SwitchWeaponLess.performed += instance.OnSwitchWeaponLess;
                @SwitchWeaponLess.canceled += instance.OnSwitchWeaponLess;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Charge.started += instance.OnCharge;
                @Charge.performed += instance.OnCharge;
                @Charge.canceled += instance.OnCharge;
                @CameraMove.started += instance.OnCameraMove;
                @CameraMove.performed += instance.OnCameraMove;
                @CameraMove.canceled += instance.OnCameraMove;
                @Aim.started += instance.OnAim;
                @Aim.performed += instance.OnAim;
                @Aim.canceled += instance.OnAim;
                @Dash.started += instance.OnDash;
                @Dash.performed += instance.OnDash;
                @Dash.canceled += instance.OnDash;
                @Talk.started += instance.OnTalk;
                @Talk.performed += instance.OnTalk;
                @Talk.canceled += instance.OnTalk;
                @AimCameraMove.started += instance.OnAimCameraMove;
                @AimCameraMove.performed += instance.OnAimCameraMove;
                @AimCameraMove.canceled += instance.OnAimCameraMove;
            }
        }
    }
    public PlayerActions @player => new PlayerActions(this);

    // Menu
    private readonly InputActionMap m_Menu;
    private IMenuActions m_MenuActionsCallbackInterface;
    private readonly InputAction m_Menu_MenuSelectRight;
    private readonly InputAction m_Menu_MenuSelectLeft;
    private readonly InputAction m_Menu_MenuSelectDown;
    private readonly InputAction m_Menu_MenuSelectUp;
    private readonly InputAction m_Menu_Menu;
    private readonly InputAction m_Menu_MenuConfirm;
    public struct MenuActions
    {
        private @Controls m_Wrapper;
        public MenuActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @MenuSelectRight => m_Wrapper.m_Menu_MenuSelectRight;
        public InputAction @MenuSelectLeft => m_Wrapper.m_Menu_MenuSelectLeft;
        public InputAction @MenuSelectDown => m_Wrapper.m_Menu_MenuSelectDown;
        public InputAction @MenuSelectUp => m_Wrapper.m_Menu_MenuSelectUp;
        public InputAction @Menu => m_Wrapper.m_Menu_Menu;
        public InputAction @MenuConfirm => m_Wrapper.m_Menu_MenuConfirm;
        public InputActionMap Get() { return m_Wrapper.m_Menu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MenuActions set) { return set.Get(); }
        public void SetCallbacks(IMenuActions instance)
        {
            if (m_Wrapper.m_MenuActionsCallbackInterface != null)
            {
                @MenuSelectRight.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnMenuSelectRight;
                @MenuSelectRight.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnMenuSelectRight;
                @MenuSelectRight.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnMenuSelectRight;
                @MenuSelectLeft.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnMenuSelectLeft;
                @MenuSelectLeft.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnMenuSelectLeft;
                @MenuSelectLeft.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnMenuSelectLeft;
                @MenuSelectDown.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnMenuSelectDown;
                @MenuSelectDown.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnMenuSelectDown;
                @MenuSelectDown.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnMenuSelectDown;
                @MenuSelectUp.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnMenuSelectUp;
                @MenuSelectUp.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnMenuSelectUp;
                @MenuSelectUp.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnMenuSelectUp;
                @Menu.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnMenu;
                @Menu.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnMenu;
                @Menu.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnMenu;
                @MenuConfirm.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnMenuConfirm;
                @MenuConfirm.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnMenuConfirm;
                @MenuConfirm.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnMenuConfirm;
            }
            m_Wrapper.m_MenuActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MenuSelectRight.started += instance.OnMenuSelectRight;
                @MenuSelectRight.performed += instance.OnMenuSelectRight;
                @MenuSelectRight.canceled += instance.OnMenuSelectRight;
                @MenuSelectLeft.started += instance.OnMenuSelectLeft;
                @MenuSelectLeft.performed += instance.OnMenuSelectLeft;
                @MenuSelectLeft.canceled += instance.OnMenuSelectLeft;
                @MenuSelectDown.started += instance.OnMenuSelectDown;
                @MenuSelectDown.performed += instance.OnMenuSelectDown;
                @MenuSelectDown.canceled += instance.OnMenuSelectDown;
                @MenuSelectUp.started += instance.OnMenuSelectUp;
                @MenuSelectUp.performed += instance.OnMenuSelectUp;
                @MenuSelectUp.canceled += instance.OnMenuSelectUp;
                @Menu.started += instance.OnMenu;
                @Menu.performed += instance.OnMenu;
                @Menu.canceled += instance.OnMenu;
                @MenuConfirm.started += instance.OnMenuConfirm;
                @MenuConfirm.performed += instance.OnMenuConfirm;
                @MenuConfirm.canceled += instance.OnMenuConfirm;
            }
        }
    }
    public MenuActions @Menu => new MenuActions(this);
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
        void OnSwitchWeaponPlus(InputAction.CallbackContext context);
        void OnSwitchWeaponLess(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnCharge(InputAction.CallbackContext context);
        void OnCameraMove(InputAction.CallbackContext context);
        void OnAim(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
        void OnTalk(InputAction.CallbackContext context);
        void OnAimCameraMove(InputAction.CallbackContext context);
    }
    public interface IMenuActions
    {
        void OnMenuSelectRight(InputAction.CallbackContext context);
        void OnMenuSelectLeft(InputAction.CallbackContext context);
        void OnMenuSelectDown(InputAction.CallbackContext context);
        void OnMenuSelectUp(InputAction.CallbackContext context);
        void OnMenu(InputAction.CallbackContext context);
        void OnMenuConfirm(InputAction.CallbackContext context);
    }
}
