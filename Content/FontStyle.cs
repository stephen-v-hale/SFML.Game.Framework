using System;
using System.Collections.Generic;
using System.Text;


/// <summary>
/// Represents a font style.
/// </summary>
[Flags]
public enum FontStyle
{
    Regular = 0,
    Bold = 1 << 0,
    Italic = 1 << 1,
    Underlined = 1 << 2,
    StrikeThrough = 1 << 3
}
