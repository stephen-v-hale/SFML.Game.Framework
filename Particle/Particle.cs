using System;
using System.Numerics;
using System.Runtime.CompilerServices;
#nullable disable
using SFML.Game.Framework.Content;

namespace SFML.Game.Framework.Particle;

/// <summary>
/// Represents a single textured particle.
/// Plain data holder so pooling is easy.
/// </summary>
public class Particle
{
    public Vector2 Position;
    public Vector2 Velocity;
    public float Lifetime;        // Remaining life in seconds
    public float TotalLifetime;   // Total life for fading/scaling
    public Color Color;
    public float Size;            // Current size in pixels
    public float StartSize;       // initial size
    public float EndSize;         // size at death
    public float Rotation;
    public float AngularVelocity;
    public Texture2D Texture;     // Particle texture
    public bool Active;

    public void Reset( in Vector2 position, in Vector2 velocity, float lifetime, Color color, Texture2D texture, float startSize, float endSize, float rotation = 0f, float angularVelocity = 0f )
    {
        Position = position;
        Velocity = velocity;
        Lifetime = lifetime;
        TotalLifetime = lifetime;
        Color = color;
        Texture = texture;
        StartSize = startSize;
        EndSize = endSize;
        Size = startSize;
        Rotation = rotation;
        AngularVelocity = angularVelocity;
        Active = true;
    }


    [MethodImpl( MethodImplOptions.AggressiveInlining )]
    public void Kill()
    {
        Active = false;
        Lifetime = 0f;
    }

    public bool IsAlive => Active && Lifetime > 0f;

    /// <summary>
    /// Progress built-in fade & size interpolation. Additional modifiers can further modify fields.
    /// </summary>
    public void BasicUpdate( float delta )
    {
        Lifetime -= delta;
        if ( Lifetime <= 0f )
        {
            Active = false;
            return;
        }

        var lifeT = 1f - (Lifetime / TotalLifetime); // 0..1
                                                     // size interpolation (linear)
        Size = StartSize + ( EndSize - StartSize ) * lifeT;

        // fade color alpha by remaining lifetime (keeps original rgb)
        float alphaRatio = MathF.Max(Lifetime / TotalLifetime, 0f);
        Color.A = ( int )( 255 * alphaRatio );

        Position += Velocity * delta;
        Rotation += AngularVelocity * delta;
    }
}