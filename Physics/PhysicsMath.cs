using System;
using System.Collections.Generic;
using System.Text;

namespace SFML.Game.Framework.Physics;

/// <summary>
/// Provides mathematical helper functions for 2D physics calculations.
/// </summary>
public static class PhysicsMath
{
    /// <summary>
    /// Clamps a value between a minimum and maximum.
    /// </summary>
    public static float Clamp( float value, float min, float max )
        => Math.Max( min, Math.Min( max, value ) );

    /// <summary>
    /// Projects vector <paramref name="a"/> onto vector <paramref name="b"/>.
    /// </summary>
    public static Vector2 Project( Vector2 a, Vector2 b )
        => b * ( Vector2.Dot( a, b ) / b.LengthSquared() );
}