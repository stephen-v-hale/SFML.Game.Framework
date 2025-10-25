/*
 *--------------------------------------------------
 * Auther: Stephen Hale
 * File: InputDevice.cs
 *-------------------------------------------------
*/

using System.Runtime.InteropServices;

namespace SFML.Game.Framework.Input;

/// <summary>
/// Represents the index of an input device.
/// </summary>
public enum InputDeviceIndex : int
{
    One = 0,
    Two = 1,
    Three = 2,
    Four = 3,

    Max = 4,
}

/// <summary>
/// Abstract base class for input devices.
/// </summary>
public abstract class InputDevice
{
    /// <summary>
    /// Gets the name of the input device.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the index of the input device.
    /// </summary>
    public InputDeviceIndex Index { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="InputDevice"/> class.
    /// </summary>
    /// <param name="name">The name of the device.</param>
    /// <param name="index">The index of the device.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="name"/> is null.</exception>
    public InputDevice( string name, InputDeviceIndex index )
    {
        Name = name ?? throw new ArgumentNullException( nameof( name ) );
        Index = index;
    }

    /// <summary>
    /// Update this device.
    /// </summary>
    public abstract void Update();
}
