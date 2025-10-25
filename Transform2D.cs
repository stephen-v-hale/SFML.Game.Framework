using System;
using System.Collections.Generic;
using System.Text;

using SFML.Graphics;

namespace SFML.Game.Framework;


/// <summary>
/// Represents a 2D transformation (translation, rotation, scale) similar to SFML's Transform.
/// </summary>
public class Transform2D
{
    /// <summary>
    /// The underlying SFML Transform.
    /// </summary>
    public Transform Transform { get; private set; } = Transform.Identity;

    /// <summary>
    /// Initializes a new identity transform (no translation, rotation, or scale).
    /// </summary>
    public Transform2D() { }

    /// <summary>
    /// Translates the transform by the given offset.
    /// </summary>
    /// <param name="offset">Translation offset.</param>
    public void Translate( Vector2 offset )
    {
        Transform.Translate( Vector2.ToVector2f(offset) );
    }

    /// <summary>
    /// Rotates the transform around the origin.
    /// </summary>
    /// <param name="angle">Rotation in degrees.</param>
    public void Rotate( float angle )
    {
        Transform.Rotate( angle );
    }

    /// <summary>
    /// Scales the transform by the given factors.
    /// </summary>
    /// <param name="scale">Scaling factors.</param>
    public void Scale( Vector2 scale )
    {
        Transform.Scale( Vector2.ToVector2f( scale ) );
    }

    /// <summary>
    /// Transforms a point using this transform.
    /// </summary>
    /// <param name="point">Point to transform.</param>
    /// <returns>Transformed point.</returns>
    public Vector2 TransformPoint( Vector2 point )
    {
        var vect = Transform.TransformPoint( Vector2.ToVector2f(point) );
        return new( vect.X, vect.Y );
    }

    /// <summary>
    /// Transforms a rectangle by applying the transform to its corners.
    /// </summary>
    /// <param name="rect">Rectangle to transform.</param>
    /// <returns>Transformed rectangle as FloatRect.</returns>
    public Rectangle TransformRect( Rectangle rect )
    {
        Vector2 topLeft = TransformPoint(new Vector2(rect.Left, rect.Top));
        Vector2 topRight = TransformPoint(new Vector2(rect.Left + rect.Width, rect.Top));
        Vector2 bottomLeft = TransformPoint(new Vector2(rect.Left, rect.Top + rect.Height));
        Vector2 bottomRight = TransformPoint(new Vector2(rect.Left + rect.Width, rect.Top + rect.Height));

        float minX = MathF.Min(topLeft.X, MathF.Min(topRight.X, MathF.Min(bottomLeft.X, bottomRight.X)));
        float minY = MathF.Min(topLeft.Y, MathF.Min(topRight.Y, MathF.Min(bottomLeft.Y, bottomRight.Y)));
        float maxX = MathF.Max(topLeft.X, MathF.Max(topRight.X, MathF.Max(bottomLeft.X, bottomRight.X)));
        float maxY = MathF.Max(topLeft.Y, MathF.Max(topRight.Y, MathF.Max(bottomLeft.Y, bottomRight.Y)));

        return new Rectangle( (int)minX, ( int )minY, ( int )maxX - ( int )minX, ( int )maxY - ( int )minY );
    }

    /// <summary>
    /// Resets the transform to identity (no translation, rotation, or scale).
    /// </summary>
    public void Reset()
    {
        Transform = Transform.Identity;
    }
}
