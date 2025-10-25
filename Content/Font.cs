using System;
using System.Collections.Generic;
using System.Text;

using SFML.Graphics;

namespace SFML.Game.Framework.Content;

/// <summary>
/// Represents a font for measuring text size.
/// </summary>
public class Font
{
    private SFML.Graphics.Font sfmlFont;
    private int size;

    /// <summary>
    /// Gets or sets the font style.
    /// </summary>
    public FontStyle Style { get; set; } = FontStyle.Regular;

    /// <summary>
    /// Initializes a new instance of the <see cref="Font"/> class from a file.
    /// </summary>
    /// <param name="fileName">Path to the font file.</param>
    /// <param name="size">Font size.</param>
    public Font( string fileName, int size )
    {
        sfmlFont = new SFML.Graphics.Font( fileName );
        this.size = size;

        Game.logger.Write( $"Font created from '{fileName}', '{size}'", this );
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Font"/> class from a byte array.
    /// </summary>
    /// <param name="fontData">Font data as a byte array.</param>
    /// <param name="size">Font size.</param>
    public Font( byte[] fontData, int size )
    {
        sfmlFont = new SFML.Graphics.Font( fontData );
        this.size = size;
        Game.logger.Write( $"Font created from byte[], '{size}'", this );
    }

    /// <summary>
    /// Measures the width and height of a string using this font and style.
    /// </summary>
    /// <param name="text">The string to measure.</param>
    /// <returns>A <see cref="Vector2"/> containing the width and height.</returns>
    public Vector2 MeasureString( string text, uint size )
    {
        Text tempText = new Text(text, sfmlFont, size)
        {
            Style = ConvertToSFMLStyle(Style)
        };
        FloatRect bounds = tempText.GetLocalBounds();
        return new Vector2( bounds.Width, bounds.Height );
    }

    /// <summary>
    /// Measures the width and height of a string using the font’s natural size and style.
    /// </summary>
    /// <param name="text">The string to measure.</param>
    /// <returns>A <see cref="Vector2"/> containing the width and height.</returns>
    public Vector2 MeasureString( string text )
    {
        // Choose a reasonable "default" size based on font metrics.
        // SFML fonts are scalable, so we can use line spacing as a baseline.
        uint defaultSize = 16; // fallback
        float lineSpacing = sfmlFont.GetLineSpacing(defaultSize);

        Text tempText = new Text(text, sfmlFont, defaultSize)
        {
            Style = ConvertToSFMLStyle(Style)
        };

        FloatRect bounds = tempText.GetLocalBounds();

        // Return bounds scaled relative to the line spacing
        return new Vector2( bounds.Width, lineSpacing );
    }


    /// <summary>
    /// Converts our <see cref="FontStyle"/> enum to SFML <see cref="Text.Styles"/>.
    /// </summary>
    internal Text.Styles ConvertToSFMLStyle( FontStyle style )
    {
        Text.Styles sfmlStyle = Text.Styles.Regular;

        if ( style.HasFlag( FontStyle.Bold ) )
            sfmlStyle |= Text.Styles.Bold;
        if ( style.HasFlag( FontStyle.Italic ) )
            sfmlStyle |= Text.Styles.Italic;
        if ( style.HasFlag( FontStyle.Underlined ) )
            sfmlStyle |= Text.Styles.Underlined;
        if ( style.HasFlag( FontStyle.StrikeThrough ) )
            sfmlStyle |= Text.Styles.StrikeThrough;

        return sfmlStyle;
    }

    internal SFML.Graphics.Font ToSFML() => sfmlFont;
}