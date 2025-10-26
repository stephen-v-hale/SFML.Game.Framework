using System;
using System.Collections.Generic;
using System.Text;

namespace SFML.Game.Framework.Particle.Modifiers;

public class AngularVelocityOverLifeModifier : IParticleModifier
{
    public float StartAngular;
    public float EndAngular;

    public AngularVelocityOverLifeModifier( float start, float end )
    {
        StartAngular = start;
        EndAngular = end;
    }

    public void Apply( Particle p, float delta )
    {
        if ( !p.IsAlive ) return;
        float t = 1f - (p.Lifetime / p.TotalLifetime);
        p.AngularVelocity = StartAngular + ( EndAngular - StartAngular ) * t;
    }
}