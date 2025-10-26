using System;
using System.Collections.Generic;
using System.Text;

namespace SFML.Game.Framework.Particle.Modifiers;

/// <summary>
/// Applies a directional flow field based on 2D noise.
/// Produces organic swirling motion, similar to fluid simulation.
/// </summary>
public class TurbulentFieldModifier : IParticleModifier
{
    private readonly SimplexNoise noise = new();
    private float time = 0f;

    /// <summary>
    /// Strength of the turbulence effect.
    /// </summary>
    public float Strength { get; set; } = 50f;

    /// <summary>
    /// Frequency of the noise field.
    /// </summary>
    public float Frequency { get; set; } = 0.05f;

    /// <summary>
    /// Rate of temporal noise evolution.
    /// </summary>
    public float TimeScale { get; set; } = 0.5f;

    /// <summary>
    /// Applies noise-based directional motion to the particle.
    /// </summary>
    /// <param name="p">The particle to modify.</param>
    /// <param name="delta">Time delta in seconds.</param>
    public void Apply( Particle p, float delta )
    {
        if ( !p.IsAlive ) return;

        time += delta * TimeScale;
        float nx = noise.Noise(p.Position.X * Frequency, p.Position.Y * Frequency);
        float ny = noise.Noise(p.Position.Y * Frequency + 100f, p.Position.X * Frequency);

        Vector2 dir = new(nx, ny);
        p.Velocity += dir * Strength * delta;
    }
}