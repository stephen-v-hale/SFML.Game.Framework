using System;
using System.Collections.Generic;
using System.Text;

using SFML.Game.Framework.Graphics;

namespace SFML.Game.Framework.Animation;

#nullable disable
public class Animator
{
    private Animation currentAnimation;
    private int currentFrameIndex = 0;
    private float frameTimer = 0f;

    public Vector2 Position { get; set; } = new Vector2( 0, 0 );
    public Vector2 Scale { get; set; } = new Vector2( 1, 1 );
    public float Rotation { get; set; } = 0f;
    public Color Color { get; set; } = Color.White;

    public Animation CurrentAnimation => currentAnimation;

    public void Play( Animation animation )
    {
        if ( currentAnimation == animation )
            return;

        currentAnimation = animation;
        currentFrameIndex = 0;
        frameTimer = 0f;
    }

    public void Update( float delta )
    {
        if ( currentAnimation == null || currentAnimation.Frames.Count == 0 )
            return;

        frameTimer += delta;
        var frame = currentAnimation.Frames[currentFrameIndex];

        while ( frameTimer >= frame.Duration )
        {
            frameTimer -= frame.Duration;
            currentFrameIndex++;

            if ( currentFrameIndex >= currentAnimation.Frames.Count )
            {
                if ( currentAnimation.Loop )
                    currentFrameIndex = 0;
                else
                {
                    currentFrameIndex = currentAnimation.Frames.Count - 1; // stop at last frame
                    break;
                }
            }

            frame = currentAnimation.Frames[currentFrameIndex];
        }
    }

    public void Draw( GraphicsDrawer drawer )
    {
        if ( currentAnimation == null || currentAnimation.Frames.Count == 0 )
            return;

        var frame = currentAnimation.Frames[currentFrameIndex];
        drawer.DrawTexture(
            frame.Texture,
            frame.SourceRect,
            Position,
            Color,
            Rotation,
            Scale
        );
    }
}