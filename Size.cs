using System;
using System.Collections.Generic;
using System.Text;

namespace SFML.Game.Framework;

public class Size
{
    /// <summary>
    /// Gets the width.
    /// </summary>
    public int Width { get; }

    /// <summary>
    /// Gets the height.
    /// </summary>
    public int Height { get; }


    /// <summary>
    /// Initialize a new instance of <see cref="Size"/>
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    public Size( int width, int height )
    {
        Width = width;
        Height = height;
    }
}
