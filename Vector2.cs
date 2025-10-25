

namespace SFML.Game.Framework;

public struct Vector2
{
    /// <summary>
    /// Gets or sets the X coordinate.
    /// </summary>
    public float X { get; set; }

    /// <summary>
    /// Gets or sets the Y coordinate.
    /// </summary>
    public float Y { get; set; }

    /// <summary>
    /// Initializes a new instance of <see cref="Vector2"/>.
    /// </summary>
    public Vector2( float x, float y )
    {
        X = x;
        Y = y;
    }
    /// <summary>Returns the length (magnitude) of the vector.</summary>
    public float Length() => MathF.Sqrt( X * X + Y * Y );

    /// <summary>Returns the squared length (avoids sqrt).</summary>
    public float LengthSquared() => X * X + Y * Y;

    /// <summary>Returns a normalized (unit length) version of this vector.</summary>
    public Vector2 Normalized()
    {
        float length = Length();
        return length > 0 ? this / length : new Vector2( 0, 0 );
    }

    /// <summary>Normalizes this vector in place.</summary>
    public void Normalize()
    {
        float length = Length();
        if ( length > 0 )
        {
            X /= length;
            Y /= length;
        }
    }

    /// <summary>Calculates the distance between two vectors.</summary>
    public static float Distance( Vector2 a, Vector2 b )
        => MathF.Sqrt( ( a.X - b.X ) * ( a.X - b.X ) + ( a.Y - b.Y ) * ( a.Y - b.Y ) );

    /// <summary>Calculates the squared distance between two vectors.</summary>
    public static float DistanceSquared( Vector2 a, Vector2 b )
        => ( a.X - b.X ) * ( a.X - b.X ) + ( a.Y - b.Y ) * ( a.Y - b.Y );

    /// <summary>Calculates the dot product of two vectors.</summary>
    public static float Dot( Vector2 a, Vector2 b )
        => a.X * b.X + a.Y * b.Y;

    /// <summary>Calculates the angle between two vectors in radians.</summary>
    public static float Angle( Vector2 a, Vector2 b )
    {
        float dot = Dot(a, b);
        float len = a.Length() * b.Length();
        return len == 0 ? 0 : MathF.Acos( dot / len );
    }

    /// <summary>Returns a vector that is the linear interpolation between two vectors.</summary>
    public static Vector2 Lerp( Vector2 a, Vector2 b, float t )
        => a + ( b - a ) * Math.Clamp( t, 0f, 1f );

    /// <summary>Returns a perpendicular vector (rotated 90° counter-clockwise).</summary>
    public Vector2 Perpendicular() => new Vector2( -Y, X );

    public static Vector2 operator +( Vector2 a, Vector2 b ) => new Vector2( a.X + b.X, a.Y + b.Y );
    public static Vector2 operator -( Vector2 a, Vector2 b ) => new Vector2( a.X - b.X, a.Y - b.Y );
    public static Vector2 operator -( Vector2 v ) => new Vector2( -v.X, -v.Y );
    public static Vector2 operator *( Vector2 v, float scalar ) => new Vector2( v.X * scalar, v.Y * scalar );
    public static Vector2 operator *( float scalar, Vector2 v ) => new Vector2( v.X * scalar, v.Y * scalar );
    public static Vector2 operator /( Vector2 v, float scalar ) => new Vector2( v.X / scalar, v.Y / scalar );

    public static bool operator ==( Vector2 a, Vector2 b ) => Math.Abs( a.X - b.X ) < float.Epsilon && Math.Abs( a.Y - b.Y ) < float.Epsilon;
    public static bool operator !=( Vector2 a, Vector2 b ) => !( a == b );
    
    public override bool Equals( object? obj ) => obj is Vector2 v && this == v;
    public override int GetHashCode() => HashCode.Combine( X, Y );
    public override string ToString() => $"({X:0.###}, {Y:0.###})";

    public static readonly Vector2 Zero = new Vector2(0, 0);
    public static readonly Vector2 One = new Vector2(1, 1);
    public static readonly Vector2 UnitX = new Vector2(1, 0);
    public static readonly Vector2 UnitY = new Vector2(0, 1);

    internal static SFML.System.Vector2f ToVector2f( Vector2 vector ) { return new System.Vector2f( vector.X, vector.Y ); }
}