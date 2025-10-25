using System;
using System.Collections.Generic;
using System.Text;

namespace SFML.Game.Framework.Graphics;

/// <summary>
/// Represents a color stop in a gradient.
/// </summary>
public class GradientColorPoint
{
    /// <summary>
    /// Gets or sets the color of this point.
    /// </summary>
    public Color Color { get; set; }

    /// <summary>
    /// Gets or sets the start position of the color (0.0 - 1.0).
    /// </summary>
    public float Start { get; set; }

    /// <summary>
    /// Gets or sets the end position of the color (0.0 - 1.0).
    /// </summary>
    public float End { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ColorPoint"/> class.
    /// </summary>
    /// <param name="color">The color for this segment.</param>
    /// <param name="start">The normalized start position (0–1).</param>
    /// <param name="end">The normalized end position (0–1).</param>
    public GradientColorPoint( Color color, float start, float end )
    {
        if ( end < start )
            throw new ArgumentException( "End cannot be less than Start." );

        Color = color;
        Start = start;
        End = end;
    }

    /// <summary>
    /// Returns a string describing this color point.
    /// </summary>
    public override string ToString() =>
        $"ColorPoint(Color={Color}, Start={Start}, End={End})";
}