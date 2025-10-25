using System;
using System.Collections.Generic;
using System.Text;

#nullable disable
namespace SFML.Game.Framework.Physics;

/// <summary>
/// Contains information about a collision event between two bodies.
/// </summary>
public class CollisionManifold
{
    /// <summary>
    /// Gets the collision normal direction.
    /// </summary>
    public Vector2 Normal { get; }

    /// <summary>
    /// Gets the penetration depth of the collision.
    /// </summary>
    public float Penetration { get; }

    /// <summary>
    /// Initializes a new instance of <see cref="CollisionManifold"/>.
    /// </summary>
    /// <param name="normal">The normal vector of the collision.</param>
    /// <param name="penetration">The penetration depth.</param>
    public CollisionManifold( Vector2 normal, float penetration )
    {
        Normal = normal;
        Penetration = penetration;
    }

    /// <summary>
    /// Checks for collision between two rigidbodies.
    /// </summary>
    public static CollisionManifold CheckCollision( Rigidbody2D A, Rigidbody2D B )
    {
        if ( A.Collider.CheckCollision( B.Collider, out CollisionManifold manifold ) )
            return manifold;
        return null;
    }

    /// <summary>
    /// Resolves a detected collision between two bodies.
    /// </summary>
    public static void ResolveCollision( Rigidbody2D A, Rigidbody2D B, CollisionManifold m )
    {
        if ( A.IsStatic && B.IsStatic ) return;

        float totalMass = A.Mass + B.Mass;
        Vector2 correction = m.Normal * (m.Penetration / totalMass) * 1.01f;

        if ( !A.IsStatic )
            A.Position -= correction * B.Mass;

        if ( !B.IsStatic )
            B.Position += correction * A.Mass;

        Vector2 relativeVelocity = B.Velocity - A.Velocity;
        float velAlongNormal = Vector2.Dot(relativeVelocity, m.Normal);

        if ( velAlongNormal > 0 ) return;

        float restitution = 0.5f;
        float impulseMag = -(1 + restitution) * velAlongNormal / (1 / A.Mass + 1 / B.Mass);
        Vector2 impulse = m.Normal * impulseMag;

        if ( !A.IsStatic ) A.Velocity -= impulse / A.Mass;
        if ( !B.IsStatic ) B.Velocity += impulse / B.Mass;
    }
}