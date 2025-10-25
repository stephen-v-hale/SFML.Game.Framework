using System;
using System.Collections.Generic;
using System.Text;

namespace SFML.Game.Framework.Physics;

#nullable disable

/// <summary>
/// Represents a physical object that can move, collide, and be affected by forces.
/// </summary>
public class Rigidbody2D
{
    /// <summary>
    /// Gets or sets the position of the body.
    /// </summary>
    public Vector2 Position;

    /// <summary>
    /// Gets or sets the current velocity.
    /// </summary>
    public Vector2 Velocity;

    /// <summary>
    /// Gets or sets the current acceleration.
    /// </summary>
    public Vector2 Acceleration;

    /// <summary>
    /// Gets the mass of the body.
    /// </summary>
    public float Mass { get; private set; }

    /// <summary>
    /// Gets or sets whether the body is static (immovable).
    /// </summary>
    public bool IsStatic { get; set; }

    /// <summary>
    /// Gets or sets the collider associated with this body.
    /// </summary>
    public Collider2D Collider { get; set; }

    /// <summary>
    /// Initializes a new instance of <see cref="Rigidbody2D"/>.
    /// </summary>
    /// <param name="mass">Mass of the body (default is 1).</param>
    public Rigidbody2D( float mass = 1f )
    {
        Mass = mass <= 0 ? 1f : mass;
    }

    /// <summary>
    /// Applies a continuous force to this body.
    /// </summary>
    /// <param name="force">The force vector.</param>
    public void ApplyForce( Vector2 force )
    {
        if ( IsStatic ) return;
        Acceleration += force / Mass;
    }

    /// <summary>
    /// Integrates the velocity and position over time.
    /// </summary>
    /// <param name="deltaTime">Time step (in seconds).</param>
    public void Integrate( float deltaTime )
    {
        if ( IsStatic ) return;

        Velocity += Acceleration * deltaTime;
        Position += Velocity * deltaTime;
        Acceleration = Vector2.Zero;

        if ( Collider != null )
            Collider.Position = Position;
    }
}