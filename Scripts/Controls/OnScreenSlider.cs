using UnityEngine;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.UI;

namespace Toolkit.InputSystem
{
	[AddComponentMenu("Input/On-Screen Slider")]
	[RequireComponent(typeof(Slider))]
	public class OnScreenSlider : OnScreenControl
	{
		[InputControl(layout = "Axis")]
		[SerializeField]
		private string _controlPath;

		private Slider _slider;

		protected override string controlPathInternal
		{
			get => _controlPath;
			set => _controlPath = value;
		}

		private void Awake()
		{
			_slider = GetComponent<Slider>();
			_slider.onValueChanged.AddListener(SendSliderValue);
		}

		private void OnDestroy()
		{
			if (_slider != null)
				_slider.onValueChanged.RemoveListener(SendSliderValue);
		}

		private void SendSliderValue(float value)
		{
			SendValueToControl(value);
		}
	}
}