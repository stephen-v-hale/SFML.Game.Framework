using System;
using System.Collections.Generic;
using System.Text;

using SFML.Graphics;
using SFML.System;

namespace SFML.Game.Framework;

/// <summary>
/// Represents a 2D camera that can control the view of the scene.
/// Inherits from <see cref="Transform2D"/> to use its translation, rotation, and scaling.
/// </summary>
public class Camera : Transform2D
{
    /// <summary>
    /// Gets or sets the position of the camera in world space.
    /// </summary>
    public Vector2 Position { get; private set; } = new Vector2( 0, 0 );

    /// <summary>
    /// Gets or sets the rotation of the camera in degrees.
    /// </summary>
    public float RotationAngle { get; private set; } = 0f;

    /// <summary>
    /// Gets or sets the zoom (scale) of the camera.
    /// </summary>
    public Vector2 ScaleFactor { get; private set; } = new Vector2( 1f, 1f );

    /// <summary>
    /// Moves the camera by the given offset.
    /// </summary>
    /// <param name="offset">The offset to move the camera.</param>
    public void Move( Vector2 offset )
    {
        Position += offset;
        UpdateTransform();
    }

    /// <summary>
    /// Sets the absolute position of the camera.
    /// </summary>
    /// <param name="position">The new position of the camera.</param>
    public void SetPosition( Vector2 position )
    {
        Position = position;
        UpdateTransform();
    }

    /// <summary>
    /// Rotates the camera by the given angle in degrees.
    /// </summary>
    /// <param name="angle">Rotation angle.</param>
    public new void Rotate( float angle )
    {
        RotationAngle += angle;
        UpdateTransform();
    }

    /// <summary>
    /// Sets the absolute rotation of the camera in degrees.
    /// </summary>
    /// <param name="angle">Rotation angle.</param>
    public void SetRotation( float angle )
    {
        RotationAngle = angle;
        UpdateTransform();
    }

    /// <summary>
    /// Zooms the camera by a scale factor.
    /// </summary>
    /// <param name="factor">Scale factor. Greater than 1 zooms in, less than 1 zooms out.</param>
    public void Zoom( Vector2 factor )
    {
        ScaleFactor = new Vector2( ScaleFactor.X * factor.X, ScaleFactor.Y * factor.Y );
        UpdateTransform();
    }

    /// <summary>
    /// Sets the absolute scale of the camera.
    /// </summary>
    /// <param name="scale">Scale vector.</param>
    public void SetScale( Vector2 scale )
    {
        ScaleFactor = scale;
        UpdateTransform();
    }

    /// <summary>
    /// Returns the SFML transform representing the camera's world-to-view transform.
    /// </summary>
    /// <returns>The SFML.Transform to use in RenderStates.</returns>
    public Transform GetSFMLTransform()
    {
        return Transform;
    }

    /// <summary>
    /// Smoothly follows a target position with a given speed factor.
    /// </summary>
    /// <param name="target">Target position to follow.</param>
    /// <param name="lerpFactor">Interpolation factor (0-1), higher = faster follow.</param>
    public void Follow( Vector2 target, float lerpFactor )
    {
        Vector2 newPos = new Vector2(
                Position.X + (target.X - Position.X) * lerpFactor,
                Position.Y + (target.Y - Position.Y) * lerpFactor
            );
        SetPosition( newPos );
    }

    /// <summary>
    /// Applies a camera shake effect by randomly offsetting the position.
    /// </summary>
    /// <param name="intensity">Maximum offset in pixels.</param>
    public void Shake( float intensity )
    {
        float offsetX = (Random.Shared.NextSingle() * 2f - 1f) * intensity;
        float offsetY = (Random.Shared.NextSingle() * 2f - 1f) * intensity;
        Translate( new Vector2( offsetX, offsetY ) );
    }

    /// <summary>
    /// Converts world coordinates to camera (screen) coordinates.
    /// </summary>
    /// <param name="worldPos">Position in world space.</param>
    /// <returns>Position in camera space.</returns>
    public Vector2 WorldToCamera( Vector2 worldPos )
    {
        return TransformPoint( worldPos );
    }

    /// <summary>
    /// Converts camera (screen) coordinates to world coordinates.
    /// </summary>
    /// <param name="cameraPos">Position in camera space.</param>
    /// <returns>Position in world space.</returns>
    public Vector2 CameraToWorld( Vector2 cameraPos )
    {
        Transform inverse = Transform.GetInverse();
        var ver = inverse.TransformPoint( Vector2.ToVector2f(cameraPos) );
        return new Vector2( ver.X, ver.Y );
    }

    /// <summary>
    /// Restricts the camera's position within given bounds.
    /// </summary>
    /// <param name="min">Minimum world coordinates.</param>
    /// <param name="max">Maximum world coordinates.</param>
    public void Clamp( Vector2 min, Vector2 max )
    {
        float clampedX = MathF.Max(min.X, MathF.Min(Position.X, max.X));
        float clampedY = MathF.Max(min.Y, MathF.Min(Position.Y, max.Y));
        SetPosition( new Vector2( clampedX, clampedY ) );
    }

    /// <summary>
    /// Centers the camera on a given position instantly.
    /// </summary>
    /// <param name="center">World position to center the camera on.</param>
    public void CenterOn( Vector2 center )
    {
        SetPosition( center );
    }

    /// <summary>
    /// Smoothly zooms the camera towards a target scale.
    /// </summary>
    /// <param name="targetScale">Target scale factor.</param>
    /// <param name="lerpFactor">Interpolation factor (0-1), higher = faster zoom.</param>
    public void SmoothZoom( Vector2 targetScale, float lerpFactor )
    {
        Vector2 newScale = new Vector2(
                ScaleFactor.X + (targetScale.X - ScaleFactor.X) * lerpFactor,
                ScaleFactor.Y + (targetScale.Y - ScaleFactor.Y) * lerpFactor
            );
        SetScale( newScale );
    }

    /// <summary>
    /// Rotates the camera to face a given world position smoothly.
    /// </summary>
    /// <param name="target">Target world position to look at.</param>
    /// <param name="lerpFactor">Interpolation factor (0-1), higher = faster rotation.</param>
    public void LookAt( Vector2 target, float lerpFactor )
    {
        Vector2 dir = target - Position;
        float targetAngle = MathF.Atan2(dir.Y, dir.X) * 180f / MathF.PI;
        float newAngle = RotationAngle + (targetAngle - RotationAngle) * lerpFactor;
        SetRotation( newAngle );
    }

    /// <summary>
    /// Updates the inherited Transform2D based on the current position, rotation, and scale.
    /// </summary>
    private void UpdateTransform()
    {
        Reset();
        Translate( -Position );
        Rotate( -RotationAngle );
        Scale( ScaleFactor );
    }
}