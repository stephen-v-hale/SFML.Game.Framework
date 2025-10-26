using System;
using System.Collections.Generic;
using System.Text;

namespace SFML.Game.Framework.Particle.Modifiers;

/// <summary>
/// Rotates particles around a center point, creating a swirling vortex effect.
/// </summary>
public class VortexModifier : IParticleModifier
{
    /// <summary>
    /// The center point of the vortex.
    /// </summary>
    public Vector2 Center { get; set; }

    /// <summary>
    /// The strength of the rotational pull (degrees per second).
    /// </summary>
    public float AngularStrength { get; set; } = 180f;

    /// <summary>
    /// The strength of the inward pull towards the vortex center.
    /// </summary>
    public float InwardForce { get; set; } = 0f;

    /// <summary>
    /// Maximum range where particles are affected.
    /// </summary>
    public float Radius { get; set; } = 500f;

    /// <summary>
    /// Applies the vortex force to the particle.
    /// </summary>
    /// <param name="p">The particle to modify.</param>
    /// <param name="delta">Time delta in seconds.</param>
    public void Apply( Particle p, float delta )
    {
        if ( !p.IsAlive ) return;

        Vector2 offset = p.Position - Center;
        float dist = offset.Length();
        if ( dist > Radius || dist < 1e-4f ) return;

        offset /= dist;

        // perpendicular vector for circular motion
        Vector2 tangent = new(-offset.Y, offset.X);

        // apply rotational force
        p.Velocity += tangent * AngularStrength * ( 1f - dist / Radius ) * delta;

        // optional inward pull
        if ( InwardForce != 0f )
            p.Velocity -= offset * InwardForce * delta;
    }
}
