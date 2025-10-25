/*
 *--------------------------------------------------
 * Auther: Stephen Hale
 * File: GamePadState.cs
 *-------------------------------------------------
*/

using SFML.Window;

namespace SFML.Game.Framework.Input.Devices;

/// <summary>
/// Represents the axes available on a gamepad.
/// </summary>
public enum ControllerAxis
{
    PovX, PovY,
    X,
    Y,
    Z,
    R,
    U,
    V,
}

/// <summary>
/// Represents the buttons available on a gamepad.
/// </summary>
public enum ControllerButton
{
    X, Y, A, B,

    LeftBumper, RightBumper, Back, Start,
    LeftStick, RightStick, LeftTrigger, RightTrigger,
    Middle,
}

/// <summary>
/// Represents the state of a gamepad controller, including button and axis states.
/// </summary>
public class ControllerState
{
    bool[] buttons;

    /// <summary>
    /// Initializes a new instance of the <see cref="ControllerState"/> class.
    /// </summary>
    public ControllerState()
    {
        buttons = new bool[13];
    }

    /// <summary>
    /// Checks if the specified joystick index is connected.
    /// </summary>
    /// <param name="index">The joystick index to check.</param>
    /// <returns>True if the joystick is connected; otherwise, false.</returns>
    public bool IsConnected( int index )
    {
        return Joystick.IsConnected( ( uint )index );
    }

    /// <summary>
    /// Gets the position of the specified axis for the given joystick index.
    /// </summary>
    /// <param name="axis">The axis to query.</param>
    /// <param name="joyPadIndex">The joystick index.</param>
    /// <returns>The position of the axis.</returns>
    public float AxisPosition( ControllerAxis axis, int joyPadIndex )
    {
        return Joystick.GetAxisPosition( ( uint )joyPadIndex, ( Joystick.Axis )axis );
    }

    /// <summary>
    /// Updates the state of the gamepad buttons for the specified joystick index.
    /// </summary>
    /// <param name="joyPadIndex">The joystick index to update.</param>
    public void GetState( int joyPadIndex )
    {
        Joystick.Update();
        for ( int i = 0; i < buttons.Length; i++ )
        {
            buttons[i] = Joystick.IsButtonPressed( ( uint )joyPadIndex, ( uint )i );
        }
    }

    /// <summary>
    /// Checks if a button is currently pressed down.
    /// </summary>
    /// <param name="button">The button to check.</param>
    /// <returns>True if the button is pressed; otherwise, false.</returns>
    public bool IsButtonDown( ControllerButton button )
    {
        return buttons[( int )button];
    }
}
