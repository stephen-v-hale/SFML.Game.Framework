
using System.Globalization;

namespace SFML.Game.Framework;

public class Color
{
    /// <summary>
    /// Gets or sets the red component of this <see cref="Color"/> (0–255).
    /// </summary>
    public int R { get; set; }

    /// <summary>
    /// Gets or sets the green component of this <see cref="Color"/> (0–255).
    /// </summary>
    public int G { get; set; }

    /// <summary>
    /// Gets or sets the blue component of this <see cref="Color"/> (0–255).
    /// </summary>
    public int B { get; set; }

    /// <summary>
    /// Gets or sets the alpha (transparency) component of this <see cref="Color"/> (0–255).
    /// </summary>
    public int A { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Color"/> class with RGBA values (0–255).
    /// </summary>
    /// <param name="r">Red (0–255).</param>
    /// <param name="g">Green (0–255).</param>
    /// <param name="b">Blue (0–255).</param>
    /// <param name="a">Alpha (0–255).</param>
    public Color( int r, int g, int b, int a = 255 )
    {
        R = r;
        G = g;
        B = b;
        A = a;
    }
    /// <summary>
    /// Returns a new <see cref="Color"/> with each component clamped to the [0, 255] range.
    /// </summary>
    public Color Clamp()
    {
        return new Color(
            Math.Clamp( R, 0, 255 ),
            Math.Clamp( G, 0, 255 ),
            Math.Clamp( B, 0, 255 ),
            Math.Clamp( A, 0, 255 ) );
    }

    /// <summary>
    /// Linearly interpolates between two colors.
    /// </summary>
    /// <param name="a">The first color.</param>
    /// <param name="b">The second color.</param>
    /// <param name="t">Interpolation factor (0–1).</param>
    /// <returns>The interpolated color.</returns>
    public static Color Lerp( Color a, Color b, float t )
    {
        t = Math.Clamp( t, 0f, 1f );
        return new Color(
            ( int )( a.R + ( b.R - a.R ) * t ),
            ( int )( a.G + ( b.G - a.G ) * t ),
            ( int )( a.B + ( b.B - a.B ) * t ),
            ( int )( a.A + ( b.A - a.A ) * t ) );
    }

    /// <summary>
    /// Converts this color to a hexadecimal string in the format #RRGGBB or #RRGGBBAA.
    /// </summary>
    /// <param name="includeAlpha">Whether to include alpha in the output.</param>
    public string ToHex( bool includeAlpha = true )
    {
        return includeAlpha
            ? $"#{R:X2}{G:X2}{B:X2}{A:X2}"
            : $"#{R:X2}{G:X2}{B:X2}";
    }

    /// <summary>
    /// Creates a color from a hexadecimal string (e.g. "#FF00FF" or "#FF00FFFF").
    /// </summary>
    public static Color FromHex( string hex )
    {
        if ( string.IsNullOrWhiteSpace( hex ) )
            throw new ArgumentException( "Invalid hex string" );

        hex = hex.TrimStart( '#' );
        if ( hex.Length != 6 && hex.Length != 8 )
            throw new ArgumentException( "Hex must be 6 or 8 characters long" );

        int r = int.Parse(hex.Substring(0, 2), NumberStyles.HexNumber);
        int g = int.Parse(hex.Substring(2, 2), NumberStyles.HexNumber);
        int b = int.Parse(hex.Substring(4, 2), NumberStyles.HexNumber);
        int a = hex.Length == 8 ? int.Parse(hex.Substring(6, 2), NumberStyles.HexNumber) : 255;

        return new Color( r, g, b, a );
    }



    /// <summary>
    /// Returns a string representation of this color.
    /// </summary>
    public override string ToString() =>
        $"Color(R={R}, G={G}, B={B}, A={A})";

    /// <summary>
    /// Determines equality with another object.
    /// </summary>
    public override bool Equals( object? obj ) =>
        obj is Color c && R == c.R && G == c.G && B == c.B && A == c.A;

    /// <summary>
    /// Gets the hash code for this color.
    /// </summary>
    public override int GetHashCode() => HashCode.Combine( R, G, B, A );


    #region Color Utilities

    /// <summary>
    /// Lightens the color by a fraction (0 = unchanged, 1 = white).
    /// </summary>
    public Color Lighten( float amount )
    {
        return new Color(
            ( byte )Math.Min( 255, R + 255 * amount ),
            ( byte )Math.Min( 255, G + 255 * amount ),
            ( byte )Math.Min( 255, B + 255 * amount ),
            A
        );
    }

    /// <summary>
    /// Darkens the color by a fraction (0 = unchanged, 1 = black).
    /// </summary>
    public Color Darken( float amount )
    {
        return new Color(
            ( byte )Math.Max( 0, R - 255 * amount ),
            ( byte )Math.Max( 0, G - 255 * amount ),
            ( byte )Math.Max( 0, B - 255 * amount ),
            A
        );
    }

    /// <summary>
    /// Blends this color with another color using a weight (0 = this, 1 = other).
    /// </summary>
    public Color Blend( Color other, float weight )
    {
        weight = Math.Clamp( weight, 0f, 1f );
        float inv = 1f - weight;
        return new Color(
            ( byte )( R * inv + other.R * weight ),
            ( byte )( G * inv + other.G * weight ),
            ( byte )( B * inv + other.B * weight ),
            ( byte )( A * inv + other.A * weight )
        );
    }

    /// <summary>
    /// Returns a grayscale version of the color.
    /// </summary>
    public Color ToGrayScale()
    {
        byte gray = (byte)((R * 0.299f) + (G * 0.587f) + (B * 0.114f));
        return new Color( gray, gray, gray, A );
    }

    /// <summary>
    /// Returns the inverted color (negative).
    /// </summary>
    public Color Invert()
    {
        return new Color( ( byte )( 255 - R ), ( byte )( 255 - G ), ( byte )( 255 - B ), A );
    }

    /// <summary>
    /// Returns a new color with the specified alpha channel.
    /// </summary>
    public Color WithAlpha( byte alpha )
    {
        return new Color( R, G, B, alpha );
    }

    /// <summary>
    /// Multiplies the RGB channels by a factor (for brightness adjustment).
    /// </summary>
    public Color Multiply( float factor )
    {
        return new Color(
            ( byte )Math.Clamp( R * factor, 0, 255 ),
            ( byte )Math.Clamp( G * factor, 0, 255 ),
            ( byte )Math.Clamp( B * factor, 0, 255 ),
            A
        );
    }

    #endregion
    
    
    /// <summary>
    /// Adds two colors together (component-wise).
    /// </summary>
    public static Color operator +( Color a, Color b ) =>
        new Color( a.R + b.R, a.G + b.G, a.B + b.B, a.A + b.A ).Clamp();

    /// <summary>
    /// Subtracts one color from another (component-wise).
    /// </summary>
    public static Color operator -( Color a, Color b ) =>
        new Color( a.R - b.R, a.G - b.G, a.B - b.B, a.A - b.A ).Clamp();

    /// <summary>
    /// Multiplies a color by a scalar.
    /// </summary>
    public static Color operator *( Color c, float scale ) =>
        new Color(
            ( int )( c.R * scale ),
            ( int )( c.G * scale ),
            ( int )( c.B * scale ),
            ( int )( c.A * scale ) ).Clamp();

    /// <summary>
    /// Divides a color by a scalar.
    /// </summary>
    public static Color operator /( Color c, float scale ) =>
        new Color(
            ( int )( c.R / scale ),
            ( int )( c.G / scale ),
            ( int )( c.B / scale ),
            ( int )( c.A / scale ) ).Clamp();

    /// <summary>
    /// Determines whether two colors are equal.
    /// </summary>
    public static bool operator ==( Color a, Color b ) => a.Equals( b );

    /// <summary>
    /// Determines whether two colors are not equal.
    /// </summary>
    public static bool operator !=( Color a, Color b ) => !a.Equals( b );

    public static readonly Color Transparent = new(0, 0, 0, 0);
    public static readonly Color Black = new(0, 0, 0);
    public static readonly Color White = new(255, 255, 255);
    public static readonly Color Red = new(255, 0, 0);
    public static readonly Color Green = new(0, 255, 0);
    public static readonly Color Blue = new(0, 0, 255);
    public static readonly Color Yellow = new(255, 255, 0);
    public static readonly Color Cyan = new(0, 255, 255);
    public static readonly Color Magenta = new(255, 0, 255);
    public static readonly Color Gray = new(128, 128, 128);
    public static readonly Color DarkGray = new(64, 64, 64);
    public static readonly Color LightGray = new(192, 192, 192);
    public static readonly Color Orange = new(255, 165, 0);
    public static readonly Color Brown = new(139, 69, 19);
    public static readonly Color Pink = new(255, 192, 203);
    public static readonly Color Purple = new(128, 0, 128);
    public static readonly Color Lime = new(191, 255, 0);
    public static readonly Color Olive = new(128, 128, 0);
    public static readonly Color Teal = new(0, 128, 128);
    public static readonly Color Navy = new(0, 0, 128);
    public static readonly Color Gold = new(255, 215, 0);
    public static readonly Color Silver = new(192, 192, 192);
    public static readonly Color Coral = new(255, 127, 80);
    public static readonly Color Salmon = new(250, 128, 114);
    public static readonly Color Crimson = new(220, 20, 60);
    public static readonly Color Indigo = new(75, 0, 130);
    public static readonly Color Violet = new(238, 130, 238);
    public static readonly Color Maroon = new(128, 0, 0);
    public static readonly Color Beige = new(245, 245, 220);
    public static readonly Color Mint = new(152, 255, 152);
    public static readonly Color Ivory = new(255, 255, 240);
    public static readonly Color Khaki = new(195, 176, 145);
    public static readonly Color Lavender = new(230, 230, 250);
    public static readonly Color Plum = new(221, 160, 221);
    public static readonly Color Tomato = new(255, 99, 71);
    public static readonly Color Wheat = new(245, 222, 179);
    public static readonly Color SkyBlue = new(135, 206, 235);
    public static readonly Color DeepSkyBlue = new(0, 191, 255);
    public static readonly Color DodgerBlue = new(30, 144, 255);
    public static readonly Color RoyalBlue = new(65, 105, 225);
    public static readonly Color SteelBlue = new(70, 130, 180);
    public static readonly Color SlateBlue = new(106, 90, 205);
    public static readonly Color MediumSlateBlue = new(123, 104, 238);
    public static readonly Color LightSteelBlue = new(176, 196, 222);
    public static readonly Color DeepPink = new(255, 20, 147);
    public static readonly Color HotPink = new(255, 105, 180);
    public static readonly Color Orchid = new(218, 112, 214);
    public static readonly Color MediumOrchid = new(186, 85, 211);
    public static readonly Color DarkOrchid = new(153, 50, 204);
    public static readonly Color DarkViolet = new(148, 0, 211);
    public static readonly Color MediumVioletRed = new(199, 21, 133);
    public static readonly Color PaleVioletRed = new(219, 112, 147);
    public static readonly Color LightPink = new(255, 182, 193);
    public static readonly Color DarkRed = new(139, 0, 0);
    public static readonly Color Firebrick = new(178, 34, 34);
    public static readonly Color OrangeRed = new(255, 69, 0);
    public static readonly Color DarkOrange = new(255, 140, 0);
    public static readonly Color LightCoral = new(240, 128, 128);
    public static readonly Color IndianRed = new(205, 92, 92);
    public static readonly Color RosyBrown = new(188, 143, 143);
    public static readonly Color SandyBrown = new(244, 164, 96);
    public static readonly Color Peru = new(205, 133, 63);
    public static readonly Color Chocolate = new(210, 105, 30);
    public static readonly Color SaddleBrown = new(139, 69, 19);
    public static readonly Color Sienna = new(160, 82, 45);
    public static readonly Color DarkGoldenrod = new(184, 134, 11);
    public static readonly Color LightGoldenrod = new(238, 221, 130);
    public static readonly Color PaleGoldenrod = new(238, 232, 170);
    public static readonly Color LightYellow = new(255, 255, 224);
    public static readonly Color LemonChiffon = new(255, 250, 205);
    public static readonly Color LightCyan = new(224, 255, 255);
    public static readonly Color PowderBlue = new(176, 224, 230);
    public static readonly Color LightBlue = new(173, 216, 230);
    public static readonly Color MediumBlue = new(0, 0, 205);
    public static readonly Color DarkBlue = new(0, 0, 139);
    public static readonly Color MidnightBlue = new(25, 25, 112);
    public static readonly Color LightGreen = new(144, 238, 144);
    public static readonly Color MediumSeaGreen = new(60, 179, 113);
    public static readonly Color SeaGreen = new(46, 139, 87);
    public static readonly Color DarkGreen = new(0, 100, 0);
    public static readonly Color ForestGreen = new(34, 139, 34);
    public static readonly Color PaleGreen = new(152, 251, 152);
    public static readonly Color SpringGreen = new(0, 255, 127);
    public static readonly Color MediumSpringGreen = new(0, 250, 154);
    public static readonly Color LawnGreen = new(124, 252, 0);
    public static readonly Color Chartreuse = new(127, 255, 0);
    public static readonly Color DarkOliveGreen = new(85, 107, 47);
    public static readonly Color YellowGreen = new(154, 205, 50);
    public static readonly Color CornflowerBlue = new Color(100, 149, 237, 255);
    internal SFML.Graphics.Color ToSFMLColor() => new SFML.Graphics.Color( ( byte )this.R, ( byte )G, ( byte )B, ( byte )A );
}
