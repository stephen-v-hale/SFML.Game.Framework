/*
 *--------------------------------------------------
 * Auther: Stephen Hale
 * File: GamePad.cs
 *-------------------------------------------------
*/

namespace SFML.Game.Framework.Input.Devices;

/// <summary>
/// Represents a game controller input device, providing access to button and axis states.
/// </summary>
public class Controller : InputDevice
{
    /// <summary>
    /// Stores the current state of the controller for each possible index.
    /// </summary>
    ControllerState[] current, previous;

    /// <summary>
    /// Gets a value indicating whether the controller is connected.
    /// </summary>
    public bool IsConnected => current[( int )Index].IsConnected( ( int )Index );

    /// <summary>
    /// Gets the position of the specified axis for the current controller index.
    /// </summary>
    /// <param name="axis">The axis to query.</param>
    /// <returns>The position of the axis.</returns>
    public float GetAxis( ControllerAxis axis ) => current[( int )Index].AxisPosition( axis, ( int )Index );

    /// <summary>
    /// Initializes a new instance of the <see cref="Controller"/> class.
    /// </summary>
    /// <param name="name">The name of the controller.</param>
    /// <param name="index">The index of the controller.</param>
    public Controller( string name, InputDeviceIndex index ) : base( name, index )
    {
        current = new ControllerState[4];
        previous = new ControllerState[4];
    }

    /// <summary>
    /// Updates the state of the controller, storing the previous and current states.
    /// </summary>
    public override void Update()
    {
        for ( int i = 0; i < 4; i++ )
        {
            previous[i] = current[i];
            current[i].GetState( ( int )Index );
        }
    }

    /// <summary>
    /// Determines whether the specified button is down, with an option to check for held state.
    /// </summary>
    /// <param name="button">The button to check.</param>
    /// <param name="held">True to check if the button is held, false for a new press.</param>
    /// <returns>True if the button is down as specified; otherwise, false.</returns>
    public bool IsButtonDown( ControllerButton button, bool held )
    {
        return held switch
        {
            true => current[( int )Index].IsButtonDown( button ),
            false => current[( int )Index].IsButtonDown( button ) && !previous[( int )Index].IsButtonDown( button ),
        };
    }

    /// <summary>
    /// Determines whether the specified button is newly pressed.
    /// </summary>
    /// <param name="button">The button to check.</param>
    /// <returns>True if the button is newly pressed; otherwise, false.</returns>
    public bool IsButtonDown( ControllerButton button ) => IsButtonDown( button, false );

    /// <summary>
    /// Determines whether the specified button (by name) is down, with an option to check for held state.
    /// </summary>
    /// <param name="buttonName">The name of the button to check.</param>
    /// <param name="held">True to check if the button is held, false for a new press.</param>
    /// <returns>True if the button is down as specified; otherwise, false.</returns>
    public bool IsButtonDown( string buttonName, bool held ) => IsButtonDown( EnumHelper.Parse<ControllerButton>( buttonName ), held );

    /// <summary>
    /// Determines whether the specified button (by name) is newly pressed.
    /// </summary>
    /// <param name="buttonName">The name of the button to check.</param>
    /// <returns>True if the button is newly pressed; otherwise, false.</returns>
    public bool IsButtonDown( string buttonName ) => IsButtonDown( EnumHelper.Parse<ControllerButton>( buttonName ), false );

    /// <summary>
    /// Determines whether the specified button is currently up (not pressed).
    /// </summary>
    /// <param name="button">The button to check.</param>
    /// <returns>True if the button is up; otherwise, false.</returns>
    public bool IsButtonUp( ControllerButton button ) => !IsButtonDown( button, true );
}