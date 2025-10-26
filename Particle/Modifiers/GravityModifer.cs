using System;
using System.Collections.Generic;
using System.Text;

namespace SFML.Game.Framework.Particle.Modifiers;

public class GravityModifier : IParticleModifier
{
    public Vector2 Gravity;

    public GravityModifier( Vector2 gravity )
    {
        Gravity = gravity;
    }

    public void Apply( Particle p, float delta )
    {
        p.Velocity += Gravity * delta;
    }
}