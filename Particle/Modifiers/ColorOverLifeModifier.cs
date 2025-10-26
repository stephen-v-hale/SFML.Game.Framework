using System;
using System.Collections.Generic;
using System.Text;

namespace SFML.Game.Framework.Particle.Modifiers;

public class ColorOverLifeModifier : IParticleModifier
{
    public Color StartColor;
    public Color EndColor;

    public ColorOverLifeModifier( Color start, Color end )
    {
        StartColor = start;
        EndColor = end;
    }

    public void Apply( Particle p, float delta )
    {
        if ( !p.IsAlive ) return;
        float t = 1f - (p.Lifetime / p.TotalLifetime);
        p.Color.R = ( int )( StartColor.R + ( EndColor.R - StartColor.R ) * t );
        p.Color.G = ( int )( StartColor.G + ( EndColor.G - StartColor.G ) * t );
        p.Color.B = ( int )( StartColor.B + ( EndColor.B - StartColor.B ) * t );
    }
}