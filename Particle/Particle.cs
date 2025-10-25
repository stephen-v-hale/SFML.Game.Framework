using System;
using System.Collections.Generic;
using System.Text;

using SFML.Game.Framework.Content;

namespace SFML.Game.Framework.Particle;
/// <summary>
/// Represents a single textured particle.
/// </summary>
public class Particle
{
    public Vector2 Position;
    public Vector2 Velocity;
    public float Lifetime;        // Remaining life in seconds
    public float TotalLifetime;   // Total life for fading/scaling
    public Color Color;
    public float Size;
    public float Rotation;
    public float AngularVelocity;
    public Texture2D Texture;     // Particle texture

    public Particle( Vector2 position, Vector2 velocity, float lifetime, Color color, Texture2D texture, float size = 1f, float rotation = 0f, float angularVelocity = 0f )
    {
        Position = position;
        Velocity = velocity;
        Lifetime = lifetime;
        TotalLifetime = lifetime;
        Color = color;
        Size = size;
        Rotation = rotation;
        AngularVelocity = angularVelocity;
        Texture = texture;
    }

    public void Update( float delta )
    {
        Lifetime -= delta;
        Position += Velocity * delta;
        Rotation += AngularVelocity * delta;

        float lifeRatio = MathF.Max(Lifetime / TotalLifetime, 0f);
        Color.A = ( int )( 255 * lifeRatio ); // Fade out
    }

    public bool IsAlive => Lifetime > 0f;
}
