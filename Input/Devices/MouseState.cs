/*
 *--------------------------------------------------
 * Auther: Stephen Hale
 * File: MouseState.cs
 *-------------------------------------------------
*/

namespace SFML.Game.Framework.Input.Devices;

/// <summary>
/// Represents the current state of the mouse, including button states and position.
/// </summary>
public class MouseState
{
    bool[] _pressedButtons;

    /// <summary>
    /// Gets the current X position of the mouse cursor relative to the desktop.
    /// </summary>
    public float X
    {
        get
        {
            return SFML.Window.Mouse.GetPosition( Game.window ).X;
        }
    }

    /// <summary>
    /// Gets the current Y position of the mouse cursor relative to the desktop.
    /// </summary>
    public float Y
    {
        get
        {
            return SFML.Window.Mouse.GetPosition( Game.window ).Y;
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MouseState"/> class.
    /// </summary>
    public MouseState()
    {
        _pressedButtons = new bool[5];
    }

    /// <summary>
    /// Updates the state of all mouse buttons.
    /// </summary>
    public void GetState()
    {
        for ( int i = 0; i < _pressedButtons.Length; i++ )
        {
            _pressedButtons[i] = SFML.Window.Mouse.IsButtonPressed( ( SFML.Window.Mouse.Button )i );
        }
    }

    /// <summary>
    /// Determines whether the specified mouse button is currently pressed.
    /// </summary>
    /// <param name="button">The mouse button to check.</param>
    /// <returns><c>true</c> if the button is pressed; otherwise, <c>false</c>.</returns>
    public bool IsButtonDown( MouseButton button )
    {
        var index = (int)button;

        return _pressedButtons[index];
    }
}
