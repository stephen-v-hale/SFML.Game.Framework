using System;
using System.Collections.Generic;
using System.Text;

namespace SFML.Game.Framework.Logging;

public enum EntryType
{
    /// <summary>
    /// The log entry is a warning.
    /// </summary>
    Warning,

    /// <summary>
    /// The log entry is fatel.
    /// </summary>
    Fatel,

    /// <summary>
    /// The log entry is critical.
    /// </summary>
    Critical,

    /// <summary>
    /// The log entry is information.
    /// </summary>
    Information,
}
