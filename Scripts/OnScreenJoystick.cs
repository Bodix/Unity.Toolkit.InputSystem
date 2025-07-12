using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;

namespace Toolkit.InputSystem
{
    // https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/OnScreen.html

    /// <summary>
    /// Designed to work with <a href="https://assetstore.unity.com/packages/tools/input-management/joystick-pack-107631"> Joystick Pack</a>
    /// </summary>
    // TODO: Fix [RequireComponent] attribute in some way. [#bug]
    // [RequireComponent(typeof(Joystick))]
    [AddComponentMenu("Input/On-Screen Joystick")]
    public class OnScreenJoystick : OnScreenControl, IPointerUpHandler
    {
        [InputControl(layout = "Stick")]
        [SerializeField]
        private string _controlPath;

        private IJoystick _joystick;

        protected override string controlPathInternal
        {
            get => _controlPath;
            set => _controlPath = value;
        }

        private void Awake()
        {
            _joystick = GetComponent<IJoystick>();
        }

        private void Update()
        {
            if (_joystick.Direction.sqrMagnitude > 0)
                SendValueToControl(_joystick.Direction);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            SendValueToControl(Vector2.zero);
        }
    }
}