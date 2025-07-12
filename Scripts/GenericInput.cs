using UnityEngine;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;

namespace Toolkit.InputSystem
{
    [AddComponentMenu("Input/Generic Input")]
    public class GenericInput : OnScreenControl
    {
        [InputControl]
        [SerializeField] private string _controlPath;

        protected override string controlPathInternal
        {
            get => _controlPath;
            set => _controlPath = value;
        }

        public void SendInput<T>(T value) where T : struct
        {
            SendValueToControl(value);
        }
    }
}