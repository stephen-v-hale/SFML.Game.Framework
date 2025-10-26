using System;
using System.Collections.Generic;
using System.Text;

namespace SFML.Game.Framework.Particle.Modifiers;

/// <summary>
/// Simulates magnetic attraction or repulsion around a single charge point.
/// </summary>
public class MagneticFieldModifier : IParticleModifier
{
    /// <summary>
    /// The position of the magnetic source.
    /// </summary>
    public Vector2 Center { get; set; }

    /// <summary>
    /// The strength of the magnetic force.
    /// Positive values attract, negative repel.
    /// </summary>
    public float Strength { get; set; } = 1000f;

    /// <summary>
    /// Minimum effective distance to avoid extreme forces.
    /// </summary>
    public float MinDistance { get; set; } = 10f;

    /// <summary>
    /// Maximum range where the effect applies.
    /// </summary>
    public float MaxDistance { get; set; } = 400f;

    /// <summary>
    /// Applies the magnetic force to the particle.
    /// </summary>
    /// <param name="p">The particle to modify.</param>
    /// <param name="delta">Time delta in seconds.</param>
    public void Apply( Particle p, float delta )
    {
        if ( !p.IsAlive ) return;

        Vector2 dir = Center - p.Position;
        float distSq = dir.LengthSquared();
        if ( distSq < MinDistance * MinDistance || distSq > MaxDistance * MaxDistance ) return;

        float dist = MathF.Sqrt(distSq);
        dir /= dist;

        // inverse-square law
        float force = Strength / distSq;
        p.Velocity += dir * force * delta;
    }
}