/*
 *--------------------------------------------------
 * Auther: Stephen Hale
 * File: MouseButton.cs
 *-------------------------------------------------
*/

namespace SFML.Game.Framework.Input.Devices;


/// <summary>
/// Specifies mouse buttons that can be used as input.
/// </summary>
public enum MouseButton
{
    /// <summary>
    /// The left mouse button.
    /// </summary>
    Left   = 0,
    /// <summary>
    /// The right mouse button.
    /// </summary>
    Right  = 1,
    /// <summary>
    /// The middle mouse button (usually the scroll wheel button).
    /// </summary>
    Middle = 2,
    /// <summary>
    /// The first extended mouse button (typically a side button).
    /// </summary>
    XButton1 = 3,
    /// <summary>
    /// The second extended mouse button (typically a side button).
    /// </summary>
    XButton2 = 4,

    /// <summary>
    /// Indicates that no mouse button is pressed or specified.
    /// </summary>
    None,
}
