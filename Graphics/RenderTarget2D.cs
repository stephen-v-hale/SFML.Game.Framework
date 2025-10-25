using System;
using System.Collections.Generic;
using System.Text;

using SFML.Game.Framework.Content;

namespace SFML.Game.Framework.Graphics;

/// <summary>
/// Represents an offscreen render target (texture) that can be drawn to like a window.
/// </summary>
public class RenderTarget2D : IDisposable
{
    private SFML.Graphics.RenderTexture renderTexture;
    private bool isDisposed = false;

    /// <summary>
    /// The width of the render target in pixels.
    /// </summary>
    public int Width { get; private set; }

    /// <summary>
    /// The height of the render target in pixels.
    /// </summary>
    public int Height { get; private set; }

    /// <summary>
    /// The underlying texture that can be drawn from.
    /// </summary>
    public Texture2D Texture { get; private set; }

    /// <summary>
    /// Creates a new offscreen render target with the given size.
    /// </summary>
    public RenderTarget2D( int width, int height )
    {
        Width = width;
        Height = height;
        renderTexture = new SFML.Graphics.RenderTexture( ( uint )width, ( uint )height );
        Texture = Texture2D.FromSFML( renderTexture.Texture );
    }

    /// <summary>
    /// Clears the render target with the given color.
    /// </summary>
    public void Clear( Color color )
    {
        renderTexture.Clear( color.ToSFMLColor() );
    }

    /// <summary>
    /// Display the draw content.
    /// </summary>
    public void Display() => renderTexture.Display();

    public void Dispose()
    {
        if ( !isDisposed )
        {
            renderTexture.Dispose();
            isDisposed = true;
        }
    }
}