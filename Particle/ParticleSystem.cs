using System;
using System.Collections.Generic;
using System.Text;

using SFML.Game.Framework.Content;
using SFML.Game.Framework.Graphics;

namespace SFML.Game.Framework.Particle;

/// <summary>
/// 2D particle system for spawning, updating, and rendering particles.
/// </summary>
public class ParticleSystem
{
    private readonly List<Particle> particles = new();
    private readonly GraphicsDrawer drawer;
    private readonly Random random = new();

    public Vector2 Position;
    public int MaxParticles = 1000;
    public float SpawnRate = 50f;          // particles per second
    public List<Texture2D> Textures;       // particle textures
    public Vector2 VelocityMin = new Vector2(-50, -50);
    public Vector2 VelocityMax = new Vector2(50, 50);
    public float LifetimeMin = 1f;
    public float LifetimeMax = 2f;
    public float SizeMin = 16f;
    public float SizeMax = 32f;

    private float spawnAccumulator = 0f;

    public ParticleSystem( GraphicsDrawer drawer, Vector2 position, List<Texture2D> textures )
    {
        this.drawer = drawer;
        this.Position = position;
        this.Textures = textures;
    }

    public void Update( float delta )
    {
        spawnAccumulator += SpawnRate * delta;

        while ( spawnAccumulator >= 1f && particles.Count < MaxParticles )
        {
            SpawnParticle();
            spawnAccumulator -= 1f;
        }

        for ( int i = particles.Count - 1; i >= 0; i-- )
        {
            particles[i].Update( delta );
            if ( !particles[i].IsAlive )
                particles.RemoveAt( i );
        }
    }

    private void SpawnParticle()
    {
        Vector2 velocity = new Vector2(
            VelocityMin.X + (float)random.NextDouble() * (VelocityMax.X - VelocityMin.X),
            VelocityMin.Y + (float)random.NextDouble() * (VelocityMax.Y - VelocityMin.Y)
        );

        float lifetime = LifetimeMin + (float)random.NextDouble() * (LifetimeMax - LifetimeMin);
        float size = SizeMin + (float)random.NextDouble() * (SizeMax - SizeMin);

        // Pick a random texture
        Texture2D texture = Textures[random.Next(Textures.Count)];

        particles.Add( new Particle( Position, velocity, lifetime, Color.White, texture, size ) );
    }

    public void Draw()
    {
        foreach ( var p in particles )
        {
            drawer.DrawTexture( p.Texture, new Rectangle( 0, 0, p.Texture.Size.Width, p.Texture.Size.Height ),
                               p.Position, p.Color, p.Rotation, new Vector2( p.Size / p.Texture.Size.Width, p.Size / p.Texture.Size.Height ) );
        }
    }
}
