using System;
using System.Collections.Generic;
using System.Text;

namespace SFML.Game.Framework.Particle.Modifiers;
/// <summary>
/// Causes particles to orbit around a specified center point.
/// Useful for satellites, magical rings, or energy fields.
/// </summary>
public class OrbitModifier : IParticleModifier
{
    /// <summary>
    /// The center of the orbit.
    /// </summary>
    public Vector2 Center { get; set; }

    /// <summary>
    /// The angular velocity in degrees per second.
    /// </summary>
    public float AngularVelocity { get; set; } = 90f;

    /// <summary>
    /// The radius correction factor to pull particles toward or away from the center.
    /// </summary>
    public float RadiusCorrection { get; set; } = 0f;

    /// <summary>
    /// The maximum range where orbiting is applied.
    /// </summary>
    public float MaxDistance { get; set; } = 500f;

    /// <summary>
    /// Applies the orbit motion to a particle.
    /// </summary>
    /// <param name="p">The particle to modify.</param>
    /// <param name="delta">Time step in seconds.</param>
    public void Apply( Particle p, float delta )
    {
        if ( !p.IsAlive ) return;

        Vector2 offset = p.Position - Center;
        float dist = offset.Length();
        if ( dist < 1e-4f || dist > MaxDistance ) return;

        // Normalize offset
        offset /= dist;

        // Tangential velocity (perpendicular)
        Vector2 tangent = new(-offset.Y, offset.X);

        // Orbit motion
        float radians = MathF.PI / 180f * AngularVelocity;
        p.Velocity += tangent * radians * dist * delta;

        // Optional radial pull/push
        if ( MathF.Abs( RadiusCorrection ) > 0.01f )
            p.Velocity -= offset * RadiusCorrection * delta;
    }
}