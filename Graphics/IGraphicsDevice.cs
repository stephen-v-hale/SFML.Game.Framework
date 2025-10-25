using System;
using System.Collections.Generic;
using System.Text;

using SFML.Graphics;

namespace SFML.Game.Framework.Graphics;

public interface IGraphicsDevice : IDisposable
{
    /// <summary>
    /// Gets the <see cref="Game"/> this <see cref="IGraphicsDevice"/> belongs to.
    /// </summary>
    Game Game { get; }

    /// <summary>
    /// Clears the screen.
    /// </summary>
    /// <param name="color"></param>
    void Clear( Color color );

    /// <summary>
    /// Draws a <see cref="SFML.Graphics.Drawable"/>
    /// </summary>
    /// <param name="drawable"></param>
    void Draw( Drawable drawable );

    /// <summary>
    /// Draws a <see cref="Drawable"/> with the specific <see cref="RenderStates"/>
    /// </summary>
    /// <param name="drawable"></param>
    /// <param name="renderState"></param>
    void Draw( Drawable drawable, RenderStates renderState );
}
