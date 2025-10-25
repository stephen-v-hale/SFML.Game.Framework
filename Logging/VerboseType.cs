using System;
using System.Collections.Generic;
using System.Text;

namespace SFML.Game.Framework.Logging;

public enum VerboseType
{
    /// <summary>
    /// The logs output is simple, and readable.
    /// </summary>
    Simple,

    /// <summary>
    /// The logs output has every detail.
    /// </summary>
    Detailed = 1,
}
