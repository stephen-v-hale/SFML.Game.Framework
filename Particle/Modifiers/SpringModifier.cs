using System;
using System.Collections.Generic;
using System.Text;

namespace SFML.Game.Framework.Particle.Modifiers;

/// <summary>
/// Connects particles to an anchor point with a virtual spring.
/// Produces elastic motion and damping.
/// </summary>
public class SpringModifier : IParticleModifier
{
    /// <summary>
    /// The anchor point of the spring.
    /// </summary>
    public Vector2 Anchor { get; set; }

    /// <summary>
    /// The spring constant controlling the pull strength.
    /// </summary>
    public float Stiffness { get; set; } = 10f;

    /// <summary>
    /// The damping coefficient to slow down oscillations.
    /// </summary>
    public float Damping { get; set; } = 0.9f;

    /// <summary>
    /// The rest length of the spring (distance at which no force is applied).
    /// </summary>
    public float RestLength { get; set; } = 50f;

    /// <summary>
    /// Applies spring physics to a particle.
    /// </summary>
    /// <param name="p">The particle to modify.</param>
    /// <param name="delta">Time step in seconds.</param>
    public void Apply( Particle p, float delta )
    {
        if ( !p.IsAlive ) return;

        Vector2 dir = Anchor - p.Position;
        float dist = dir.Length();
        if ( dist < 1e-4f ) return;

        dir /= dist;

        float displacement = dist - RestLength;
        Vector2 force = dir * displacement * Stiffness;

        p.Velocity += force * delta;
        p.Velocity *= 1f - Damping * delta;
    }
}