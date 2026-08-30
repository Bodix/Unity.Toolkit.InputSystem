using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;

namespace Toolkit.InputSystem
{
	[AddComponentMenu("Input/Cancellable On-Screen Button")]
	public class CancellableOnScreenButton : OnScreenControl, IPointerDownHandler, IPointerUpHandler, IDragHandler
	{
		[InputControl(layout = "Button")]
		[SerializeField]
		private string _controlPath;

		[Space]
		public UnityEvent OnPress;
		public UnityEvent OnReleaseInside;
		public UnityEvent OnReleaseOutside;

		protected override string controlPathInternal
		{
			get => _controlPath;
			set => _controlPath = value;
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			SendValueToControl(1.0f);
			OnPress?.Invoke();
		}

		/// <summary>
		/// An empty method is required. Without an IDragHandler, Unity will stop
		/// tracking the finger if it moves away from the initial tap location.
		/// </summary>
		public void OnDrag(PointerEventData eventData)
		{
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			RectTransform rect = transform as RectTransform;

			if (RectTransformUtility.RectangleContainsScreenPoint(rect, eventData.position, eventData.pressEventCamera))
				OnReleaseInside?.Invoke();
			else
				OnReleaseOutside?.Invoke();

			SendValueToControl(0.0f);
		}
	}
}