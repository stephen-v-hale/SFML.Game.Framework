using System;

using SFML.Graphics;

namespace SFML.Game.Framework;

public class Rectangle
{
    /// <summary>
    /// Gets the X position (left coordinate) of this <see cref="Rectangle"/>.
    /// </summary>
    public int X { get; }

    /// <summary>
    /// Gets the Y position (top coordinate) of this <see cref="Rectangle"/>.
    /// </summary>
    public int Y { get; }

    /// <summary>
    /// Gets the width of this <see cref="Rectangle"/>.
    /// </summary>
    public int Width { get; }

    /// <summary>
    /// Gets the height of this <see cref="Rectangle"/>.
    /// </summary>
    public int Height { get; }

    /// <summary>
    /// Gets the left edge (same as X).
    /// </summary>
    public int Left => X;

    /// <summary>
    /// Gets the right edge (X + Width).
    /// </summary>
    public int Right => X + Width;

    /// <summary>
    /// Gets the top edge (same as Y).
    /// </summary>
    public int Top => Y;

    /// <summary>
    /// Gets the bottom edge (Y + Height).
    /// </summary>
    public int Bottom => Y + Height;

    /// <summary>
    /// Gets the X coordinate of the rectangle’s center.
    /// </summary>
    public int CenterX => ( X + Width / 2 );

    /// <summary>
    /// Gets the Y coordinate of the rectangle’s center.
    /// </summary>
    public int CenterY => ( Y + Height / 2 );

