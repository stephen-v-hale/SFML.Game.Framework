using System;
using System.Collections.Generic;
using System.Text;

namespace SFML.Game.Framework.Particle.Modifiers;


public class VelocityDampingModifier : IParticleModifier
{
    public float Factor; // per second damping: e.g. 0.9f reduces quickly

    public VelocityDampingModifier( float factor )
    {
        Factor = factor;
    }

    public void Apply( Particle p, float delta )
    {
        // simple exponential damping
        float damp = MathF.Pow(Factor, delta);
        p.Velocity *= damp;
    }
}
