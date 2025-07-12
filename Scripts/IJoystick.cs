using UnityEngine;

namespace Toolkit.InputSystem
{
    /// <summary>
    /// Designed to work with <a href="https://assetstore.unity.com/packages/tools/input-management/joystick-pack-107631"> Joystick Pack</a>
    /// </summary>
    public interface IJoystick
    {
        Vector2 Direction { get; }
    }
}
