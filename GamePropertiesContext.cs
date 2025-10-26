using System;
using System.Collections.Generic;
using System.Text;

namespace SFML.Game.Framework;

public class GamePropertiesContext
{
    public enum Attribute
    {
        Debug,
        Default,
        Core,
    }
    public enum AntiAlising
    {
        X2,
        X4,
        X6,
        X8,
        None = 0,
    }

    public enum Bits
    {
        None = 0,
        Bit32,
        Bit24,
        Bit16,
        Bit8,
    }

    public enum Version
    {
        Zero,
        One,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
    }

    /// <summary>
    /// Gets or sets the <see cref="AntiAlising"/>
    /// </summary>
    public AntiAlising Antialising
    {
        get;
        set;
    } = AntiAlising.None;

    /// <summary>
    /// Gets or sets the stencil bits.
    /// </summary>
    public Bits Stencil
    {
        get;
        set;
    } = Bits.None;

    /// <summary>
    /// Gets the depth bits.
    /// </summary>
    public Bits Depth
    {
        get;
        set;
    } = Bits.None;

    /// <summary>
    /// Gets or sets the open gl context major version. MAX 4. As of 25/10/2025.
    /// </summary>
    public Version MajorVersion { get; set; } = Version.Four;

    /// <summary>
    /// Gets or sets the open gl context minor version.
    /// </summary>
    public Version MinorVersion { get; set; } = Version.One;

    /// <summary>
    /// Gets or sets a bool value indicating whether this <see cref="Game"/> is SRgbCapable.
    /// </summary>
    public bool SRgbCapable { get; set; } = false;

    /// <summary>
    /// Gets or sets the <see cref="Attribute"/>Flags for the current Open Gl Context.
    /// </summary>
    public Attribute AttributeFlags
    {
        get;
        set;
    } = Attribute.Default;

    /// <summary>
    /// Initialize a new instance of <see cref="GamePropertiesContext"/>
    /// </summary>
    public GamePropertiesContext() { }

    /// <summary>
    /// Gets the default values.
    /// </summary>
    public static GamePropertiesContext Default
    {
        get
        {
            return new()
            {
                 Antialising = AntiAlising.None,
                 Depth = Bits.Bit24,
                 Stencil = Bits.Bit8,
                 MajorVersion = Version.Four,
                 MinorVersion = Version.Six,
            };
        }
    }
}
