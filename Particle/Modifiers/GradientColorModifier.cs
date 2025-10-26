using System;
using System.Collections.Generic;
using System.Text;

namespace SFML.Game.Framework.Particle.Modifiers;

/// <summary>
/// Applies a color gradient to particles based on their lifetime progress.
/// Supports multiple gradient stops for rich effects.
/// </summary>
public class GradientColorModifier : IParticleModifier
{
    /// <summary>
    /// Represents a gradient stop containing color and normalized position.
    /// </summary>
    public struct GradientStop
    {
        public float Position; // 0..1
        public Color Color;

        public GradientStop( float position, Color color )
        {
            Position = position;
            Color = color;
        }
    }

    /// <summary>
    /// List of color gradient stops.
    /// </summary>
    public List<GradientStop> Stops { get; } = new();

    /// <summary>
    /// Interpolates colors linearly between gradient stops.
    /// </summary>
    /// <param name="p">The particle to modify.</param>
    /// <param name="delta">Delta time in seconds.</param>
    public void Apply( Particle p, float delta )
    {
        if ( !p.IsAlive || Stops.Count < 2 ) return;

        float t = 1f - (p.Lifetime / p.TotalLifetime);
        GradientStop lower = Stops[0], upper = Stops[^1];

        for ( int i = 0; i < Stops.Count - 1; i++ )
        {
            if ( t >= Stops[i].Position && t <= Stops[i + 1].Position )
            {
                lower = Stops[i];
                upper = Stops[i + 1];
                break;
            }
        }

        float range = upper.Position - lower.Position;
        float localT = (range <= 0f) ? 0f : (t - lower.Position) / range;

        p.Color = LerpColor( lower.Color, upper.Color, localT );
    }

    /// <summary>
    /// Performs linear interpolation between two colors.
    /// </summary>
    private static Color LerpColor( Color a, Color b, float t )
    {
        t = Math.Clamp( t, 0f, 1f );
        return new Color(
            ( int )( a.R + ( b.R - a.R ) * t ),
            ( int )( a.G + ( b.G - a.G ) * t ),
            ( int )( a.B + ( b.B - a.B ) * t ),
            ( int )( a.A + ( b.A - a.A ) * t )
        );
    }
}
