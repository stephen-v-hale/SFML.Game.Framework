using System;
using System.Collections.Generic;
using System.Text;

namespace SFML.Game.Framework.Particle.Modifiers;

/// <summary>
/// Applies a constant or gusty wind force to all particles.
/// </summary>
public class WindModifier : IParticleModifier
{
    public Vector2 BaseWind = new Vector2(50, 0);
    public float GustStrength = 0f;
    public float GustFrequency = 0.5f;

    private float time = 0f;
    private readonly Random random = new Random();

    public WindModifier( Vector2 baseWind, float gustStrength = 0f, float gustFrequency = 0.5f )
    {
        BaseWind = baseWind;
        GustStrength = gustStrength;
        GustFrequency = gustFrequency;
    }

    public void Apply( Particle p, float delta )
    {
        if ( !p.IsAlive ) return;

        time += delta;
        float gust = (float)Math.Sin(time * GustFrequency * 2f * MathF.PI) * GustStrength;
        var wind = BaseWind + new Vector2(gust, 0);
        p.Velocity += wind * delta;
    }
}