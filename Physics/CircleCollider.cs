using System;
using System.Collections.Generic;
using System.Text;
#nullable disable
namespace SFML.Game.Framework.Physics;

/// <summary>
/// Represents a circular collider for 2D collision detection.
/// </summary>
public class CircleCollider : Collider2D
{
    /// <summary>
    /// Gets the radius of the circle.
    /// </summary>
    public float Radius { get; }

    /// <summary>
    /// Initializes a new instance of <see cref="CircleCollider"/>.
    /// </summary>
    /// <param name="radius">The radius of the circle.</param>
    public CircleCollider( float radius )
    {
        Radius = radius;
    }

    /// <inheritdoc/>
    public override bool CheckCollision( Collider2D other, out CollisionManifold manifold )
    {
        manifold = null;

        if ( other is CircleCollider circle )
        {
            Vector2 diff = other.Position - Position;
            float dist = diff.Length();
            float penetration = Radius + circle.Radius - dist;

            if ( penetration > 0 )
            {
                manifold = new CollisionManifold( diff.Normalized(), penetration );
                return true;
            }
        }

        return false;
    }
}
