using System;
using System.Collections.Generic;
using System.Text;

namespace SFML.Game.Framework.Particle.Modifiers;

/// <summary>
/// Pulls particles toward one or more attractor points.
/// </summary>
public class AttractorModifier : IParticleModifier
{
    public List<Vector2> Points { get; } = new();
    public float Strength = 100f;
    public float Radius = 300f;

    public void Apply( Particle p, float delta )
    {
        if ( !p.IsAlive ) return;

        foreach ( var point in Points )
        {
            Vector2 dir = point - p.Position;
            float distSq = dir.LengthSquared();
            if ( distSq < 1e-4f || distSq > Radius * Radius ) continue;

            float dist = MathF.Sqrt(distSq);
            dir /= dist;
            float force = Strength * (1f - dist / Radius);
            p.Velocity += dir * force * delta;
        }
    }
}