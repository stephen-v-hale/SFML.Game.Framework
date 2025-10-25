/*
 *--------------------------------------------------
 * Author: Stephen Hale
 * File: Mouse.cs
 *-------------------------------------------------
*/

namespace SFML.Game.Framework.Input.Devices;

#nullable disable
/// <summary>
/// Represents a mouse input device, providing access to mouse position and button states.
/// </summary>
public class Mouse : InputDevice
{
    MouseState current, previous;

    /// <summary>
    /// Gets the current position of the mouse cursor.
    /// </summary>
    public Point Position
    {
        get => new Point( (int)current.X, (int)current.Y );
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Mouse"/> class with the specified name.
    /// </summary>
    /// <param name="name">The name of the mouse device.</param>
    public Mouse( string name ) : base( name, InputDeviceIndex.One )
    {
    }

    /// <summary>
    /// Updates the mouse state, storing the previous and current states.
    /// </summary>
    public override void Update()
    {
        previous = current;

        current = new MouseState();
        current.GetState();
    }

    /// <summary>
    /// Determines whether the specified mouse button is down.
    /// </summary>
    /// <param name="button">The mouse button to check.</param>
    /// <param name="held">If true, checks if the button is held; if false, checks if it was just pressed.</param>
    /// <returns><c>true</c> if the button is down; otherwise, <c>false</c>.</returns>
    public bool IsButtonDown( MouseButton button, bool held )
    {
        return held switch
        {
            true => current.IsButtonDown( button ),
            false => current.IsButtonDown( button ) && !previous.IsButtonDown( button )
        };
    }

    /// <summary>
    /// Determines whether the specified mouse button was just pressed.
    /// </summary>
    /// <param name="button">The mouse button to check.</param>
    /// <returns><c>true</c> if the button was just pressed; otherwise, <c>false</c>.</returns>
    public bool IsButtonDown( MouseButton button ) => IsButtonDown( button, false );

    /// <summary>
    /// Determines whether the specified mouse button (by name) is down.
    /// </summary>
    /// <param name="buttonName">The name of the mouse button to check.</param>
    /// <param name="held">If true, checks if the button is held; if false, checks if it was just pressed.</param>
    /// <returns><c>true</c> if the button is down; otherwise, <c>false</c>.</returns>
    public bool IsButtonDown( string buttonName, bool held ) => IsButtonDown( ( MouseButton )Enum.Parse( typeof( MouseButton ), buttonName ), held );

    /// <summary>
    /// Determines whether the specified mouse button (by name) was just pressed.
    /// </summary>
    /// <param name="buttonName">The name of the mouse button to check.</param>
    /// <returns><c>true</c> if the button was just pressed; otherwise, <c>false</c>.</returns>
    public bool IsButtonDown( string buttonName ) => IsButtonDown( ( MouseButton )Enum.Parse( typeof( MouseButton ), buttonName ), false );
}