// GENERATED AUTOMATICALLY FROM 'Assets/Content/Input/GameInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class GameInput : IInputActionCollection
{
    private InputActionAsset asset;
    public GameInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""GameInput"",
    ""maps"": [
        {
            ""name"": ""UI"",
            ""id"": ""537d86c0-94ea-4815-9bc4-fd7270ceb48c"",
            ""actions"": [
                {
                    ""name"": ""Navigate"",
                    ""id"": ""47208bd3-1f91-4352-ace3-fb17241db969"",
                    ""expectedControlLayout"": ""Vector2"",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                },
                {
                    ""name"": ""Submit"",
                    ""id"": ""307fc4c9-412e-47c5-8077-2eb5498a9e78"",
                    ""expectedControlLayout"": ""Button"",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                },
                {
                    ""name"": ""Cancel"",
                    ""id"": ""a9d6900f-2266-473e-9bca-8b596f092edc"",
                    ""expectedControlLayout"": ""Button"",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                },
                {
                    ""name"": ""Point"",
                    ""id"": ""df693aeb-913e-4269-9d96-6b027f778852"",
                    ""expectedControlLayout"": ""Vector2"",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                },
                {
                    ""name"": ""Click"",
                    ""id"": ""5981cd3b-6a6b-42ad-ac33-87299fd3df2e"",
                    ""expectedControlLayout"": ""Button"",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                },
                {
                    ""name"": ""ScrollWheel"",
                    ""id"": ""23353cf5-41fc-475a-b73b-20c1900a558e"",
                    ""expectedControlLayout"": """",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                },
                {
                    ""name"": ""MiddleClick"",
                    ""id"": ""b847a2c0-0552-42a1-95ed-ca17012c7a9e"",
                    ""expectedControlLayout"": """",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                },
                {
                    ""name"": ""RightClick"",
                    ""id"": ""2617ac18-b43b-4171-91ff-e4603a56c01f"",
                    ""expectedControlLayout"": """",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                },
                {
                    ""name"": ""Pause"",
                    ""id"": ""c4b9e43a-3286-476c-9d32-e37c1aea0e04"",
                    ""expectedControlLayout"": """",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""892f6ecc-312a-4c70-9f27-cbca6cc8477b"",
                    ""path"": ""<Gamepad>/dpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""ab866ef0-da65-47fe-8732-f6da63a21baf"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""f8b8506b-8a88-41fe-aacf-88f5f480b8c5"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""33b3776c-7e53-4bb7-8a15-b63fb089fd53"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""79690a5a-adb0-4036-9d3d-84cc0151b75c"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""Point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""a83a0532-0962-49e2-bf77-c382cb7d60a5"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""d0c1d622-0955-4d0a-ba52-4af057759cc3"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""ScrollWheel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""ca896fb2-e726-4b82-aa09-7908f4d64f04"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""MiddleClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""42944f98-ca2e-4b43-89b3-b54c2d161a08"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""RightClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""7f4dabc9-4c5f-44ea-a737-70c3de102755"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""0010d47c-012f-44f8-9d48-b53a376e191f"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                }
            ]
        },
        {
            ""name"": ""Player"",
            ""id"": ""8ba21629-32c9-496d-b968-4c86a8718840"",
            ""actions"": [
                {
                    ""name"": ""SteerAxis"",
                    ""id"": ""28b92d92-fb88-4fff-947a-46799f0f445e"",
                    ""expectedControlLayout"": ""Axis"",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                },
                {
                    ""name"": ""GasAxis"",
                    ""id"": ""2534446a-a597-471c-9c41-18f4b40575bc"",
                    ""expectedControlLayout"": ""Axis"",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                },
                {
                    ""name"": ""Horn"",
                    ""id"": ""0c2f8232-35d4-4c9e-bb99-243b4ab8b76e"",
                    ""expectedControlLayout"": """",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""612a5412-9778-4108-a616-3e1ac396723b"",
                    ""path"": ""<Gamepad>/leftStick/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SteerAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""1dd28a4b-b410-460b-8314-30b9bbc89df8"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SteerAxis"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""30075a90-380e-4b86-9d4b-aa511e5ee516"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SteerAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""b325709b-2df3-46e9-b43e-ed1c2f160ed3"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SteerAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""6b898f0c-a068-4f94-a545-6b535a3d0610"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GasAxis"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""14456d37-b7a8-41e9-844c-d0ed7f6f911d"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GasAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""7883b3e3-6cfd-4938-af06-3719f3f203b0"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GasAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""Keyboard Input"",
                    ""id"": ""3509b98e-f25d-4180-82ca-682339d12714"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GasAxis"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""312ba965-a2cd-4227-a8b1-b2d08123e2af"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GasAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""4944c8d6-b96e-4a69-ab67-a279e086b9a8"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GasAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""d685823f-5c21-4079-8401-7e964be436e1"",
                    ""path"": ""<Gamepad>/leftStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""a77a9926-edd1-4da8-8505-c293b2726dc4"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""35e38ef3-0f51-4464-a804-72530eacfc2d"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // UI
        m_UI = asset.GetActionMap("UI");
        m_UI_Navigate = m_UI.GetAction("Navigate");
        m_UI_Submit = m_UI.GetAction("Submit");
        m_UI_Cancel = m_UI.GetAction("Cancel");
        m_UI_Point = m_UI.GetAction("Point");
        m_UI_Click = m_UI.GetAction("Click");
        m_UI_ScrollWheel = m_UI.GetAction("ScrollWheel");
        m_UI_MiddleClick = m_UI.GetAction("MiddleClick");
        m_UI_RightClick = m_UI.GetAction("RightClick");
        m_UI_Pause = m_UI.GetAction("Pause");
        // Player
        m_Player = asset.GetActionMap("Player");
        m_Player_SteerAxis = m_Player.GetAction("SteerAxis");
        m_Player_GasAxis = m_Player.GetAction("GasAxis");
        m_Player_Horn = m_Player.GetAction("Horn");
    }

    ~GameInput()
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

    public ReadOnlyArray<InputControlScheme> controlSchemes
    {
        get => asset.controlSchemes;
    }

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

    // UI
    private InputActionMap m_UI;
    private IUIActions m_UIActionsCallbackInterface;
    private InputAction m_UI_Navigate;
    private InputAction m_UI_Submit;
    private InputAction m_UI_Cancel;
    private InputAction m_UI_Point;
    private InputAction m_UI_Click;
    private InputAction m_UI_ScrollWheel;
    private InputAction m_UI_MiddleClick;
    private InputAction m_UI_RightClick;
    private InputAction m_UI_Pause;
    public struct UIActions
    {
        private GameInput m_Wrapper;
        public UIActions(GameInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Navigate { get { return m_Wrapper.m_UI_Navigate; } }
        public InputAction @Submit { get { return m_Wrapper.m_UI_Submit; } }
        public InputAction @Cancel { get { return m_Wrapper.m_UI_Cancel; } }
        public InputAction @Point { get { return m_Wrapper.m_UI_Point; } }
        public InputAction @Click { get { return m_Wrapper.m_UI_Click; } }
        public InputAction @ScrollWheel { get { return m_Wrapper.m_UI_ScrollWheel; } }
        public InputAction @MiddleClick { get { return m_Wrapper.m_UI_MiddleClick; } }
        public InputAction @RightClick { get { return m_Wrapper.m_UI_RightClick; } }
        public InputAction @Pause { get { return m_Wrapper.m_UI_Pause; } }
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled { get { return Get().enabled; } }
        public InputActionMap Clone() { return Get().Clone(); }
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void SetCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterface != null)
            {
                Navigate.started -= m_Wrapper.m_UIActionsCallbackInterface.OnNavigate;
                Navigate.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnNavigate;
                Navigate.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnNavigate;
                Submit.started -= m_Wrapper.m_UIActionsCallbackInterface.OnSubmit;
                Submit.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnSubmit;
                Submit.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnSubmit;
                Cancel.started -= m_Wrapper.m_UIActionsCallbackInterface.OnCancel;
                Cancel.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnCancel;
                Cancel.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnCancel;
                Point.started -= m_Wrapper.m_UIActionsCallbackInterface.OnPoint;
                Point.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnPoint;
                Point.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnPoint;
                Click.started -= m_Wrapper.m_UIActionsCallbackInterface.OnClick;
                Click.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnClick;
                Click.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnClick;
                ScrollWheel.started -= m_Wrapper.m_UIActionsCallbackInterface.OnScrollWheel;
                ScrollWheel.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnScrollWheel;
                ScrollWheel.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnScrollWheel;
                MiddleClick.started -= m_Wrapper.m_UIActionsCallbackInterface.OnMiddleClick;
                MiddleClick.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnMiddleClick;
                MiddleClick.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnMiddleClick;
                RightClick.started -= m_Wrapper.m_UIActionsCallbackInterface.OnRightClick;
                RightClick.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnRightClick;
                RightClick.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnRightClick;
                Pause.started -= m_Wrapper.m_UIActionsCallbackInterface.OnPause;
                Pause.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnPause;
                Pause.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnPause;
            }
            m_Wrapper.m_UIActionsCallbackInterface = instance;
            if (instance != null)
            {
                Navigate.started += instance.OnNavigate;
                Navigate.performed += instance.OnNavigate;
                Navigate.canceled += instance.OnNavigate;
                Submit.started += instance.OnSubmit;
                Submit.performed += instance.OnSubmit;
                Submit.canceled += instance.OnSubmit;
                Cancel.started += instance.OnCancel;
                Cancel.performed += instance.OnCancel;
                Cancel.canceled += instance.OnCancel;
                Point.started += instance.OnPoint;
                Point.performed += instance.OnPoint;
                Point.canceled += instance.OnPoint;
                Click.started += instance.OnClick;
                Click.performed += instance.OnClick;
                Click.canceled += instance.OnClick;
                ScrollWheel.started += instance.OnScrollWheel;
                ScrollWheel.performed += instance.OnScrollWheel;
                ScrollWheel.canceled += instance.OnScrollWheel;
                MiddleClick.started += instance.OnMiddleClick;
                MiddleClick.performed += instance.OnMiddleClick;
                MiddleClick.canceled += instance.OnMiddleClick;
                RightClick.started += instance.OnRightClick;
                RightClick.performed += instance.OnRightClick;
                RightClick.canceled += instance.OnRightClick;
                Pause.started += instance.OnPause;
                Pause.performed += instance.OnPause;
                Pause.canceled += instance.OnPause;
            }
        }
    }
    public UIActions @UI
    {
        get
        {
            return new UIActions(this);
        }
    }

    // Player
    private InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private InputAction m_Player_SteerAxis;
    private InputAction m_Player_GasAxis;
    private InputAction m_Player_Horn;
    public struct PlayerActions
    {
        private GameInput m_Wrapper;
        public PlayerActions(GameInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @SteerAxis { get { return m_Wrapper.m_Player_SteerAxis; } }
        public InputAction @GasAxis { get { return m_Wrapper.m_Player_GasAxis; } }
        public InputAction @Horn { get { return m_Wrapper.m_Player_Horn; } }
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled { get { return Get().enabled; } }
        public InputActionMap Clone() { return Get().Clone(); }
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                SteerAxis.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSteerAxis;
                SteerAxis.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSteerAxis;
                SteerAxis.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSteerAxis;
                GasAxis.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGasAxis;
                GasAxis.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGasAxis;
                GasAxis.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGasAxis;
                Horn.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHorn;
                Horn.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHorn;
                Horn.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHorn;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                SteerAxis.started += instance.OnSteerAxis;
                SteerAxis.performed += instance.OnSteerAxis;
                SteerAxis.canceled += instance.OnSteerAxis;
                GasAxis.started += instance.OnGasAxis;
                GasAxis.performed += instance.OnGasAxis;
                GasAxis.canceled += instance.OnGasAxis;
                Horn.started += instance.OnHorn;
                Horn.performed += instance.OnHorn;
                Horn.canceled += instance.OnHorn;
            }
        }
    }
    public PlayerActions @Player
    {
        get
        {
            return new PlayerActions(this);
        }
    }
    public interface IUIActions
    {
        void OnNavigate(InputAction.CallbackContext context);
        void OnSubmit(InputAction.CallbackContext context);
        void OnCancel(InputAction.CallbackContext context);
        void OnPoint(InputAction.CallbackContext context);
        void OnClick(InputAction.CallbackContext context);
        void OnScrollWheel(InputAction.CallbackContext context);
        void OnMiddleClick(InputAction.CallbackContext context);
        void OnRightClick(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
    }
    public interface IPlayerActions
    {
        void OnSteerAxis(InputAction.CallbackContext context);
        void OnGasAxis(InputAction.CallbackContext context);
        void OnHorn(InputAction.CallbackContext context);
    }
}
