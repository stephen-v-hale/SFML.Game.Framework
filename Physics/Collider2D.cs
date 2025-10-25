using System;
using System.Collections.Generic;
using System.Text;

namespace SFML.Game.Framework.Physics;

/// <summary>
/// Represents a 2D collision shape used by <see cref="Rigidbody2D"/>.
/// </summary>
public abstract class Collider2D
{
    /// <summary>
    /// Gets or sets the position of the collider.
    /// </summary>
    public Vector2 Position { get; set; }

    /// <summary>
    /// Checks for collision with another collider.
    /// </summary>
    /// <param name="other">The other collider to test against.</param>
    /// <param name="manifold">The resulting collision manifold.</param>
    /// <returns>True if the colliders overlap, otherwise false.</returns>
    public abstract bool CheckCollision( Collider2D other, out CollisionManifold manifold );
}