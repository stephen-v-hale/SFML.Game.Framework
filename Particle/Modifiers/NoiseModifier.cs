using System;
using System.Collections.Generic;
using System.Text;

namespace SFML.Game.Framework.Particle.Modifiers;

/// <summary>
/// Adds smooth turbulence to particle velocity.
/// Ideal for smoke, fire, or magical particles.
/// </summary>
public class NoiseModifier : IParticleModifier
{
    private readonly float strength;
    private readonly float frequency;
    private readonly float timeScale;
    private float elapsed = 0f;
    private readonly SimplexNoise noise;

    public NoiseModifier( float strength = 30f, float frequency = 0.5f, float timeScale = 1f )
    {
        this.strength = strength;
        this.frequency = frequency;
        this.timeScale = timeScale;
        noise = new SimplexNoise();
    }

    public void Apply( Particle p, float delta )
    {
        if ( !p.IsAlive ) return;

        elapsed += delta * timeScale;
        float nx = noise.Noise(p.Position.X * frequency, elapsed);
        float ny = noise.Noise(p.Position.Y * frequency, elapsed + 100f);

        var force = new Vector2(nx, ny) * strength * delta;
        p.Velocity += force;
    }
}
