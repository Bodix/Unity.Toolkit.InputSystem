using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.UI;

namespace Toolkit.InputSystem
{
    // TODO:
    // 1. Add position handling (in addition to position delta handling).
    // 2. Add pointer index.

    // https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/OnScreen.html
    [AddComponentMenu("Input/On-Screen Input Rect")]
    [RequireComponent(typeof(Graphic))]
    public class OnScreenInputRect : OnScreenControl, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        [InputControl(layout = "Vector2")]
        [SerializeField]
        private string _controlPath;

        private Vector2 _prevPointerPosition;
        private Vector2 _currentPointerPosition;
        private bool _isPointerDown;

        protected override string controlPathInternal
        {
            get => _controlPath;
            set => _controlPath = value;
        }

        private void Update()
        {
            if (_isPointerDown)
            {
                SendValueToControl(_currentPointerPosition - _prevPointerPosition);

                _prevPointerPosition = _currentPointerPosition;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _isPointerDown = true;
            _prevPointerPosition = eventData.position;
            _currentPointerPosition = eventData.position;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isPointerDown = false;

            SendValueToControl(Vector2.zero);
        }

        // OnDrag works only while the pointer is moving and not while the pointer is held,
        // so we have to calculate the delta in Update by ourselves.
        public void OnDrag(PointerEventData eventData)
        {
            _currentPointerPosition = eventData.position;
        }
    }
}