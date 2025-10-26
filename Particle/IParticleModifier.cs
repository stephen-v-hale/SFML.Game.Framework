using System;
using System.Collections.Generic;
using System.Text;

namespace SFML.Game.Framework.Particle;

/// <summary>
/// Small modifier interface that manipulates a particle each update.
/// </summary>
public interface IParticleModifier
{
    /// <summary>
    /// Called each update for the particle. delta = seconds.
    /// </summary>
    void Apply( Particle p, float delta );
}