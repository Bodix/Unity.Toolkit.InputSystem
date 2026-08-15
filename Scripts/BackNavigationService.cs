using System;
using System.Collections.Generic;
using Bodix.Evolunity.Services;
using UnityEngine.InputSystem;

namespace Toolkit.InputSystem
{
	public class BackNavigationService : IBackNavigationService, IDisposable
	{
		private readonly List<IBackNavigationHandler> _handlers = new List<IBackNavigationHandler>();
		private readonly InputAction _backAction;

		public event Action QuitRequested;

		public BackNavigationService()
		{
			_backAction = new InputAction("Back", InputActionType.Button, "<Keyboard>/escape");
			_backAction.AddBinding("*/{Back}");
			_backAction.AddBinding("<Gamepad>/buttonEast");

			_backAction.performed += OnBackPerformed;
			_backAction.Enable();
		}

		public void Register(IBackNavigationHandler handler)
		{
			if (_handlers.Contains(handler))
				_handlers.Remove(handler);

			_handlers.Add(handler);
		}

		public void Unregister(IBackNavigationHandler handler)
		{
			_handlers.Remove(handler);
		}

		private void OnBackPerformed(InputAction.CallbackContext context)
		{
			for (int i = _handlers.Count - 1; i >= 0; i--)
				if (_handlers[i].OnBackPressed())
					return;

			QuitRequested?.Invoke();
		}

		public void Dispose()
		{
			_backAction.performed -= OnBackPerformed;
			_backAction.Disable();
			_backAction.Dispose();
		}
	}
}