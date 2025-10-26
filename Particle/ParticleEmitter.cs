using System;
using System.Collections.Generic;
using System.Text;

using SFML.Game.Framework.Content;
using SFML.Game.Framework.Graphics;

namespace SFML.Game.Framework.Particle;
#nullable disable
/// <summary>
/// Configurable particle emitter with pooling and modifiers.
/// </summary>
public class ParticleEmitter
{
    private readonly ParticlePool pool;
    private readonly GraphicsDrawer drawer;
    private readonly Random random = new Random();

    private readonly List<int> activeIndices = new();
    private readonly List<IParticleModifier> modifiers = new();

    // Public config
    public Vector2 Position;
    public int MaxParticles { get; set; } = 1000;
    public float SpawnRate = 50f; // per second
    public List<Texture2D> Textures = new();
    public Vector2 VelocityMin = new Vector2(-50, -50);
    public Vector2 VelocityMax = new Vector2(50, 50);
    public float LifetimeMin = 1f;
    public float LifetimeMax = 2f;
    public float SizeMin = 8f;
    public float SizeMax = 24f;
    public float EndSizeMin = 0f;
    public float EndSizeMax = 0f;
    public Color StartColor = Color.White;
    public SpawnShape SpawnShape = SpawnShape.Point;
    public float SpawnRadius = 0f; // for circle shape
    public Vector2 SpawnRect = new Vector2(0,0); // for rectangle shape
    public bool Enabled = true;

    private float spawnAccumulator = 0f;
    private readonly object sync = new object();

    public ParticleEmitter( GraphicsDrawer drawer, Vector2 position, int capacity = 1000 )
    {
        this.drawer = drawer;
        this.Position = position;
        MaxParticles = capacity;
        pool = new ParticlePool( capacity );
    }

    public void AddModifier( IParticleModifier m ) => modifiers.Add( m );
    public void RemoveModifier( IParticleModifier m ) => modifiers.Remove( m );

    /// <summary>
    /// Spawn a one-shot burst of N particles.
    /// </summary>
    public void Burst( int count )
    {
        for ( int i = 0; i < count; i++ ) 
            TrySpawnOne();
    }

    public void Update( float delta )
    {
        if ( !Enabled ) return;

        // spawn by rate
        spawnAccumulator += SpawnRate * delta;
        int spawnCount = (int)spawnAccumulator;
        spawnAccumulator -= spawnCount;

        for ( int i = 0; i < spawnCount; i++ )
            TrySpawnOne();

        // update active particles (iterate backward to allow remove)
        for ( int i = activeIndices.Count - 1; i >= 0; i-- )
        {
            int idx = activeIndices[i];
            var p = pool[idx];
            if ( !p.IsAlive )
            {
                pool.Release( idx );
                activeIndices.RemoveAt( i );
                continue;
            }

            // basic built-in update (movement, fade, size linear)
            p.BasicUpdate( delta );

            // apply modifiers
            for ( int m = 0; m < modifiers.Count; m++ )
            {
                modifiers[m].Apply( p, delta );
            }

            // safety: if lifetime became dead inside modifiers
            if ( !p.IsAlive )
            {
                pool.Release( idx );
                activeIndices.RemoveAt( i );
            }
        }
    }

    private void TrySpawnOne()
    {
        if ( activeIndices.Count >= MaxParticles ) return;

        if ( !pool.TryAcquire( out Particle p, out int idx ) )
            return;

        // compute spawn position depending on shape
        var spawnPos = Position;
        switch ( SpawnShape )
        {
            case SpawnShape.Point:
            spawnPos = Position;
            break;
            case SpawnShape.Circle:
            {
                // random point in circle radius
                float r = (float)Math.Sqrt(random.NextDouble()) * SpawnRadius;
                float a = (float)random.NextDouble() * MathF.PI * 2f;
                spawnPos = Position + new Vector2( MathF.Cos( a ) * r, MathF.Sin( a ) * r );
            }
            break;
            case SpawnShape.Rectangle:
            {
                float rx = (float)(random.NextDouble() * SpawnRect.X) - SpawnRect.X * 0.5f;
                float ry = (float)(random.NextDouble() * SpawnRect.Y) - SpawnRect.Y * 0.5f;
                spawnPos = Position + new Vector2( rx, ry );
            }
            break;
        }

        // velocity
        var vel = new Vector2(
                VelocityMin.X + (float)random.NextDouble() * (VelocityMax.X - VelocityMin.X),
                VelocityMin.Y + (float)random.NextDouble() * (VelocityMax.Y - VelocityMin.Y)
            );

        float lifetime = LifetimeMin + (float)random.NextDouble() * (LifetimeMax - LifetimeMin);
        float startSize = SizeMin + (float)random.NextDouble() * (SizeMax - SizeMin);
        float endSize = EndSizeMin + (float)random.NextDouble() * (EndSizeMax - EndSizeMin);

        // pick texture safely
        Texture2D tex = Textures.Count > 0 ? Textures[random.Next(Textures.Count)] : null;

        // rotation and angular velocity
        float rotation = (float)random.NextDouble() * 360f;
        float angVel = (float)(random.NextDouble() * 180f - 90f); // -90..90 deg/s

        p.Reset( spawnPos, vel, lifetime, StartColor, tex, startSize, endSize, rotation, angVel );
        activeIndices.Add( idx );
    }

    public void Draw()
    {
        // draw each active particle
        for ( int i = 0; i < activeIndices.Count; i++ )
        {
            int idx = activeIndices[i];
            var p = pool[idx];
            if ( !p.IsAlive || p.Texture == null ) continue;

            // compute source rect
            var src = new Rectangle(0, 0, p.Texture.Size.Width, p.Texture.Size.Height);
            // scale so sprite's width equals p.Size (assumes texture width is horizontal dimension)
            var scale = new Vector2(p.Size / p.Texture.Size.Width, p.Size / p.Texture.Size.Height);

            drawer.DrawTexture( p.Texture, src, p.Position, p.Color, p.Rotation, scale );
        }
    }

    /// <summary>
    /// Removes all particles immediately.
    /// </summary>
    public void Clear()
    {
        for ( int i = 0; i < activeIndices.Count; i++ )
        {
            pool.Release( activeIndices[i] );
        }
        activeIndices.Clear();
        spawnAccumulator = 0f;
    }
}