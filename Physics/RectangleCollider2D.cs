using System;
using System.Collections.Generic;
using System.Text;
#nullable disable
namespace SFML.Game.Framework.Physics;

/// <summary>
/// Represents a rectangular collider for 2D collision detection.
/// </summary>
public class RectangleCollider : Collider2D
{
    /// <summary>
    /// Gets the width of the rectangle.
    /// </summary>
    public float Width { get; }

    /// <summary>
    /// Gets the height of the rectangle.
    /// </summary>
    public float Height { get; }

    /// <summary>
    /// Initializes a new instance of <see cref="RectangleCollider"/>.
    /// </summary>
    /// <param name="width">The width of the rectangle.</param>
    /// <param name="height">The height of the rectangle.</param>
    public RectangleCollider( float width, float height )
    {
        Width = width;
        Height = height;
    }

    /// <inheritdoc/>
    public override bool CheckCollision( Collider2D other, out CollisionManifold manifold )
    {
        manifold = null;

        if ( other is RectangleCollider rect )
        {
            float dx = (Position.X + Width / 2) - (rect.Position.X + rect.Width / 2);
            float dy = (Position.Y + Height / 2) - (rect.Position.Y + rect.Height / 2);
            float overlapX = (Width / 2 + rect.Width / 2) - Math.Abs(dx);
            float overlapY = (Height / 2 + rect.Height / 2) - Math.Abs(dy);

            if ( overlapX > 0 && overlapY > 0 )
            {
                manifold = overlapX < overlapY
                    ? new CollisionManifold( new Vector2( Math.Sign( dx ), 0 ), overlapX )
                    : new CollisionManifold( new Vector2( 0, Math.Sign( dy ) ), overlapY );

                return true;
            }
        }

        return false;
    }
}