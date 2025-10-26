using System;
using System.Collections.Generic;
using System.Text;

namespace SFML.Game.Framework.Particle.Modifiers;

/// <summary>
/// Controls particle rotation speed and direction over its lifetime.
/// </summary>
public class RotationOverLifeModifier : IParticleModifier
{
    /// <summary>
    /// Rotation at birth (degrees per second).
    /// </summary>
    public float StartRotation { get; set; } = 0f;

    /// <summary>
    /// Rotation at death (degrees per second).
    /// </summary>
    public float EndRotation { get; set; } = 360f;

    /// <summary>
    /// Determines if the rotation interpolation should be eased.
    /// </summary>
    public bool Smooth { get; set; } = true;

    /// <summary>
    /// Applies the rotation effect to the given particle.
    /// </summary>
    /// <param name="p">The particle to modify.</param>
    /// <param name="delta">Delta time in seconds.</param>
    public void Apply( Particle p, float delta )
    {
        if ( !p.IsAlive ) return;

        float t = 1f - (p.Lifetime / p.TotalLifetime);
        if ( Smooth )
            t = t * t * ( 3f - 2f * t );

        float newRotation = StartRotation + (EndRotation - StartRotation) * t;
        p.Rotation = newRotation;
    }
}