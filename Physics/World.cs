using System;
using System.Collections.Generic;
using System.Text;

namespace SFML.Game.Framework.Physics;

/// <summary>
/// Represents the 2D physics simulation world.
/// Responsible for applying gravity, integrating motion, and resolving collisions.
/// </summary>
public class PhysicsWorld
{
    /// <summary>
    /// Gets or sets the global gravity vector (in pixels per second squared).
    /// </summary>
    public Vector2 Gravity { get; set; } = new Vector2( 0, 980f );

    /// <summary>
    /// Gets all physics bodies currently in the world.
    /// </summary>
    public List<Rigidbody2D> Bodies { get; } = new();

    /// <summary>
    /// Adds a body to the physics world.
    /// </summary>
    /// <param name="body">The rigidbody to add.</param>
    public void AddBody( Rigidbody2D body )
    {
        if ( !Bodies.Contains( body ) )
            Bodies.Add( body );
    }

    /// <summary>
    /// Removes a body from the physics world.
    /// </summary>
    /// <param name="body">The rigidbody to remove.</param>
    public void RemoveBody( Rigidbody2D body )
    {
        Bodies.Remove( body );
    }

    /// <summary>
    /// Updates all physics bodies, applies gravity, and resolves collisions.
    /// </summary>
    /// <param name="deltaTime">Time elapsed since the last frame (in seconds).</param>
    public void Update( float deltaTime )
    {
        // Apply forces
        foreach ( var body in Bodies )
        {
            if ( !body.IsStatic )
                body.ApplyForce( Gravity * body.Mass );
            body.Integrate( deltaTime );
        }

        // Collision detection and resolution
        for ( int i = 0; i < Bodies.Count; i++ )
        {
            for ( int j = i + 1; j < Bodies.Count; j++ )
            {
                var A = Bodies[i];
                var B = Bodies[j];

                if ( A.Collider == null || B.Collider == null )
                    continue;

                var manifold = CollisionManifold.CheckCollision(A, B);
                if ( manifold != null )
                    CollisionManifold.ResolveCollision( A, B, manifold );
            }
        }
    }
}