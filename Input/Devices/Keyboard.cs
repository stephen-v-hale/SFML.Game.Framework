/*
 *--------------------------------------------------
 * Auther: Stephen Hale
 * File: Keyboard.cs
 *-------------------------------------------------
*/

namespace SFML.Game.Framework.Input.Devices;

/// <summary>
/// Represents a keyboard input device and provides methods to query key states.
/// </summary>
public class Keyboard : InputDevice
{
    /// <summary>
    /// The current state of the keyboard for each device index.
    /// </summary>
    KeyboardState[] currentState, previousState;

    /// <summary>
    /// Initializes a new instance of the <see cref="Keyboard"/> class.
    /// </summary>
    /// <param name="name">The name of the keyboard device.</param>
    /// <param name="index">The index of the keyboard device.</param>
    public Keyboard( string name, InputDeviceIndex index ) : base( name, index )
    {
        currentState = new KeyboardState[( int )InputDeviceIndex.Max];
        previousState = new KeyboardState[( int )InputDeviceIndex.Max];
    }

    /// <summary>
    /// Updates the keyboard state for all device indices.
    /// </summary>
    public override void Update()
    {
        for ( int i = 0; i < 4; i++ )
        {
            previousState[i] = currentState[i];

            currentState[i] = new KeyboardState();
            currentState[i].GetState();
        }
    }

    /// <summary>
    /// Determines whether the specified key is down, optionally checking if it is being held.
    /// </summary>
    /// <param name="key">The key to check.</param>
    /// <param name="held">True to check if the key is held, false for a new press.</param>
    /// <returns>True if the key is down (or held), otherwise false.</returns>
    public bool IsKeyDown( Keys key, bool held )
    {
        var index = (int)Index;

        return held switch
        {
            true => currentState[index].IsKeyDown( key ),
            false => currentState[index].IsKeyDown( key ) && previousState[index].IsKeyUp( key )
        };
    }

    /// <summary>
    /// Determines whether the specified key is currently down (not held).
    /// </summary>
    /// <param name="key">The key to check.</param>
    /// <returns>True if the key is down, otherwise false.</returns>
    public bool IsKeyDown( Keys key ) => IsKeyDown( key, false );

    /// <summary>
    /// Determines whether the specified key (by name) is down, optionally checking if it is being held.
    /// </summary>
    /// <param name="keyName">The name of the key to check.</param>
    /// <param name="held">True to check if the key is held, false for a new press.</param>
    /// <returns>True if the key is down (or held), otherwise false.</returns>
    public bool IsKeyDown( string keyName, bool held ) => IsKeyDown( ( Keys )Enum.Parse( typeof( Keys ), keyName ), held );

    /// <summary>
    /// Determines whether the specified key (by name) is currently down (not held).
    /// </summary>
    /// <param name="keyName">The name of the key to check.</param>
    /// <returns>True if the key is down, otherwise false.</returns>
    public bool IsKeyDown( string keyName ) => IsKeyDown( ( Keys )Enum.Parse( typeof( Keys ), keyName ), false );

    /// <summary>
    /// Determines whether the specified key is currently up.
    /// </summary>
    /// <param name="key">The key to check.</param>
    /// <returns>True if the key is up, otherwise false.</returns>
    public bool IsKeyUp( Keys key )
    {
        var index = (int)Index;
        return currentState[index].IsKeyUp( key );
    }

    /// <summary>
    /// Determines whether the specified key (by name) is currently up.
    /// </summary>
    /// <param name="keyName">The name of the key to check.</param>
    /// <returns>True if the key is up, otherwise false.</returns>
    public bool IsKeyUp( string keyName ) => IsKeyUp( ( Keys )Enum.Parse( typeof( Keys ), keyName ) );
}