using System;
using System.Collections.Generic;
using System.Text;

namespace SFML.Game.Framework;

public class Point
{
    /// <summary>
    /// Gets or sets the X coordinate of this <see cref="Point"/>.
    /// </summary>
    public int X { get; set; }

    /// <summary>
    /// Gets or sets the Y coordinate of this <see cref="Point"/>.
    /// </summary>
    public int Y { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Point"/> class.
    /// </summary>
    public Point()
    {
        X = 0;
        Y = 0;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Point"/> class with specified coordinates.
    /// </summary>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    public Point( int x, int y )
    {
        X = x;
        Y = y;
    }

    /// <summary>
    /// Returns the distance between two <see cref="Point"/> instances.
    /// </summary>
    /// <param name="a">The first point.</param>
    /// <param name="b">The second point.</param>
    /// <returns>The distance between points as a float.</returns>
    public static float Distance( Point a, Point b )
        => MathF.Sqrt( ( a.X - b.X ) * ( a.X - b.X ) + ( a.Y - b.Y ) * ( a.Y - b.Y ) );

    /// <summary>
    /// Returns the squared distance between two <see cref="Point"/> instances (avoids square root).
    /// </summary>
    /// <param name="a">The first point.</param>
    /// <param name="b">The second point.</param>
    /// <returns>The squared distance as an integer.</returns>
    public static int DistanceSquared( Point a, Point b )
        => ( a.X - b.X ) * ( a.X - b.X ) + ( a.Y - b.Y ) * ( a.Y - b.Y );

    /// <summary>
    /// Offsets this <see cref="Point"/> by the specified X and Y amounts.
    /// </summary>
    /// <param name="offsetX">The X offset.</param>
    /// <param name="offsetY">The Y offset.</param>
    public void Offset( int offsetX, int offsetY )
    {
        X += offsetX;
        Y += offsetY;
    }

    /// <summary>
    /// Returns a new <see cref="Point"/> offset by the specified amount.
    /// </summary>
    /// <param name="offsetX">The X offset.</param>
    /// <param name="offsetY">The Y offset.</param>
    /// <returns>A new offset point.</returns>
    public Point OffsetCopy( int offsetX, int offsetY )
        => new Point( X + offsetX, Y + offsetY );

    /// <summary>
    /// Converts this <see cref="Point"/> to a <see cref="Vector2"/>.
    /// </summary>
    /// <returns>A <see cref="Vector2"/> with the same coordinates.</returns>
    public Vector2 ToVector2() => new Vector2( X, Y );

    /// <summary>
    /// Returns a new <see cref="Point"/> created by adding two points.
    /// </summary>
    /// <param name="a">The first point.</param>
    /// <param name="b">The second point.</param>
    /// <returns>A new point representing the sum.</returns>
    public static Point Add( Point a, Point b )
        => new Point( a.X + b.X, a.Y + b.Y );

    /// <summary>
    /// Returns a new <see cref="Point"/> created by subtracting one point from another.
    /// </summary>
    /// <param name="a">The first point.</param>
    /// <param name="b">The second point.</param>
    /// <returns>A new point representing the difference.</returns>
    public static Point Subtract( Point a, Point b )
        => new Point( a.X - b.X, a.Y - b.Y );

    /// <summary>
    /// Returns a new <see cref="Point"/> scaled by the given factor.
    /// </summary>
    /// <param name="point">The point to scale.</param>
    /// <param name="scale">The scale factor.</param>
    /// <returns>A new scaled point.</returns>
    public static Point Multiply( Point point, float scale )
        => new Point( ( int )( point.X * scale ), ( int )( point.Y * scale ) );

    /// <summary>
    /// Returns the dot product of two points treated as vectors.
    /// </summary>
    /// <param name="a">The first point.</param>
    /// <param name="b">The second point.</param>
    /// <returns>The dot product as a float.</returns>
    public static float Dot( Point a, Point b )
        => a.X * b.X + a.Y * b.Y;

    /// <summary>
    /// Returns the length (magnitude) of this <see cref="Point"/> treated as a vector.
    /// </summary>
    /// <returns>The magnitude as a float.</returns>
    public float Length() => MathF.Sqrt( X * X + Y * Y );

    // ----------------------------------------------------------
    // Operator Overloads
    // ----------------------------------------------------------

    /// <summary>
    /// Adds two <see cref="Point"/> instances.
    /// </summary>
    public static Point operator +( Point a, Point b ) => new Point( a.X + b.X, a.Y + b.Y );

    /// <summary>
    /// Subtracts one <see cref="Point"/> from another.
    /// </summary>
    public static Point operator -( Point a, Point b ) => new Point( a.X - b.X, a.Y - b.Y );

    /// <summary>
    /// Negates the coordinates of this <see cref="Point"/>.
    /// </summary>
    public static Point operator -( Point p ) => new Point( -p.X, -p.Y );

    /// <summary>
    /// Multiplies a <see cref="Point"/> by a scalar value.
    /// </summary>
    public static Point operator *( Point p, float scale )
        => new Point( ( int )( p.X * scale ), ( int )( p.Y * scale ) );

    /// <summary>
    /// Divides a <see cref="Point"/> by a scalar value.
    /// </summary>
    public static Point operator /( Point p, float scale )
        => new Point( ( int )( p.X / scale ), ( int )( p.Y / scale ) );

    /// <summary>
    /// Determines whether two <see cref="Point"/> instances are equal.
    /// </summary>
    public static bool operator ==( Point a, Point b ) => a.X == b.X && a.Y == b.Y;

    /// <summary>
    /// Determines whether two <see cref="Point"/> instances are not equal.
    /// </summary>
    public static bool operator !=( Point a, Point b ) => !( a == b );

    // ----------------------------------------------------------
    // Utility Overrides
    // ----------------------------------------------------------

    /// <summary>
    /// Determines whether this <see cref="Point"/> is equal to another object.
    /// </summary>
    /// <param name="obj">The object to compare to.</param>
    /// <returns>True if equal; otherwise, false.</returns>
    public override bool Equals( object? obj )
        => obj is Point p && X == p.X && Y == p.Y;

    /// <summary>
    /// Returns a hash code for this <see cref="Point"/>.
    /// </summary>
    /// <returns>An integer hash code.</returns>
    public override int GetHashCode() => HashCode.Combine( X, Y );

    /// <summary>
    /// Returns a string representation of this <see cref="Point"/>.
    /// </summary>
    /// <returns>A string in the format (X, Y).</returns>
    public override string ToString() => $"({X}, {Y})";

    // ----------------------------------------------------------
    // Static Members
    // ----------------------------------------------------------

    /// <summary>
    /// Gets a <see cref="Point"/> representing (0, 0).
    /// </summary>
    public static readonly Point Zero = new Point(0, 0);

    /// <summary>
    /// Gets a <see cref="Point"/> representing (1, 1).
    /// </summary>
    public static readonly Point One = new Point(1, 1);
}
