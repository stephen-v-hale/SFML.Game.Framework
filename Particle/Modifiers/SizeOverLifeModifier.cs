using System;
using System.Collections.Generic;
using System.Text;

namespace SFML.Game.Framework.Particle.Modifiers;

/// <summary>
/// Interpolates particle size over its lifetime using a smooth curve.
/// Ideal for effects like fire, explosions, or dissipating smoke.
/// </summary>
public class SizeOverLifeModifier : IParticleModifier
{
    /// <summary>
    /// Starting size multiplier at birth (1 = no change).
    /// </summary>
    public float StartMultiplier { get; set; } = 1f;

    /// <summary>
    /// Ending size multiplier at death (1 = no change).
    /// </summary>
    public float EndMultiplier { get; set; } = 0f;

    /// <summary>
    /// If true, uses a smooth step curve instead of linear interpolation.
    /// </summary>
    public bool Smooth { get; set; } = true;

    /// <summary>
    /// Applies the size-over-life effect to the given particle.
    /// </summary>
    /// <param name="p">The particle to modify.</param>
    /// <param name="delta">Delta time in seconds.</param>
    public void Apply( Particle p, float delta )
    {
        if ( !p.IsAlive ) return;

        float t = 1f - (p.Lifetime / p.TotalLifetime);
        if ( Smooth )
            t = t * t * ( 3f - 2f * t ); // smoothstep

        float multiplier = StartMultiplier + (EndMultiplier - StartMultiplier) * t;
        p.Size = p.StartSize * multiplier;
    }
}