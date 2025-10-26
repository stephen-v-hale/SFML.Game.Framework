using System;
using System.Collections.Generic;
using System.Text;

namespace SFML.Game.Framework;

/// <summary>
/// Represents the frame time with time scaling support.
/// All values are computed automatically via properties.
/// </summary>
public class GameTime
{
    /// <summary>
    /// Gets or sets the unscaled elapsed time since the last frame.
    /// </summary>
    public TimeSpan UnscaledElapsedGameTime { get; set; }

    /// <summary>
    /// Gets or sets the total unscaled game time since the start.
    /// </summary>
    public TimeSpan TotalUnscaledGameTime { get; set; }

    /// <summary>
    /// Gets or sets the unscaled delta time in seconds.
    /// </summary>
    public float UnscaledDelta { get; set; }

    /// <summary>
    /// Gets or sets the time scale (1.0 = normal speed, 0.5 = half speed, etc.)
    /// </summary>
    public float TimeScale { get; set; } = 1.0f;

    // --- Derived properties ---

    /// <summary>
    /// Gets the elapsed game time since the last frame, affected by TimeScale.
    /// </summary>
    public TimeSpan ElapsedGameTime => TimeSpan.FromSeconds( UnscaledElapsedGameTime.TotalSeconds * TimeScale );

    /// <summary>
    /// Gets the total game time since the start, affected by TimeScale.
    /// </summary>
    public TimeSpan TotalGameTime => TimeSpan.FromSeconds( TotalUnscaledGameTime.TotalSeconds * TimeScale );

    /// <summary>
    /// Gets the delta time in seconds, affected by TimeScale.
    /// </summary>
    public float Delta => UnscaledDelta * TimeScale;

    /// <summary>
    /// Gets the delta time in milliseconds, affected by TimeScale.
    /// </summary>
    public float DeltaMilliseconds => Delta * 1000f;

    /// <summary>
    /// Gets the instantaneous FPS based on scaled delta.
    /// </summary>
    public float FPS => Delta > 0f ? 1f / Delta : 0f;

    /// <summary>
    /// Gets the instantaneous FPS based on unscaled delta.
    /// </summary>
    public float UnscaledFPS => UnscaledDelta > 0f ? 1f / UnscaledDelta : 0f;

    /// <summary>
    /// Gets the inverse of scaled delta.
    /// </summary>
    public float InverseDelta => Delta > 0f ? 1f / Delta : 0f;

    /// <summary>
    /// Gets the inverse of unscaled delta.
    /// </summary>
    public float InverseUnscaledDelta => UnscaledDelta > 0f ? 1f / UnscaledDelta : 0f;
}