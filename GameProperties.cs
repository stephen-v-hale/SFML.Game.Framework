using System;
using System.Collections.Generic;
using System.Text;

using SFML.Graphics;

using SFML.Window;

namespace SFML.Game.Framework;

#nullable disable
public class GameProperties
{

    Game game;
    int preferedBackBufferWidth = 800, preferedBackBufferHeight = 600;
    bool vsyncronization = false;

    /// <summary>
    /// Gets or sets the prefered back buffer width.
    /// </summary>
    public int PreferedBackbufferWidth
    {
        get => preferedBackBufferWidth;
        set
        {
            preferedBackBufferWidth = value;

            if(Game.window != null)
            {
                Game.window.SetView( new View( new FloatRect( 0, 0, value, preferedBackBufferHeight ) ) );
                Game.window.Size = new System.Vector2u( ( uint )value, ( uint )preferedBackBufferHeight );
            }
        }
    }

    /// <summary>
    /// Gets or sets the prefered back buffer height.
    /// </summary>
    public int PreferedBackBufferHeight
    {
        get => preferedBackBufferHeight;
        set
        {
            preferedBackBufferHeight = value;

            if ( Game.window != null )
            {
                Game.window.SetView( new View( new FloatRect( 0, 0, preferedBackBufferWidth, value ) ) );
                Game.window.Size = new System.Vector2u(
                    ( uint )preferedBackBufferWidth,
                    ( uint )value
                );
            }
        }
    }

    /// <summary>
    /// Gets or sets a bool value indicating whether to vsync.
    /// </summary>
    public bool VSyncronization
    {
        get => vsyncronization;
        set
        {
            vsyncronization = value;

            if ( Game.window != null )
            {
                Game.window.SetVerticalSyncEnabled( value );
            }
        }
    }

    /// <summary>
    /// Gets or sets the window <see cref="Styles"/> if this <see cref="Game"/>
    /// </summary>
    public Styles Style { get; set; } = Styles.Close;

    /// <summary>
    /// Gets or sets the bits per pixel.
    /// </summary>
    /// <remarks>Default is normally 32.</remarks>
    public GamePropertiesContext.Bits BitPerPixel { get; set; } = GamePropertiesContext.Bits.Bit16;

    /// <summary>
    /// Gets the Settings for the current <see cref="Game"/>
    /// </summary>
    public GamePropertiesContext Settings { get; set; } = GamePropertiesContext.Default;


    /// <summary>
    /// Initialize a new instance of <see cref="GameProperties"/>
    /// </summary>
    /// <param name="game">The game these <see cref="GameProperties"/> are for.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public GameProperties(Game game)
    {
        if ( game == null )
            throw new ArgumentNullException( nameof( game ) );

        this.game = game;
    }

    /// <summary>
    /// Caps the frames per second to the cap.
    /// </summary>
    /// <param name="cap">The cap.</param>
    public void SetFrameCap( int cap )
    {
        Game.window.SetFramerateLimit( (uint)cap );
    }

    /// <summary>
    /// Sets the icon of the window.
    /// </summary>
    /// <param name="size"></param>
    /// <param name="iconData"></param>
    public void SetIcon(Size size, byte[] iconData)
    {
        Game.window.SetIcon((uint)size.Width, (uint)size.Height, iconData );
    }

}
