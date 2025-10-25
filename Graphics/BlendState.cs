using System;
using System.Collections.Generic;
using System.Text;

namespace SFML.Game.Framework.Graphics;

public enum BlendState
{
    /// <summary>
    /// Blends only the alpha parts.
    /// </summary>
    Alpha,

    /// <summary>
    /// Brightens the output.
    /// </summary>
    Additive,

    /// <summary>
    /// Darkens the output.
    /// </summary>
    Multiply,

    /// <summary>
    /// No blending.
    /// </summary>
    None
}
