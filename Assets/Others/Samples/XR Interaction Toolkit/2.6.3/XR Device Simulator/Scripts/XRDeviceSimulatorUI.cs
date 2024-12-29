using System;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Simulation;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UnityEngine.XR.Interaction.Toolkit.Samples.DeviceSimulator
{
    internal class XRDeviceSimulatorUI : MonoBehaviour
    {
        private XRDeviceSimulator m_Simulator;
        private const string k_MouseDeviceType = "Mouse";
        private const string k_TranslateLookText = "Move";
        private const string k_RotateLookText = "Look";

#if UNITY_EDITOR
        private const string k_MenuOpenStateKey = "XRI." + nameof(XRDeviceSimulatorUI) + ".MenuOpenState";
#endif
        [SerializeField]
        [HideInInspector]
        private bool m_IsMenuOpen = true;

        private bool isMenuOpen
        {
            get
            {
#if UNITY_EDITOR
                m_IsMenuOpen = EditorPrefs.GetBool(k_MenuOpenStateKey, m_IsMenuOpen);
#endif
                return m_IsMenuOpen;
            }

            set
            {
                m_IsMenuOpen = value;
#if UNITY_EDITOR
                EditorPrefs.SetBool(k_MenuOpenStateKey, m_IsMenuOpen);
#endif
            }
        }

        [Header("Open/Close Panels")]

        [SerializeField]
        private GameObject m_XRDeviceSimulatorMainPanel;
        [SerializeField]
        private GameObject m_XRDeviceSimulatorCollapsedPanel;

        [Header("Sprites")]

        [SerializeField]
        private Sprite m_HmdSpriteDark;
        [SerializeField]
        private Sprite m_HmdSpriteLight;

        [SerializeField]
        private Sprite m_KeyboardSprite;
        internal Sprite keyboardSprite => m_KeyboardSprite;

        [SerializeField]
        private Sprite m_MouseSprite;
        internal Sprite mouseSprite => m_MouseSprite;

        [SerializeField]
        private Sprite m_RMouseSpriteDark;
        internal Sprite rMouseSpriteDark => m_RMouseSpriteDark;

        [SerializeField]
        private Sprite m_RMouseSpriteLight;
        internal Sprite rMouseSpriteLight => m_RMouseSpriteLight;

        [HideInInspector]
        [SerializeField]
        private Sprite m_RMouseSprite;
        internal Sprite rMouseSprite
        {
            get
            {
#if !UNITY_EDITOR
                if (m_RMouseSprite == null)
                    m_RMouseSprite = m_RMouseSpriteDark;
#endif
                return m_RMouseSprite;
            }
        }

        [SerializeField]
        private Sprite m_RoundedRectangle;

        [Header("General")]

        [SerializeField]
        private Text m_CycleDevicesText;

        [SerializeField]
        private Text m_CurrentSelectedDeviceText;

        [Header("Headset Device")]

        [SerializeField]
        private Image m_HeadsetImage;

        [Space]

        [SerializeField]
        private Image m_HeadsetMoveButton;

        [SerializeField]
        private Image m_HeadsetMoveButtonIcon;

        [SerializeField]
        private Text m_HeadsetMoveButtonText;

        [SerializeField]
        private Image m_HeadsetMoveValueIcon;

        [SerializeField]
        private Text m_HeadsetMoveValueText;

        [Space]

        [SerializeField]
        private Image m_HeadsetLookButton;

        [SerializeField]
        private Text m_HeadsetLookButtonText;

        [SerializeField]
        private Image m_HeadsetLookValueIcon;

        [SerializeField]
        private Text m_HeadsetLookValueText;

        [Space]

        [SerializeField]
        [FormerlySerializedAs("m_ShowCursorButton")]
        private Image m_CursorLockButton;

        [SerializeField]
        [FormerlySerializedAs("m_ShowCursorValueText")]
        private Text m_CursorLockValueText;

        [Space]

        [SerializeField]
        private Text m_MouseModeButtonText;

        [SerializeField]
        private Text m_MouseModeValueText;

        [Space]

        [SerializeField]
        private Image m_HeadsetSelectedButton;

        [SerializeField]
        private Text m_HeadsetSelectedValueText;

        [Header("Controllers")]

        [SerializeField]
        private Image m_ControllerSelectedButton;

        [SerializeField]
        private Image m_ControllerSelectedIcon;

        [SerializeField]
        private Text m_ControllerSelectedText;

        [SerializeField]
        private Text m_ControllersSelectedValueText;

        [SerializeField]
        private CanvasGroup m_ControllersCanvasGroup;

        [Header("Left Controller")]

        [SerializeField]
        private XRDeviceSimulatorControllerUI m_LeftController;

        [SerializeField]
        private Text m_LeftControllerButtonText;

        [Header("Right Controller")]

        [SerializeField]
        private XRDeviceSimulatorControllerUI m_RightController;

        [SerializeField]
        private Text m_RightControllerButtonText;

        [Header("Hands")]

        [SerializeField]
        private Image m_HandsSelectedButton;

        [SerializeField]
        private Image m_HandsSelectedIcon;

        [SerializeField]
        private Text m_HandsSelectedText;

        [SerializeField]
        private Image m_HandsSelectedValueIcon;

        [SerializeField]
        private Text m_HandsSelectedValueText;

        [SerializeField]
        private CanvasGroup m_HandsCanvasGroup;

        [Header("Left Hand")]

        [SerializeField]
        private XRDeviceSimulatorHandsUI m_LeftHand;

        [SerializeField]
        private Text m_LeftHandButtonText;

        [Header("Right Hand")]

        [SerializeField]
        private XRDeviceSimulatorHandsUI m_RightHand;

        [SerializeField]
        private Text m_RightHandButtonText;
        private static readonly Color k_EnabledColorDark = new Color(0xC4 / 255f, 0xC4 / 255f, 0xC4 / 255f);
        private static readonly Color k_EnabledColorLight = new Color(0x55 / 255f, 0x55 / 255f, 0x55 / 255f);
        [HideInInspector]
        [SerializeField]
        private Color m_EnabledColor = Color.clear;
        internal Color enabledColor
        {
            get
            {
#if !UNITY_EDITOR
                if (m_EnabledColor == Color.clear)
                    m_EnabledColor = k_EnabledColorDark;
#endif
                return m_EnabledColor;
            }
        }

        private static readonly Color k_DisabledColorDark = new Color(0xC4 / 255f, 0xC4 / 255f, 0xC4 / 255f, 0.5f);
        private static readonly Color k_DisabledColorLight = new Color(0x55 / 255f, 0x55 / 255f, 0x55 / 255f, 0.5f);
        [HideInInspector]
        [SerializeField]
        private Color m_DisabledColor = Color.clear;
        internal Color disabledColor
        {
            get
            {
#if !UNITY_EDITOR
                if (m_DisabledColor == Color.clear)
                    m_DisabledColor = k_DisabledColorDark;
#endif
                return m_DisabledColor;
            }
        }

        private static readonly Color k_ButtonColorDark = new Color(0x55 / 255f, 0x55 / 255f, 0x55 / 255f);
        private static readonly Color k_ButtonColorLight = new Color(0xE4 / 255f, 0xE4 / 255f, 0xE4 / 255f);
        [HideInInspector]
        [SerializeField]
        private Color m_ButtonColor = Color.clear;
        internal Color buttonColor
        {
            get
            {
#if !UNITY_EDITOR
                if (m_ButtonColor == Color.clear)
                    m_ButtonColor = k_ButtonColorDark;
#endif
                return m_ButtonColor;
            }
        }

        private static readonly Color k_DisabledButtonColorDark = new Color(0x55 / 255f, 0x55 / 255f, 0x55 / 255f, 0.5f);
        private static readonly Color k_DisabledButtonColorLight = new Color(0xE4 / 255f, 0xE4 / 255f, 0xE4 / 255f, 0.5f);
        [HideInInspector]
        [SerializeField]
        private Color m_DisabledButtonColor = Color.clear;
        internal Color disabledButtonColor
        {
            get
            {
#if !UNITY_EDITOR
                if (m_DisabledButtonColor == Color.clear)
                    m_DisabledButtonColor = k_DisabledButtonColorDark;
#endif
                return m_DisabledButtonColor;
            }
        }

        private static readonly Color k_SelectedColorDark = new Color(0x4F / 255f, 0x65 / 255f, 0x7F / 255f);
        private static readonly Color k_SelectedColorLight = new Color(0x96 / 255f, 0xC3 / 255f, 0xFB / 255f);
        [HideInInspector]
        [SerializeField]
        private Color m_SelectedColor = Color.clear;
        internal Color selectedColor
        {
            get
            {
#if !UNITY_EDITOR
                if (m_SelectedColor == Color.clear)
                    m_SelectedColor = k_SelectedColorDark;
#endif
                return m_SelectedColor;
            }
        }

        private static readonly Color k_BackgroundColorDark = Color.black;
        private static readonly Color k_BackgroundColorLight = new Color(0xB6 / 255f, 0xB6 / 255f, 0xB6 / 255f);
        [HideInInspector]
        [SerializeField]
        private Color m_BackgroundColor = Color.clear;
        internal Color backgroundColor
        {
            get
            {
#if !UNITY_EDITOR
                if (m_BackgroundColor == Color.clear)
                    m_BackgroundColor = k_BackgroundColorDark;
#endif
                return m_BackgroundColor;
            }
        }

        private static readonly Color k_DeviceColorDark = new Color(0x6E / 255f, 0x6E / 255f, 0x6E / 255f);
        private static readonly Color k_DeviceColorLight = new Color(0xE4 / 255f, 0xE4 / 255f, 0xE4 / 255f);
        [HideInInspector]
        [SerializeField]
        private Color m_DeviceColor = Color.clear;
        internal Color deviceColor
        {
            get
            {
#if !UNITY_EDITOR
                if (m_DeviceColor == Color.clear)
                    m_DeviceColor = k_DeviceColorDark;
#endif
                return m_DeviceColor;
            }
        }

        private static readonly Color k_DisabledDeviceColorDark = new Color(0x58 / 255f, 0x58 / 255f, 0x58 / 255f);
        private static readonly Color k_DisabledDeviceColorLight = new Color(0xA2 / 255f, 0xA2 / 255f, 0xA2 / 255f, 0.5f);
        [HideInInspector]
        [SerializeField]
        private Color m_DisabledDeviceColor = Color.clear;
        internal Color disabledDeviceColor
        {
            get
            {
#if !UNITY_EDITOR
                if (m_DisabledDeviceColor == Color.clear)
                    m_DisabledDeviceColor = k_DisabledDeviceColorDark;
#endif
                return m_DisabledDeviceColor;
            }
        }


        // Handles 2 axis activation for 1 UI Button
        private bool m_XAxisActivated;
        private bool m_ZAxisActivated;

        /// <summary>
        /// See <see cref="MonoBehaviour"/>.
        /// </summary>
        protected void Start()
        {
            var simulator = GetComponentInParent<XRDeviceSimulator>();
            if (simulator != null)
                Initialize(simulator);
        }

        /// <summary>
        /// See <see cref="MonoBehaviour"/>.
        /// </summary>
        protected void OnDestroy()
        {
            if (m_Simulator != null)
            {
                Unsubscribe(m_Simulator.manipulateLeftAction, OnManipulateLeftAction);
                Unsubscribe(m_Simulator.manipulateRightAction, OnManipulateRightAction);
                Unsubscribe(m_Simulator.toggleManipulateLeftAction, OnToggleManipulateLeftAction);
                Unsubscribe(m_Simulator.toggleManipulateRightAction, OnToggleManipulateRightAction);
                Unsubscribe(m_Simulator.toggleManipulateBodyAction, OnToggleManipulateBodyAction);
                Unsubscribe(m_Simulator.manipulateHeadAction, OnManipulateHeadAction);
                Unsubscribe(m_Simulator.handControllerModeAction, OnHandControllerModeAction);
                Unsubscribe(m_Simulator.cycleDevicesAction, OnCycleDevicesAction);
                Unsubscribe(m_Simulator.stopManipulationAction, OnStopManipulationAction);
                Unsubscribe(m_Simulator.toggleMouseTransformationModeAction, OnToggleMouseTransformationModeAction);
                Unsubscribe(m_Simulator.negateModeAction, OnNegateModeAction);
                Unsubscribe(m_Simulator.toggleCursorLockAction, OnToggleCursorLockAction);
                Unsubscribe(m_Simulator.keyboardXTranslateAction, OnKeyboardXTranslateAction);
                Unsubscribe(m_Simulator.keyboardYTranslateAction, OnKeyboardYTranslateAction);
                Unsubscribe(m_Simulator.keyboardZTranslateAction, OnKeyboardZTranslateAction);
                Unsubscribe(m_Simulator.restingHandAxis2DAction, OnRestingHandAxis2DAction);
                Unsubscribe(m_Simulator.gripAction, OnGripAction);
                Unsubscribe(m_Simulator.triggerAction, OnTriggerAction);
                Unsubscribe(m_Simulator.menuAction, OnMenuAction);
                Unsubscribe(m_Simulator.primaryButtonAction, OnPrimaryButtonAction);
                Unsubscribe(m_Simulator.secondaryButtonAction, OnSecondaryButtonAction);
#if XR_HANDS_1_1_OR_NEWER
                foreach (var simulatedExpression in m_Simulator.simulatedHandExpressions)
                {
                    simulatedExpression.performed -= OnHandExpressionAction;
                }
#endif

            }
        }

        private void Initialize(XRDeviceSimulator simulator)
        {
            m_Simulator = simulator;
            InitColorTheme();
            Initialize();
            // Start with the headset enabled
            OnSetMouseMode();
            OnActivateHeadsetDevice();
        }

        private void InitColorTheme()
        {
#if UNITY_EDITOR
            var isEditorPro = EditorGUIUtility.isProSkin;
            m_EnabledColor = isEditorPro ? k_EnabledColorDark : k_EnabledColorLight;
            m_DisabledColor = isEditorPro ? k_DisabledColorDark : k_DisabledColorLight;
            m_ButtonColor = isEditorPro ? k_ButtonColorDark : k_ButtonColorLight;
            m_DisabledButtonColor = isEditorPro ? k_DisabledButtonColorDark : k_DisabledButtonColorLight;
            m_SelectedColor = isEditorPro ? k_SelectedColorDark : k_SelectedColorLight;
            m_BackgroundColor = isEditorPro ? k_BackgroundColorDark : k_BackgroundColorLight;
            m_DeviceColor = isEditorPro ? k_DeviceColorDark : k_DeviceColorLight;
            m_DisabledDeviceColor = isEditorPro ? k_DisabledDeviceColorDark : k_DisabledDeviceColorLight;
            m_HeadsetImage.sprite = isEditorPro ? m_HmdSpriteDark : m_HmdSpriteLight;
            m_RMouseSprite = isEditorPro ? m_RMouseSpriteDark : m_RMouseSpriteLight;
#endif
        }

        private void Initialize()
        {
            var bckgrdAlpha = m_XRDeviceSimulatorMainPanel.GetComponent<Image>().color.a;

            foreach (var image in GetComponentsInChildren<Image>(true))
                image.color = image.sprite == null || image.sprite == m_RoundedRectangle ? buttonColor : enabledColor;


            foreach (var text in GetComponentsInChildren<Text>(true))
                text.color = enabledColor;

            m_HeadsetImage.color = Color.white;

            var bckgrdColor = backgroundColor;
            bckgrdColor.a = bckgrdAlpha;
            m_XRDeviceSimulatorMainPanel.GetComponent<Image>().color = bckgrdColor;
            m_XRDeviceSimulatorCollapsedPanel.GetComponent<Image>().color = bckgrdColor;

            m_CycleDevicesText.text = m_Simulator.cycleDevicesAction.action.controls[0].displayName;

            // Headset
            var toggleManipulateBodyActionControl = m_Simulator.toggleManipulateBodyAction.action.controls[0];
            m_HeadsetSelectedValueText.text = $"{toggleManipulateBodyActionControl.displayName}";

            var ctrlsBinding1 = m_Simulator.axis2DAction.action.controls;
            var ctrlsBinding2 = m_Simulator.keyboardYTranslateAction.action.controls;
            m_HeadsetMoveValueText.text = $"{ctrlsBinding1[0].displayName},{ctrlsBinding1[1].displayName},{ctrlsBinding1[2].displayName},{ctrlsBinding1[3].displayName} + " +
                $"{ctrlsBinding2[0].displayName},{ctrlsBinding2[1].displayName}";

            m_CursorLockValueText.text = m_Simulator.toggleCursorLockAction.action.controls[0].displayName;
            m_CursorLockButton.color = Cursor.lockState == CursorLockMode.Locked ? selectedColor : buttonColor;

            m_HeadsetLookButtonText.text = m_Simulator.mouseTransformationMode == XRDeviceSimulator.TransformationMode.Translate ? k_TranslateLookText : k_RotateLookText;
            m_MouseModeValueText.text = m_Simulator.toggleMouseTransformationModeAction.action.controls[0].displayName;

            var manipulateHeadActionControl = m_Simulator.manipulateHeadAction.action.controls[0];
            m_HeadsetLookValueIcon.sprite = GetInputIcon(manipulateHeadActionControl);
            if (manipulateHeadActionControl.name.Equals("leftButton") || manipulateHeadActionControl.name.Equals("rightButton"))
            {
                m_HeadsetLookValueIcon.color = Color.white;

                // If the binding is using the left button, mirror the MouseR image
                if (manipulateHeadActionControl.name.Equals("leftButton"))
                    m_HeadsetLookValueIcon.transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            m_HeadsetLookValueText.text = manipulateHeadActionControl.device.name == k_MouseDeviceType ? k_MouseDeviceType : manipulateHeadActionControl.displayName;

            m_LeftController.Initialize(m_Simulator);
            m_RightController.Initialize(m_Simulator);
            var toggleSlashHoldLeftText = $"{m_Simulator.toggleManipulateLeftAction.action.controls[0].displayName} / {m_Simulator.manipulateLeftAction.action.controls[0].displayName} [Hold]";
            var toggleSlashHoldRightText = $"{m_Simulator.toggleManipulateRightAction.action.controls[0].displayName} / {m_Simulator.manipulateRightAction.action.controls[0].displayName} [Hold]";
            m_LeftControllerButtonText.text = toggleSlashHoldLeftText;
            m_RightControllerButtonText.text = toggleSlashHoldRightText;

            m_LeftHand.Initialize(m_Simulator);
            m_RightHand.Initialize(m_Simulator);
            m_LeftHandButtonText.text = toggleSlashHoldLeftText;
            m_RightHandButtonText.text = toggleSlashHoldRightText;

            UpdateDeviceInputMethod();

            HandsSetActive(false);

#if XR_HANDS_1_1_OR_NEWER
            m_HandsSelectedValueIcon.color = enabledColor;
            m_HandsSelectedValueText.color = enabledColor;
#else
            m_HandsSelectedValueIcon.color = disabledColor;
            m_HandsSelectedValueText.color = disabledColor;
#endif

            m_HeadsetMoveButtonIcon.color = enabledColor;

            // Update OnDestroy with corresponding Unsubscribe call when adding here
            Subscribe(m_Simulator.manipulateLeftAction, OnManipulateLeftAction);
            Subscribe(m_Simulator.manipulateRightAction, OnManipulateRightAction);
            Subscribe(m_Simulator.toggleManipulateLeftAction, OnToggleManipulateLeftAction);
            Subscribe(m_Simulator.toggleManipulateRightAction, OnToggleManipulateRightAction);
            Subscribe(m_Simulator.toggleManipulateBodyAction, OnToggleManipulateBodyAction);
            Subscribe(m_Simulator.manipulateHeadAction, OnManipulateHeadAction);
            Subscribe(m_Simulator.handControllerModeAction, OnHandControllerModeAction);
            Subscribe(m_Simulator.cycleDevicesAction, OnCycleDevicesAction);
            Subscribe(m_Simulator.stopManipulationAction, OnStopManipulationAction);
            Subscribe(m_Simulator.toggleMouseTransformationModeAction, OnToggleMouseTransformationModeAction);
            Subscribe(m_Simulator.negateModeAction, OnNegateModeAction);
            Subscribe(m_Simulator.toggleCursorLockAction, OnToggleCursorLockAction);
            Subscribe(m_Simulator.keyboardXTranslateAction, OnKeyboardXTranslateAction);
            Subscribe(m_Simulator.keyboardYTranslateAction, OnKeyboardYTranslateAction);
            Subscribe(m_Simulator.keyboardZTranslateAction, OnKeyboardZTranslateAction);
            Subscribe(m_Simulator.restingHandAxis2DAction, OnRestingHandAxis2DAction);
            Subscribe(m_Simulator.gripAction, OnGripAction);
            Subscribe(m_Simulator.triggerAction, OnTriggerAction);
            Subscribe(m_Simulator.menuAction, OnMenuAction);
            Subscribe(m_Simulator.primaryButtonAction, OnPrimaryButtonAction);
            Subscribe(m_Simulator.secondaryButtonAction, OnSecondaryButtonAction);
#if XR_HANDS_1_1_OR_NEWER
            foreach (var simulatedExpression in m_Simulator.simulatedHandExpressions)
            {
                simulatedExpression.performed += OnHandExpressionAction;
            }
#endif

            m_XRDeviceSimulatorMainPanel.SetActive(isMenuOpen);
            m_XRDeviceSimulatorCollapsedPanel.SetActive(!isMenuOpen);
        }

        private void UpdateDeviceInputMethod()
        {
            var toggleManipulateText = $"{m_Simulator.toggleManipulateLeftAction.action.controls[0].displayName}, {m_Simulator.toggleManipulateRightAction.action.controls[0].displayName} [Toggle]";
#if XR_HANDS_1_1_OR_NEWER
            var modeChangeText = m_Simulator.handControllerModeAction != null ? $"{m_Simulator.handControllerModeAction.action.controls[0].displayName}" : string.Empty;
#else
            var modeChangeText = string.Empty;
#endif

            m_ControllersSelectedValueText.text = m_Simulator.deviceMode == XRDeviceSimulator.DeviceMode.Controller ? toggleManipulateText : modeChangeText;
            m_HandsSelectedValueText.text = m_Simulator.deviceMode == XRDeviceSimulator.DeviceMode.Hand ? toggleManipulateText : modeChangeText;

            var modeColor = m_Simulator.deviceMode == XRDeviceSimulator.DeviceMode.Controller ? enabledColor : disabledColor;
            m_ControllerSelectedIcon.color = modeColor;
            m_ControllerSelectedText.color = modeColor;

            modeColor = m_Simulator.deviceMode == XRDeviceSimulator.DeviceMode.Hand ? enabledColor : disabledColor;
            m_HandsSelectedIcon.color = modeColor;
            m_HandsSelectedText.color = modeColor;
        }

        internal Sprite GetInputIcon(InputControl control)
        {
            if (control == null)
                return null;

            var icon = keyboardSprite;
            if (control.device.name == k_MouseDeviceType)
            {
                switch (control.name)
                {
                    case "leftButton":
                    case "rightButton":
                        icon = rMouseSprite;
                        break;
                    default:
                        icon = mouseSprite;
                        break;
                }
            }

            return icon;
        }

        /// <summary>
        /// Hides the simulator UI panel when called while displaying the simulator button.
        /// </summary>
        public void OnClickCloseSimulatorUIPanel()
        {
            isMenuOpen = false;
            m_XRDeviceSimulatorMainPanel.SetActive(false);
            m_XRDeviceSimulatorCollapsedPanel.SetActive(true);
        }

        /// <summary>
        /// Displays the simulator UI panel when called while hiding the simulator button.
        /// </summary>
        public void OnClickOpenSimulatorUIPanel()
        {
            isMenuOpen = true;
            m_XRDeviceSimulatorMainPanel.SetActive(true);
            m_XRDeviceSimulatorCollapsedPanel.SetActive(false);
        }

        private void OnActivateLeftDevice()
        {
            if (m_Simulator.deviceMode == XRDeviceSimulator.DeviceMode.Controller)
                OnActivateLeftController();
            else if (m_Simulator.deviceMode == XRDeviceSimulator.DeviceMode.Hand)
                OnActivateLeftHand();
        }

        private void OnActivateRightDevice()
        {
            if (m_Simulator.deviceMode == XRDeviceSimulator.DeviceMode.Controller)
                OnActivateRightController();
            else if (m_Simulator.deviceMode == XRDeviceSimulator.DeviceMode.Hand)
                OnActivateRightHand();
        }

        private void OnActivateBothDevices()
        {
            if (m_Simulator.deviceMode == XRDeviceSimulator.DeviceMode.Controller)
                OnActivateBothControllers();
            else if (m_Simulator.deviceMode == XRDeviceSimulator.DeviceMode.Hand)
                OnActivateBothHands();
        }

        /// <summary>
        /// Sets the Left Controller device as active to receive input.
        /// </summary>
        private void OnActivateLeftController()
        {
            m_CurrentSelectedDeviceText.text = "Left Controller";
            OnActivateController(m_LeftController);
        }

        /// <summary>
        /// Sets the Right Controller device as active to receive input.
        /// </summary>
        private void OnActivateRightController()
        {
            m_CurrentSelectedDeviceText.text = "Right Controller";
            OnActivateController(m_RightController);
        }

        private void OnActivateController(XRDeviceSimulatorControllerUI controller)
        {
            ControllersSetActive(true);
            PushCurrentButtonState(controller);
            controller.SetAsActiveController(true, m_Simulator);
            var other = controller == m_LeftController ? m_RightController : m_LeftController;
            other.SetAsActiveController(false, m_Simulator, true);

            HeadDeviceSetActive(false);
            HandsSetActive(false);
        }

        /// <summary>
        /// Sets both Left & Right Controller devices as active to receive input.
        /// </summary>
        private void OnActivateBothControllers()
        {
            ControllersSetActive(true);
            m_CurrentSelectedDeviceText.text = "Left & Right Controllers";
            PushCurrentButtonState(m_LeftController);
            PushCurrentButtonState(m_RightController);
            m_LeftController.SetAsActiveController(true, m_Simulator);
            m_RightController.SetAsActiveController(true, m_Simulator);

            HeadDeviceSetActive(false);
            HandsSetActive(false);
        }

        private void PushCurrentButtonState(XRDeviceSimulatorControllerUI controller)
        {
            controller.OnGrip(m_Simulator.gripAction.action.inProgress);
            controller.OnTrigger(m_Simulator.triggerAction.action.inProgress);
            controller.OnMenu(m_Simulator.menuAction.action.inProgress);
            controller.OnPrimaryButton(m_Simulator.primaryButtonAction.action.inProgress);
            controller.OnSecondaryButton(m_Simulator.secondaryButtonAction.action.inProgress);
            controller.OnXAxisTranslatePerformed(m_Simulator.keyboardXTranslateAction.action.inProgress);
            controller.OnZAxisTranslatePerformed(m_Simulator.keyboardZTranslateAction.action.inProgress);
        }

        /// <summary>
        /// Sets the Left Hand device as active to receive input.
        /// </summary>
        private void OnActivateLeftHand()
        {
            m_CurrentSelectedDeviceText.text = "Left Hand";
            OnActivateHand(m_LeftHand);
        }

        /// <summary>
        /// Sets the Right Hand device as active to receive input.
        /// </summary>
        private void OnActivateRightHand()
        {
            m_CurrentSelectedDeviceText.text = "Right Hand";
            OnActivateHand(m_RightHand);
        }

        private void OnActivateHand(XRDeviceSimulatorHandsUI hand)
        {
            HandsSetActive(true);
            hand.SetActive(true, m_Simulator);
            XRDeviceSimulatorHandsUI otherHand = hand == m_LeftHand ? m_RightHand : m_LeftHand;
            otherHand.SetActive(false, m_Simulator);

            HeadDeviceSetActive(false);
            ControllersSetActive(false);
        }

        /// <summary>
        /// Sets both Left & Right Hand devices as active to receive input.
        /// </summary>
        private void OnActivateBothHands()
        {
            HandsSetActive(true);
            m_CurrentSelectedDeviceText.text = "Left & Right Hands";
            m_LeftHand.SetActive(true, m_Simulator);
            m_RightHand.SetActive(true, m_Simulator);

            HeadDeviceSetActive(false);
            ControllersSetActive(false);
        }

        /// <summary>
        /// Sets the headset device as active to receive input.
        /// </summary>
        private void OnActivateHeadsetDevice(bool activated = true)
        {
            m_LeftController.SetAsActiveController(false, m_Simulator);
            m_RightController.SetAsActiveController(false, m_Simulator);

            m_LeftHand.SetActive(false, m_Simulator);
            m_RightHand.SetActive(false, m_Simulator);

            m_CurrentSelectedDeviceText.text = activated ? "Head Mounted Display (HMD)" : "None";
            m_HeadsetImage.gameObject.SetActive(activated);

            HeadDeviceSetActive(activated);

            if (m_Simulator.manipulatingFPS)
            {
                ControllersSetActive(false, m_Simulator.deviceMode == XRDeviceSimulator.DeviceMode.Controller);
                HandsSetActive(false, m_Simulator.deviceMode == XRDeviceSimulator.DeviceMode.Hand);
            }
            else
            {
                HandsSetActive(false, Mathf.Approximately(m_HandsCanvasGroup.alpha, 1f));
                ControllersSetActive(false, Mathf.Approximately(m_ControllersCanvasGroup.alpha, 1f));
            }
        }

        /// <summary>
        /// Updates all the UI associated the the Headset.
        /// </summary>
        /// <param name="active">Whether the headset is the active device or not.</param>
        private void HeadDeviceSetActive(bool active)
        {
            m_HeadsetImage.gameObject.SetActive(active);
            m_HeadsetSelectedButton.color = active ? selectedColor : buttonColor;

            var currentColor = active ? enabledColor : disabledColor;
            m_HeadsetMoveButtonIcon.color = currentColor;
            m_HeadsetMoveButtonText.color = currentColor;
            m_HeadsetMoveValueIcon.color = currentColor;
            m_HeadsetMoveValueText.color = currentColor;

            m_HeadsetMoveButton.color = active ? buttonColor : disabledButtonColor;
        }

        private void HandsSetActive(bool isActive, bool showCanvasGroup = false)
        {
            m_HandsCanvasGroup.alpha = isActive || showCanvasGroup ? 1f : 0f;

#if XR_HANDS_1_1_OR_NEWER
            m_HandsSelectedButton.color = isActive ? selectedColor : buttonColor;
#else
            m_HandsSelectedButton.color = disabledButtonColor;
#endif
        }

        private void ControllersSetActive(bool isActive, bool showCanvasGroup = false)
        {
            m_ControllersCanvasGroup.alpha = isActive || showCanvasGroup ? 1f : 0f;
            m_ControllerSelectedButton.color = isActive ? selectedColor : buttonColor;
        }

        private static void Subscribe(InputActionReference reference, Action<InputAction.CallbackContext> performedOrCanceled)
        {
            var action = reference != null ? reference.action : null;
            if (action != null && performedOrCanceled != null)
            {
                action.performed += performedOrCanceled;
                action.canceled += performedOrCanceled;
            }
        }

        private static void Unsubscribe(InputActionReference reference, Action<InputAction.CallbackContext> performedOrCanceled)
        {
            var action = reference != null ? reference.action : null;
            if (action != null && performedOrCanceled != null)
            {
                action.performed -= performedOrCanceled;
                action.canceled -= performedOrCanceled;
            }
        }

        private void OnManipulateLeftAction(InputAction.CallbackContext context)
        {
            if (context.phase.IsInProgress())
            {
                if (m_Simulator.manipulatingLeftDevice && m_Simulator.manipulatingRightDevice)
                    OnActivateBothDevices();
                else if (m_Simulator.manipulatingLeftDevice)
                    OnActivateLeftDevice();
            }
            else
            {
                if (m_Simulator.manipulatingRightDevice)
                    OnActivateRightDevice();
                else
                    OnActivateHeadsetDevice(m_Simulator.manipulatingFPS);
            }
        }

        private void OnManipulateRightAction(InputAction.CallbackContext context)
        {
            if (context.phase.IsInProgress())
            {
                if (m_Simulator.manipulatingLeftDevice && m_Simulator.manipulatingRightDevice)
                    OnActivateBothDevices();
                else if (m_Simulator.manipulatingRightDevice)
                    OnActivateRightDevice();
            }
            else
            {
                if (m_Simulator.manipulatingLeftDevice)
                    OnActivateLeftDevice();
                else
                    OnActivateHeadsetDevice(m_Simulator.manipulatingFPS);
            }
        }

        private void OnToggleManipulateLeftAction(InputAction.CallbackContext context)
        {
            if (context.phase.IsInProgress())
            {
                if (m_Simulator.manipulatingLeftDevice)
                    OnActivateLeftDevice();
                else
                    OnActivateHeadsetDevice();
            }
        }

        private void OnToggleManipulateRightAction(InputAction.CallbackContext context)
        {
            if (context.phase.IsInProgress())
            {
                if (m_Simulator.manipulatingRightDevice)
                    OnActivateRightDevice();
                else
                    OnActivateHeadsetDevice();
            }
        }

        private void OnToggleManipulateBodyAction(InputAction.CallbackContext context)
        {
            if (context.phase.IsInProgress())
            {
                OnActivateHeadsetDevice();
            }
        }

        private void OnManipulateHeadAction(InputAction.CallbackContext context)
        {
            var isInProgress = context.phase.IsInProgress();
            var noDevices = !m_Simulator.manipulatingLeftDevice && !m_Simulator.manipulatingRightDevice;
            if (isInProgress)
            {
                if (m_Simulator.manipulatingFPS || noDevices)
                    OnActivateHeadsetDevice();
            }
            else if (noDevices)
            {
                OnActivateHeadsetDevice(m_Simulator.manipulatingFPS);
            }

            m_HeadsetLookButton.color = isInProgress ? selectedColor : buttonColor;
        }

        private void OnHandControllerModeAction(InputAction.CallbackContext context)
        {
#if XR_HANDS_1_1_OR_NEWER
            if (context.phase.IsInProgress())
            {
                if (m_Simulator.manipulatingLeftDevice && m_Simulator.manipulatingRightDevice)
                    OnActivateBothDevices();
                else if (m_Simulator.manipulatingLeftDevice)
                    OnActivateLeftDevice();
                else if (m_Simulator.manipulatingRightDevice)
                    OnActivateRightDevice();
                else if (m_Simulator.manipulatingFPS)
                    OnActivateHeadsetDevice();
                else
                {
                    ControllersSetActive(false, m_Simulator.deviceMode == XRDeviceSimulator.DeviceMode.Controller);
                    HandsSetActive(false, m_Simulator.deviceMode == XRDeviceSimulator.DeviceMode.Hand);
                }
            }

            UpdateDeviceInputMethod();
#endif
        }

        private void OnCycleDevicesAction(InputAction.CallbackContext context)
        {
            if (context.phase.IsInProgress())
            {
                if (m_Simulator.manipulatingFPS)
                    OnActivateHeadsetDevice();

                if (m_Simulator.manipulatingLeftDevice)
                    OnActivateLeftDevice();

                if (m_Simulator.manipulatingRightDevice)
                    OnActivateRightDevice();
            }
        }

        private void OnStopManipulationAction(InputAction.CallbackContext context)
        {
            if (context.phase.IsInProgress())
                OnActivateHeadsetDevice(m_Simulator.manipulatingFPS);
        }

        private void OnToggleMouseTransformationModeAction(InputAction.CallbackContext context)
        {
            if (context.phase.IsInProgress())
                OnSetMouseMode();
        }

        private void OnNegateModeAction(InputAction.CallbackContext context)
        {
            OnSetMouseMode();
        }

        private void OnToggleCursorLockAction(InputAction.CallbackContext context)
        {
            if (context.phase.IsInProgress())
                OnCursorLockChanged();
        }

        private void OnKeyboardXTranslateAction(InputAction.CallbackContext context)
        {
            OnXAxisTranslatePerformed(context.phase.IsInProgress(), false);
        }

        private void OnKeyboardYTranslateAction(InputAction.CallbackContext context)
        {
            OnYAxisTranslatePerformed(context.phase.IsInProgress());
        }

        private void OnKeyboardZTranslateAction(InputAction.CallbackContext context)
        {
            OnZAxisTranslatePerformed(context.phase.IsInProgress(), false);
        }

        private void OnRestingHandAxis2DAction(InputAction.CallbackContext context)
        {
            var restingHandAxis2DInput = Vector2.ClampMagnitude(context.ReadValue<Vector2>(), 1f);
            if (context.phase.IsInProgress())
            {
                if (restingHandAxis2DInput.x != 0f)
                    OnXAxisTranslatePerformed(true, true);
                if (restingHandAxis2DInput.y != 0f)
                    OnZAxisTranslatePerformed(true, true);
            }
            else
            {
                if (restingHandAxis2DInput.x == 0f)
                    OnXAxisTranslatePerformed(false, true);
                if (restingHandAxis2DInput.y == 0f)
                    OnZAxisTranslatePerformed(false, true);
            }
        }

        private void OnGripAction(InputAction.CallbackContext context)
        {
            OnGripPerformed(context.phase.IsInProgress());
        }

        private void OnTriggerAction(InputAction.CallbackContext context)
        {
            OnTriggerPerformed(context.phase.IsInProgress());
        }

        private void OnMenuAction(InputAction.CallbackContext context)
        {
            OnMenuPerformed(context.phase.IsInProgress());
        }

        private void OnPrimaryButtonAction(InputAction.CallbackContext context)
        {
            OnPrimaryButtonPerformed(context.phase.IsInProgress());
        }

        private void OnSecondaryButtonAction(InputAction.CallbackContext context)
        {
            OnSecondaryButtonPerformed(context.phase.IsInProgress());
        }

        private void OnHandExpressionAction(XRDeviceSimulator.SimulatedHandExpression simulatedExpression, InputAction.CallbackContext context)
        {
            if (context.phase.IsInProgress())
            {
                if (m_Simulator.manipulatingLeftHand)
                    m_LeftHand.ToggleExpression(simulatedExpression, m_Simulator);

                if (m_Simulator.manipulatingRightHand)
                    m_RightHand.ToggleExpression(simulatedExpression, m_Simulator);
            }
        }

        private void OnSetMouseMode()
        {
            // Translate/Rotate
            m_MouseModeButtonText.text = m_Simulator.negateMode
                ? XRDeviceSimulator.Negate(m_Simulator.mouseTransformationMode).ToString()
                : m_Simulator.mouseTransformationMode.ToString();
            // Move/Look
            m_HeadsetLookButtonText.text =
                m_Simulator.mouseTransformationMode == XRDeviceSimulator.TransformationMode.Translate
                    ? k_TranslateLookText
                    : k_RotateLookText;
        }

        private void OnCursorLockChanged()
        {
            m_CursorLockButton.color = Cursor.lockState == CursorLockMode.Locked ? selectedColor : buttonColor;
        }

        // x-axis [A-D]
        private void OnXAxisTranslatePerformed(bool activated, bool restingHand)
        {
            var active = activated;
            if (!restingHand)
            {
                m_XAxisActivated = activated;
                active |= m_ZAxisActivated;
            }

            if (m_Simulator.manipulatingLeftController)
            {
                var lController = restingHand ? m_RightController : m_LeftController;
                lController.OnXAxisTranslatePerformed(active);
            }

            if (m_Simulator.manipulatingRightController)
            {
                var rController = restingHand ? m_LeftController : m_RightController;
                rController.OnXAxisTranslatePerformed(active);
            }

            if (m_Simulator.manipulatingFPS)
                m_HeadsetMoveButton.color = active ? selectedColor : buttonColor;
        }

        // y-axis [Q-E]
        private void OnYAxisTranslatePerformed(bool activated)
        {
            if (m_Simulator.manipulatingFPS)
                m_HeadsetMoveButton.color = activated ? selectedColor : buttonColor;
        }

        // z-axis [W-S]
        private void OnZAxisTranslatePerformed(bool activated, bool restingHand)
        {
            var active = activated;
            if (!restingHand)
            {
                m_ZAxisActivated = activated;
                active |= m_XAxisActivated;
            }

            if (m_Simulator.manipulatingLeftController)
            {
                var lController = restingHand ? m_RightController : m_LeftController;
                lController.OnZAxisTranslatePerformed(active);
            }

            if (m_Simulator.manipulatingRightController)
            {
                var rController = restingHand ? m_LeftController : m_RightController;
                rController.OnZAxisTranslatePerformed(active);
            }

            if (m_Simulator.manipulatingFPS)
                m_HeadsetMoveButton.color = active ? selectedColor : buttonColor;
        }

        private void OnMenuPerformed(bool activated)
        {
            if (m_Simulator.manipulatingLeftController)
                m_LeftController.OnMenu(activated);

            if (m_Simulator.manipulatingRightController)
                m_RightController.OnMenu(activated);
        }

        private void OnGripPerformed(bool activated)
        {
            if (m_Simulator.manipulatingLeftController)
                m_LeftController.OnGrip(activated);

            if (m_Simulator.manipulatingRightController)
                m_RightController.OnGrip(activated);
        }

        private void OnTriggerPerformed(bool activated)
        {
            if (m_Simulator.manipulatingLeftController)
                m_LeftController.OnTrigger(activated);

            if (m_Simulator.manipulatingRightController)
                m_RightController.OnTrigger(activated);
        }

        private void OnPrimaryButtonPerformed(bool activated)
        {
            if (m_Simulator.manipulatingLeftController)
                m_LeftController.OnPrimaryButton(activated);

            if (m_Simulator.manipulatingRightController)
                m_RightController.OnPrimaryButton(activated);
        }

        private void OnSecondaryButtonPerformed(bool activated)
        {
            if (m_Simulator.manipulatingLeftController)
                m_LeftController.OnSecondaryButton(activated);

            if (m_Simulator.manipulatingRightController)
                m_RightController.OnSecondaryButton(activated);
        }
    }
}