    /// <summary>
    /// Initializes a new instance of the <see cref="Rectangle"/> class.
    /// </summary>
    /// <param name="x">The X coordinate of the top-left corner.</param>
    /// <param name="y">The Y coordinate of the top-left corner.</param>
    /// <param name="width">The width of the rectangle.</param>
    /// <param name="height">The height of the rectangle.</param>
    public Rectangle( int x, int y, int width, int height )
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }

    // ----------------------------------------------------------
    // Core Methods
    // ----------------------------------------------------------

    /// <summary>
    /// Determines whether the specified point is contained within this <see cref="Rectangle"/>.
    /// </summary>
    /// <param name="x">The X coordinate of the point.</param>
    /// <param name="y">The Y coordinate of the point.</param>
    /// <returns>True if the point is inside this rectangle; otherwise, false.</returns>
    public bool Contains( int x, int y )
        => x >= Left && x < Right && y >= Top && y < Bottom;

    /// <summary>
    /// Determines whether the specified <see cref="Rectangle"/> is entirely contained within this one.
    /// </summary>
    /// <param name="rect">The rectangle to check.</param>
    /// <returns>True if the given rectangle is fully inside this one; otherwise, false.</returns>
    public bool Contains( Rectangle rect )
        => rect.Left >= Left && rect.Right <= Right && rect.Top >= Top && rect.Bottom <= Bottom;

    /// <summary>
    /// Determines whether this <see cref="Rectangle"/> intersects with another rectangle.
    /// </summary>
    /// <param name="rect">The rectangle to test against.</param>
    /// <returns>True if the rectangles intersect; otherwise, false.</returns>
    public bool Intersects( Rectangle rect )
        => !( rect.Left >= Right || rect.Right <= Left || rect.Top >= Bottom || rect.Bottom <= Top );

    /// <summary>
    /// Returns the overlapping area between two rectangles.
    /// </summary>
    /// <param name="a">The first rectangle.</param>
    /// <param name="b">The second rectangle.</param>
    /// <returns>
    /// A new <see cref="Rectangle"/> representing the intersection area,
    /// or <see cref="Empty"/> if they do not overlap.
    /// </returns>
    public static Rectangle Intersect( Rectangle a, Rectangle b )
    {
        int left = Math.Max(a.Left, b.Left);
        int top = Math.Max(a.Top, b.Top);
        int right = Math.Min(a.Right, b.Right);
        int bottom = Math.Min(a.Bottom, b.Bottom);

        if ( right > left && bottom > top )
            return new Rectangle( left, top, right - left, bottom - top );
        else
            return Empty;
    }

    /// <summary>
    /// Returns the smallest <see cref="Rectangle"/> that contains both input rectangles.
    /// </summary>
    /// <param name="a">The first rectangle.</param>
    /// <param name="b">The second rectangle.</param>
    /// <returns>A new rectangle containing both.</returns>
    public static Rectangle Union( Rectangle a, Rectangle b )
    {
        int left = Math.Min(a.Left, b.Left);
        int top = Math.Min(a.Top, b.Top);
        int right = Math.Max(a.Right, b.Right);
        int bottom = Math.Max(a.Bottom, b.Bottom);

        return new Rectangle( left, top, right - left, bottom - top );
    }

    /// <summary>
    /// Returns a new <see cref="Rectangle"/> offset by the specified amount.
    /// </summary>
    /// <param name="offsetX">The amount to move along the X axis.</param>
    /// <param name="offsetY">The amount to move along the Y axis.</param>
    /// <returns>A new rectangle with adjusted position.</returns>
    public Rectangle Offset( int offsetX, int offsetY )
        => new Rectangle( X + offsetX, Y + offsetY, Width, Height );

    /// <summary>
    /// Expands or contracts this <see cref="Rectangle"/> by the given horizontal and vertical amounts.
    /// </summary>
    /// <param name="horizontalAmount">Amount to expand/contract horizontally.</param>
    /// <param name="verticalAmount">Amount to expand/contract vertically.</param>
    /// <returns>A new inflated or deflated rectangle.</returns>
    public Rectangle Inflate( int horizontalAmount, int verticalAmount )
        => new Rectangle( X - horizontalAmount, Y - verticalAmount,
                         Width + horizontalAmount * 2, Height + verticalAmount * 2 );

    /// <summary>
    /// Gets the geometric center of this <see cref="Rectangle"/> as a <see cref="Vector2"/>.
    /// </summary>
    /// <returns>A <see cref="Vector2"/> representing the rectangle's center point.</returns>
    public Vector2 Center() => new Vector2( CenterX, CenterY );

    /// <summary>
    /// Gets the top-left position of this <see cref="Rectangle"/> as a <see cref="Vector2"/>.
    /// </summary>
    /// <returns>A <see cref="Vector2"/> representing the top-left corner.</returns>
    public Vector2 Position() => new Vector2( X, Y );

    /// <summary>
    /// Returns whether this rectangle overlaps another.
    /// </summary>
    /// <param name="rect">The rectangle to test against.</param>
    /// <returns>True if overlapping; otherwise, false.</returns>
    public bool Overlaps( Rectangle rect ) => Intersects( rect );

    /// <summary>
    /// Calculates the area of this <see cref="Rectangle"/>.
    /// </summary>
    /// <returns>The total area (Width × Height).</returns>
    public int Area() => Width * Height;

    /// <summary>
    /// Determines whether this rectangle has zero or negative dimensions.
    /// </summary>
    /// <returns>True if empty or invalid; otherwise, false.</returns>
    public bool IsEmpty() => Width <= 0 || Height <= 0;

    // ----------------------------------------------------------
    // Static Members
    // ----------------------------------------------------------

    /// <summary>
    /// Gets an empty <see cref="Rectangle"/> instance (0,0,0,0).
    /// </summary>
    public static readonly Rectangle Empty = new Rectangle(0, 0, 0, 0);

    // ----------------------------------------------------------
    // Utility Overrides
    // ----------------------------------------------------------

    /// <summary>
    /// Returns a string representation of this <see cref="Rectangle"/>.
    /// </summary>
    /// <returns>A string describing its position and size.</returns>
    public override string ToString() => $"Rectangle({X}, {Y}, {Width}, {Height})";

    /// <summary>
    /// Determines whether the specified object is equal to this <see cref="Rectangle"/>.
    /// </summary>
    /// <param name="obj">The object to compare with.</param>
    /// <returns>True if equal; otherwise, false.</returns>
    public override bool Equals( object? obj )
    {
        if ( obj is Rectangle r )
            return X == r.X && Y == r.Y && Width == r.Width && Height == r.Height;
        return false;
    }

    /// <summary>
    /// Returns a hash code for this <see cref="Rectangle"/>.
    /// </summary>
    /// <returns>A hash code representing this instance.</returns>
    public override int GetHashCode() => HashCode.Combine( X, Y, Width, Height );

    internal static FloatRect ToFloatRect( Rectangle rect ) => new FloatRect( rect.Left, rect.Top, rect.Width, rect.Height );
    internal static IntRect ToIntRect( Rectangle rect ) => new IntRect( rect.Left, rect.Top, rect.Width, rect.Height );
}
