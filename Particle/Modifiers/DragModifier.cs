using System;
using System.Collections.Generic;
using System.Text;

namespace SFML.Game.Framework.Particle.Modifiers;

/// <summary>
/// Simulates air drag, slowing down particles proportionally to their velocity.
/// </summary>
public class DragModifier : IParticleModifier
{
    /// <summary>
    /// Drag coefficient (0 = no drag, 1 = instant stop).
    /// </summary>
    public float Coefficient { get; set; } = 0.1f;

    /// <summary>
    /// Applies the drag effect to the given particle.
    /// </summary>
    /// <param name="p">The particle to modify.</param>
    /// <param name="delta">Delta time in seconds.</param>
    public void Apply( Particle p, float delta )
    {
        if ( !p.IsAlive ) return;
        p.Velocity -= p.Velocity * Coefficient * delta;
    }
}