using System;
using System.Collections.Generic;
using System.Text;

using SFML.Graphics;

namespace SFML.Game.Framework.Content;
#nullable disable
public class Texture2D
{
    string fileName = "";
    internal SFML.Graphics.Texture texture = null;

    /// <summary>
    /// Initialize a new instance of <see cref="Texture2D"/>
    /// </summary>
    /// <param name="fileName">the filename of the texture</param>
    /// <exception cref="ArgumentNullException"></exception>
    public Texture2D( string fileName )
    {
        if ( string.IsNullOrWhiteSpace( fileName ) ) throw new ArgumentNullException( fileName );

        this.fileName = fileName;
        texture = new SFML.Graphics.Texture( fileName );

        Game.logger.Write( $"Texture created from '{fileName}'", this );
    }

    /// <summary>
    /// Initalize a new instance of <see cref="Texture2D"/>
    /// </summary>
    /// <param name="texture">The byte array.</param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public Texture2D( byte[] texture )
    {
        if ( texture.Length < 0 ) throw new ArgumentOutOfRangeException( nameof( texture ) );
        fileName = "";

        this.texture = new SFML.Graphics.Texture( texture );

        Game.logger.Write( "Texture created from byte[]", this );
    }

    /// <summary>
    /// Creates a texture of specified width and height from an array of colors.
    /// </summary>
    /// <param name="width">Texture width in pixels.</param>
    /// <param name="height">Texture height in pixels.</param>
    /// <param name="colors">Array of colors (length must be width * height).</param>
    public Texture2D( int width, int height, Color[] colors )
    {
        if ( colors.Length != width * height )
            throw new ArgumentException( "Length of colors array must be width * height." );

        // Create a SFML Image
        Image image = new Image((uint)width, (uint)height);

        for ( uint y = 0; y < height; y++ )
        {
            for ( uint x = 0; x < width; x++ )
            {
                Color c = colors[y * width + (int)x];
                image.SetPixel( x, y, c.ToSFMLColor() );
            }
        }

        // Create SFML Texture from Image
        texture = new Texture( image );

        Game.logger.Write( $"Texture created from (width: {width}, height: {height}, Colors: {colors.Length}", this );
    }

    /// <summary>
    /// Creates a texture from a specified image
    /// </summary>
    public Texture2D( Image image )
    {
        if ( image == null )
        {
            Game.logger.Write( "Image must not be a null reference", Logging.EntryType.Critical, this );
            throw new ArgumentNullException( nameof( image ) );
        }

        this.texture = new Texture( image );
    }

    /// <summary>
    /// Creates a texture of specified width and height from an array of colors.
    /// </summary>
    /// <param name="width">Texture width in pixels.</param>
    /// <param name="height">Texture height in pixels.</param>
    public Texture2D( int width, int height )
    {
        // Create a SFML Image
        Image image = new Image((uint)width, (uint)height);
        // Create SFML Texture from Image
        texture = new Texture( image );

        Game.logger.Write( $"Texture created from (width: {width}, height: {height}", this );
    }

    /// <summary>
    /// Gets the size of this <see cref="Texture2D"/>
    /// </summary>
    public Size Size => new Size( ( int )texture.Size.X, ( int )texture.Size.Y );

    /// <summary>
    /// Sets a single pixel in the texture by its 1D index and updates the SFML texture.
    /// </summary>
    /// <param name="pixel">Pixel index (0 to Width*Height-1)</param>
    /// <param name="color">The new color</param>
    public void SetPixel( int pixel, Color color )
    {
        if ( pixel < 0 || pixel >= Size.Width * Size.Height )
            throw new ArgumentOutOfRangeException( nameof( pixel ) );

        uint x = (uint)(pixel % Size.Width);
        uint y = (uint)(pixel / Size.Width);

        // Copy current texture to an image
        Image image = texture.CopyToImage();
        image.SetPixel( x, y, color.ToSFMLColor() );

        // Update the texture
        texture.Update( image );
    }

    /// <summary>
    /// Sets a single pixel in the texture at the specific width, and height.
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="color"></param>
    public void SetPixel( int width, int height, Color color )
    {
        var image = texture.CopyToImage();
        image.SetPixel( ( uint )width, ( uint )height, color.ToSFMLColor() );
        texture.Update( image );
    }

    internal static Texture2D FromSFML( Texture texture )
    {
        Texture2D t = new Texture2D(0, 0);
        t.texture = texture;
        return t;
    }

    /// <summary>
    /// Resize this texture.
    /// </summary>
    /// <param name="newWidth"></param>
    /// <param name="newHeight"></param>
    /// <param name="smooth"></param>
    /// <exception cref="ArgumentException"></exception>
    public void Resize( int newWidth, int newHeight, bool smooth = true )
    {
        if ( newWidth <= 0 || newHeight <= 0 )
        {
            Game.logger.Write( "Width and height must be greater than zero", Logging.EntryType.Warning, this );
            throw new ArgumentException( "Width and height must be greater than zero." );
        }

        // Create a render target to draw the scaled image into
        var renderTexture = new RenderTexture((uint)newWidth, (uint)newHeight);

        // Create a sprite of the current texture
        var sprite = new Sprite(this.texture)
        {
            Scale = new SFML.System.Vector2f(
            (float)newWidth / texture.Size.X,
            (float)newHeight / texture.Size.Y)
        };

        // Apply smoothing if desired
        texture.Smooth = smooth;

        // Draw the sprite into the render texture
        renderTexture.Clear( Color.Transparent.ToSFMLColor() );
        renderTexture.Draw( sprite );
        renderTexture.Display();

        // Create a new texture from the render texture result
        var resizedTexture = new Texture2D(renderTexture.Texture.CopyToImage());

        // Optionally carry over smoothing state
        resizedTexture.texture.Smooth = smooth;

        this.texture = renderTexture.Texture;
    }
}
