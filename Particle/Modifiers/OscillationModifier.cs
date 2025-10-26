using System;
using System.Collections.Generic;
using System.Text;

namespace SFML.Game.Framework.Particle.Modifiers;
/// <summary>
/// Applies a continuous oscillation to a particle's position or velocity.
/// Can be used for pulsing light or breathing-type movement.
/// </summary>
public class OscillationModifier : IParticleModifier
{
    /// <summary>
    /// Axis of oscillation. (1,0) for horizontal, (0,1) for vertical.
    /// </summary>
    public Vector2 Axis { get; set; } = new Vector2( 0, 1 );

    /// <summary>
    /// Amplitude of the oscillation in pixels.
    /// </summary>
    public float Amplitude { get; set; } = 20f;

    /// <summary>
    /// Frequency of the oscillation in Hz.
    /// </summary>
    public float Frequency { get; set; } = 2f;

    /// <summary>
    /// Whether to modify position (true) or velocity (false).
    /// </summary>
    public bool AffectPosition { get; set; } = true;

    /// <summary>
    /// Applies the oscillation effect to a particle.
    /// </summary>
    /// <param name="p">The particle to modify.</param>
    /// <param name="delta">Time step in seconds.</param>
    public void Apply( Particle p, float delta )
    {
        if ( !p.IsAlive ) return;

        float age = p.TotalLifetime - p.Lifetime;
        float oscillation = MathF.Sin(age * Frequency * MathF.Tau) * Amplitude * delta;

        if ( AffectPosition )
            p.Position += Axis * oscillation;
        else
            p.Velocity += Axis * oscillation;
    }
}