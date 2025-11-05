

namespace SFML.Game.Framework.Input;

#nullable disable
/// <summary>
/// Provides static methods to manage and update input devices in the engine.
/// </summary>
public static class InputSystem
{
    static List<InputAction> _actions = new List<InputAction>();

    /// <summary>
    /// The list of registered input devices.
    /// </summary>
    static List<InputDevice> _devices = new List<InputDevice>();

    /// <summary>
    /// Adds a new input device of type <typeparamref name="T"/> to the system.
    /// </summary>
    /// <typeparam name="T">The type of input device.</typeparam>
    /// <param name="device">The input device to add.</param>
    public static void AddDevice<T>( T device ) where T : InputDevice => _devices.Add( device );

    /// <summary>
    /// Adds a new input device to the system.
    /// </summary>
    /// <param name="device">The input device to add.</param>
    public static void AddDevice( InputDevice device ) => _devices.Add( device );

    /// <summary>
    /// Removes an input device from the system.
    /// </summary>
    /// <param name="device">The input device to remove.</param>
    public static void RemoveDevice( InputDevice device ) => _devices.Remove( device );

    /// <summary>
    /// Gets the first input device of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of input device.</typeparam>
    /// <returns>The first device of type <typeparamref name="T"/>, or null if none found.</returns>
    public static T GetDevice<T>() where T : InputDevice
    {
        if ( _devices.Count == 0 )
            return null;

        return _devices.OfType<T>().FirstOrDefault();
    }

    /// <summary>
    /// Gets the first input device of type <typeparamref name="T"/> with the specified name.
    /// </summary>
    /// <typeparam name="T">The type of input device.</typeparam>
    /// <param name="name">The name of the device.</param>
    /// <returns>The device of type <typeparamref name="T"/> with the specified name, or null if none found.</returns>
    public static T GetDevice<T>( string name ) where T : InputDevice
    {
        if ( _devices.Count == 0 )
            return null;

        return _devices.OfType<T>().FirstOrDefault( d => d.Name == name );
    }

    /// <summary>
    /// Adds a <see cref="InputAction"/> buy its <see cref="Type"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="action"></param>
    public static void AddAction<T>( T action ) where T : InputAction
    {
        _actions.Add( action );
    }

    /// <summary>
    /// Gets a specific <see cref="InputAction"/> from its <see cref="Type"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T GetAction<T>() where T : InputAction
    {
        if ( _actions.Count == 0 )
            return null;

        return _actions.OfType<T>().FirstOrDefault();
    }

    /// <summary>
    /// Updates all registered input devices.
    /// </summary>
    internal static void Update()
    {
        foreach ( var device in _devices )
        {
            device.Update();
        }
    }
}
