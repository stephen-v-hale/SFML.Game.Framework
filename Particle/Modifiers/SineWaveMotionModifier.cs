using System;
using System.Collections.Generic;
using System.Text;

namespace SFML.Game.Framework.Particle.Modifiers;

/// <summary>
/// Makes particles move along a sine wave pattern.
/// Useful for ethereal, underwater, or hovering effects.
/// </summary>
public class SineWaveMotionModifier : IParticleModifier
{
    /// <summary>
    /// Frequency of the wave oscillation.
    /// </summary>
    public float Frequency { get; set; } = 3f;

    /// <summary>
    /// Amplitude of the oscillation in pixels.
    /// </summary>
    public float Amplitude { get; set; } = 10f;

    /// <summary>
    /// Phase offset to randomize waves between particles.
    /// </summary>
    public float PhaseOffset { get; set; } = 0f;

    /// <summary>
    /// Axis of the oscillation (e.g., (0,1) for vertical, (1,0) for horizontal).
    /// </summary>
    public Vector2 Axis { get; set; } = new Vector2( 0, 1 );

    /// <summary>
    /// Applies the sine wave offset to the particle's position.
    /// </summary>
    /// <param name="p">The particle to modify.</param>
    /// <param name="delta">Time delta in seconds.</param>
    public void Apply( Particle p, float delta )
    {
        if ( !p.IsAlive ) return;

        float age = p.TotalLifetime - p.Lifetime;
        float wave = MathF.Sin(age * Frequency + PhaseOffset) * Amplitude;
        p.Position += Axis * wave * delta;
    }
}