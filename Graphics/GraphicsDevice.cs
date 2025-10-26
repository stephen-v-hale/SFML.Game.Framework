using System;
using System.Collections.Generic;
using System.Text;

namespace SFML.Game.Framework.Graphics;

public class GraphicsDevice : IDisposable, IGraphicsDevice
{

    /// <summary>
    /// Gets whether this <see cref="GraphicsDevice"/> has been disposed.
    /// </summary>
    public bool IsDisposed { get; private set; }

    /// <summary>
    /// Gets the <see cref="Game"/> this <see cref="GraphicsDevice"/> belongs too.
    /// </summary>
    public Game Game { get; }

    /// <summary>
    /// Initialize a new instance of <see cref="GraphicsDevice"/>
    /// </summary>
    /// <param name="game">The <see cref="Game"/> this <see cref="GraphicsDevice"/> belongs too.</param>
    /// <exception cref="ArgumentNullException">Thrown if <see cref="Game"/> is null.</exception>
    public GraphicsDevice(Game game)
    {
        if ( game == null ) throw new ArgumentNullException( nameof( game ) );
        this.Game = game;
    }

    public void Clear( Color color )
    {
        Game.window.Clear( color.ToSFMLColor() );
    }

    public void Dispose()
    {
        if ( !IsDisposed )
        {
            Game.Dispose();
            IsDisposed = true;
        }
        else
            throw new ObjectDisposedException( nameof( GraphicsDevice ) );
    }

    /// <summary>
    /// Draws a <see cref="SFML.Graphics.Drawable"/>
    /// </summary>
    /// <param name="drawable"></param>
    public void Draw( SFML.Graphics.Drawable drawable )
    {
        Game.window.Draw( drawable );
    }

    /// <summary>
    /// Draws a <see cref="SFML.Graphics.Drawable"/> with a specific <see cref="SFML.Graphics.RenderTarget"/>
    /// </summary>
    /// <param name="drawable"></param>
    /// <param name="renderState"></param>
    public void Draw( SFML.Graphics.Drawable drawable, SFML.Graphics.RenderStates renderState ) => Game.window.Draw( drawable, renderState );
}
