using System;
using System.Collections.Generic;
using System.Text;

namespace SFML.Game.Framework.Particle.Modifiers;
/// <summary>
/// Applies a single outward impulse from a given explosion origin.
/// Perfect for debris, sparks, or shockwaves.
/// </summary>
public class ExplosionForceModifier : IParticleModifier
{
    /// <summary>
    /// Explosion center position.
    /// </summary>
    public Vector2 Origin { get; set; }

    /// <summary>
    /// Explosion force magnitude.
    /// </summary>
    public float Strength { get; set; } = 800f;

    /// <summary>
    /// Maximum effective radius.
    /// </summary>
    public float Radius { get; set; } = 300f;

    /// <summary>
    /// If true, the explosion only applies once per particle.
    /// </summary>
    public bool OneShot { get; set; } = true;

    private bool hasFired = false;

    /// <summary>
    /// Applies the explosion force outward from the origin.
    /// </summary>
    /// <param name="p">The particle to modify.</param>
    /// <param name="delta">Time delta in seconds.</param>
    public void Apply( Particle p, float delta )
    {
        if ( !p.IsAlive || ( OneShot && hasFired ) ) return;

        Vector2 dir = p.Position - Origin;
        float dist = dir.Length();
        if ( dist > Radius || dist < 1e-4f ) return;

        dir /= dist;
        float force = Strength * (1f - dist / Radius);
        p.Velocity += dir * force * delta;

        if ( OneShot ) hasFired = true;
    }
}