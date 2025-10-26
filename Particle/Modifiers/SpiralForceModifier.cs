using System;
using System.Collections.Generic;
using System.Text;

namespace SFML.Game.Framework.Particle.Modifiers;

/// <summary>
/// Pushes particles outward in a spiral pattern.
/// Excellent for explosions, portals, or energy discharges.
/// </summary>
public class SpiralForceModifier : IParticleModifier
{
    /// <summary>
    /// Center of the spiral.
    /// </summary>
    public Vector2 Origin { get; set; }

    /// <summary>
    /// Outward radial force magnitude.
    /// </summary>
    public float RadialStrength { get; set; } = 200f;

    /// <summary>
    /// Rotational (tangential) force magnitude.
    /// </summary>
    public float TangentialStrength { get; set; } = 100f;

    /// <summary>
    /// Maximum range of influence.
    /// </summary>
    public float Radius { get; set; } = 400f;

    /// <summary>
    /// Applies a spiral motion force to the particle.
    /// </summary>
    /// <param name="p">The particle to modify.</param>
    /// <param name="delta">Time step in seconds.</param>
    public void Apply( Particle p, float delta )
    {
        if ( !p.IsAlive ) return;

        Vector2 offset = p.Position - Origin;
        float dist = offset.Length();
        if ( dist < 1e-4f || dist > Radius ) return;

        offset /= dist;
        Vector2 tangent = new(-offset.Y, offset.X);

        float falloff = 1f - dist / Radius;

        p.Velocity += ( offset * RadialStrength + tangent * TangentialStrength ) * falloff * delta;
    }
}