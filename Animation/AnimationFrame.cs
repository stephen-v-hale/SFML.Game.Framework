using System;
using System.Collections.Generic;
using System.Text;

using SFML.Game.Framework.Content;

namespace SFML.Game.Framework.Animation;

public class AnimationFrame
{
    /// <summary>
    /// Texture for this frame.
    /// </summary>
    public Texture2D Texture { get; }

    /// <summary>
    /// Source rectangle in the texture (optional for sprite sheets).
    /// </summary>
    public Rectangle SourceRect { get; }

    /// <summary>
    /// Duration of the frame in seconds.
    /// </summary>
    public float Duration { get; }

    public AnimationFrame( Texture2D texture, Rectangle sourceRect, float duration )
    {
        Texture = texture;
        SourceRect = sourceRect;
        Duration = duration;
    }

    public AnimationFrame( Texture2D texture, float duration )
        : this( texture, new Rectangle( 0, 0, texture.Size.Width, texture.Size.Height ), duration ) { }
}
