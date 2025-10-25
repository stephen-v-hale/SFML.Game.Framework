using System;
using System.Collections.Generic;
using System.Text;

namespace SFML.Game.Framework.Graphics;
#nullable disable
public class Gradient
{
    /// <summary>
    /// Gets the <see cref="GradientDirection"/>
    /// </summary>
    public GradientDirection Direction { get; }

    /// <summary>
    /// Gets the color array.
    /// </summary>
    public List<GradientColorPoint> Colors { get; } = new List<GradientColorPoint>();

    /// <summary>
    /// Initialize a new instance of <see cref="Gradient"/>
    /// </summary>
    /// <param name="direction">The direction of the gradient.</param>
    /// <param name="colorPoints">The colors and positions</param>
    public Gradient(GradientDirection direction, GradientColorPoint[] colorPoints)
    {
        this.Direction = direction;

        if( colorPoints != null && colorPoints.Length > 0)
            this.Colors.AddRange( colorPoints );
    }

    /// <summary>
    /// Initialize a new instance of <see cref="Gradient"/>
    /// </summary>
    /// <param name="direction">The direction of the gradient.</param>
    public Gradient( GradientDirection direction ) : this( direction, null ) { }
}
