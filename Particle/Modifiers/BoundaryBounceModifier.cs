using System;
using System.Collections.Generic;
using System.Text;

namespace SFML.Game.Framework.Particle.Modifiers;

/// <summary>
/// Makes particles bounce off rectangular boundaries with optional energy loss.
/// </summary>
public class BoundaryBounceModifier : IParticleModifier
{
    /// <summary>
    /// Defines the rectangular boundary area.
    /// </summary>
    public Rectangle Bounds { get; set; }

    /// <summary>
    /// Multiplier for velocity after a bounce (1 = perfect reflection, 0.8 = energy loss).
    /// </summary>
    public float BounceFactor { get; set; } = 0.8f;

    /// <summary>
    /// Applies the boundary collision and bounce behavior.
    /// </summary>
    /// <param name="p">The particle to modify.</param>
    /// <param name="delta">Delta time in seconds.</param>
    public void Apply( Particle p, float delta )
    {
        if ( !p.IsAlive ) return;

        if ( p.Position.X - p.Size < Bounds.Left )
        {
            p.Position.X = Bounds.Left + p.Size;
            p.Velocity.X = -p.Velocity.X * BounceFactor;
        }
        else if ( p.Position.X + p.Size > Bounds.Right )
        {
            p.Position.X = Bounds.Right - p.Size;
            p.Velocity.X = -p.Velocity.X * BounceFactor;
        }

        if ( p.Position.Y - p.Size < Bounds.Top )
        {
            p.Position.Y = Bounds.Top + p.Size;
            p.Velocity.Y = -p.Velocity.Y * BounceFactor;
        }
        else if ( p.Position.Y + p.Size > Bounds.Bottom )
        {
            p.Position.Y = Bounds.Bottom - p.Size;
            p.Velocity.Y = -p.Velocity.Y * BounceFactor;
        }
    }
}