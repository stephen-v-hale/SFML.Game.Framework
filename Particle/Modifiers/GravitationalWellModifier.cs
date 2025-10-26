using System;
using System.Collections.Generic;
using System.Text;

namespace SFML.Game.Framework.Particle.Modifiers;

/// <summary>
/// Simulates a gravitational pull similar to a planet or black hole.
/// Uses an inverse-square law.
/// </summary>
public class GravitationalWellModifier : IParticleModifier
{
    /// <summary>
    /// The position of the gravity source.
    /// </summary>
    public Vector2 Center { get; set; }

    /// <summary>
    /// Gravitational constant controlling attraction strength.
    /// </summary>
    public float Gravity { get; set; } = 5000f;

    /// <summary>
    /// Minimum distance to avoid excessive acceleration.
    /// </summary>
    public float MinDistance { get; set; } = 10f;

    /// <summary>
    /// Maximum range where gravity affects particles.
    /// </summary>
    public float MaxDistance { get; set; } = 800f;

    /// <summary>
    /// Applies gravitational pull toward the center.
    /// </summary>
    /// <param name="p">The particle to modify.</param>
    /// <param name="delta">Time step in seconds.</param>
    public void Apply( Particle p, float delta )
    {
        if ( !p.IsAlive ) return;

        Vector2 dir = Center - p.Position;
        float distSq = dir.LengthSquared();
        if ( distSq < MinDistance * MinDistance || distSq > MaxDistance * MaxDistance ) return;

        float dist = MathF.Sqrt(distSq);
        dir /= dist;

        float force = Gravity / distSq;
        p.Velocity += dir * force * delta;
    }
}