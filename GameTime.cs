using System;
using System.Collections.Generic;
using System.Text;

namespace SFML.Game.Framework;

/// <summary>
/// Represents the frame time.
/// </summary>
public class GameTime
{
    /// <summary>
    /// Gets the total elapsed time.
    /// </summary>
    public TimeSpan ElapsedGameTime { get; internal set; }

    /// <summary>
    /// Gets the total frame time.
    /// </summary>
    public TimeSpan TotalGameTime { get; internal set; }

    /// <summary>
    /// Gets the delta time.
    /// </summary>
    public float Delta { get; internal set; }
}
