#nullable disable
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using SFML.Game.Framework.Animation;
using SFML.Game.Framework.Graphics;
using SFML.Game.Framework.Logging;
using SFML.Game.Framework.Scene;
using SFML.Graphics;
using SFML.Window;

using Thread = System.Threading.Thread;

namespace SFML.Game.Framework;

public abstract class Game : IGameWindow, IDisposable
{
    GameTime gameTime = new GameTime();
    static internal RenderWindow window;
    internal static Logger logger;

    #region Properties
    /// <summary>
    /// Gets or sets weather this <see cref="Game"/> uses a fixed time step.
    /// </summary>
    /// <remarks>Usually means, the <see cref="Game"/> will always try to get your frame per second to match your refresh rate.</remarks>
    public bool IsFixedTimeStep { get; set; } = true;

    /// <summary>
    /// Gets weather this <see cref="Game"/> is disposed.
    /// </summary>
    /// <remarks>True; if disposed; otherwise false.</remarks>
    public bool IsDisposed { get; private set; } = false;

    /// <summary>
    /// Gets or sets this <see cref="Game"/>s properties.
    /// </summary>
    public GameProperties Properties { get; set; }

    /// <summary>
    /// Gets or sets the title of this <see cref="Game"/>
    /// </summary>
    public string Title { get; set; } = "Untitled Game";

    /// <summary>
    /// Gets the <see cref="IGraphicsDevice"/> for this <see cref="Game"/>
    /// </summary>
    public IGraphicsDevice GraphicsDevice { get; }

    /// <summary>
    /// Gets the <see cref="IGameComponent"/>s
    /// </summary>
    public List<IGameComponent> Components { get; } = new List<IGameComponent>();

    /// <summary>
    /// Gets a bool value indicating whether this <see cref="Game"/> is focused.
    /// </summary>
    public bool IsFocused { get; private set; } = true;

    /// <summary>
    /// Gets a bool value indicating whether this <see cref="Game"/> is active.
    /// </summary>
    public bool IsActive => window.IsOpen && IsFocused;

    /// <summary>
    /// Gets the <see cref="SceneSystem"/> for this <see cref="Game"/>
    /// </summary>
    public SceneSystem SceneSystem { get; }
    #endregion

    #region Contructors

    /// <summary>
    /// Initialize a new instance of <see cref="Game"/>
    /// </summary>
    public Game()
    {
        LogFactory.CreateLogger( "debug", VerboseType.Detailed );
        logger = LogFactory.GetLogger( "debug" );

        Properties = new GameProperties(this);
        GraphicsDevice = new GraphicsDevice( this );
        SceneSystem = new SceneSystem( this );

        Components.Add( SceneSystem );
    }


    #endregion

    #region methods
    public virtual void Draw( GameTime gameTime )
    {
        foreach ( var component in Components )
            if ( component is IDrawableGameComponent )
                ( ( IDrawableGameComponent )component ).Draw( gameTime );
    }
    public virtual void FixedUpdate( GameTime game ) { }
    public virtual void Initialize()
    {
        foreach ( var component in Components )
        {

            logger.Write( $"Initalizing {component}...", this );
            component.Initialize();
        }
    }
    public virtual void LoadContent()
    {
        foreach ( var component in Components )
        {
            logger.Write( $"Loading {component}s content...", this );
            component.LoadContent();
        }
    }
    public virtual void Update( GameTime gameTime )
    {
        foreach ( var component in Components )
            component.Update( gameTime );
    }

    public void Run()
    {
        logger.Write( "Initializing", this);

        try
        {
            // Initialize timing and window
            ContextSettings settings = new ContextSettings()
            {
                AntialiasingLevel = Properties.Settings.Antialising switch
                {
                    GamePropertiesContext.AntiAlising.X2 => 2,
                    GamePropertiesContext.AntiAlising.X4 => 4,
                    GamePropertiesContext.AntiAlising.X6 => 6,
                    GamePropertiesContext.AntiAlising.X8 => 6,
                    _ => 0
                },
                StencilBits = Properties.Settings.Stencil switch
                {
                    GamePropertiesContext.Bits.Bit8 => 8,
                    GamePropertiesContext.Bits.Bit16 => 16,
                    GamePropertiesContext.Bits.Bit24 => 24,
                    GamePropertiesContext.Bits.Bit32 => 32,
                    _ => 0,
                },
                DepthBits =  Properties.Settings.Depth switch
                {
                    GamePropertiesContext.Bits.Bit8 => 8,
                    GamePropertiesContext.Bits.Bit16 => 16,
                    GamePropertiesContext.Bits.Bit24 => 24,
                    GamePropertiesContext.Bits.Bit32 => 32,
                    _ => 0,
                },
                MajorVersion = Properties.Settings.MajorVersion switch
                {
                    GamePropertiesContext.Version.Zero => 0,
                    GamePropertiesContext.Version.One => 1,
                    GamePropertiesContext.Version.Two => 2,
                    GamePropertiesContext.Version.Three => 3,
                    GamePropertiesContext.Version.Four => 4,
                    GamePropertiesContext.Version.Five => 5,
                    GamePropertiesContext.Version.Six => 6,
                    GamePropertiesContext.Version.Seven => 7,
                    GamePropertiesContext.Version.Eight=> 8,
                    GamePropertiesContext.Version.Nine => 9,
                    GamePropertiesContext.Version.Ten => 10,
                    _ => 4,
                },
                MinorVersion = Properties.Settings.MinorVersion switch
                {
                    GamePropertiesContext.Version.Zero => 0,
                    GamePropertiesContext.Version.One => 1,
                    GamePropertiesContext.Version.Two => 2,
                    GamePropertiesContext.Version.Three => 3,
                    GamePropertiesContext.Version.Four => 4,
                    GamePropertiesContext.Version.Five => 5,
                    GamePropertiesContext.Version.Six => 6,
                    GamePropertiesContext.Version.Seven => 7,
                    GamePropertiesContext.Version.Eight=> 8,
                    GamePropertiesContext.Version.Nine => 9,
                    GamePropertiesContext.Version.Ten => 10,
                    _ => 4,
                },
                SRgbCapable = Properties.Settings.SRgbCapable,
                AttributeFlags = Properties.Settings.AttributeFlags switch
                {
                    GamePropertiesContext.Attribute.Default => ContextSettings.Attribute.Default,
                    GamePropertiesContext.Attribute.Debug => ContextSettings.Attribute.Debug,
                    GamePropertiesContext.Attribute.Core => ContextSettings.Attribute.Core,
                    _ => ContextSettings.Attribute.Default,
                }
            };

            VideoMode mode = new VideoMode( ( uint )Properties.PreferedBackbufferWidth, ( uint )Properties.PreferedBackBufferHeight, Properties.BitPerPixel switch
            {
                GamePropertiesContext.Bits.Bit16 => 16,
                GamePropertiesContext.Bits.Bit32 => 32,
                GamePropertiesContext.Bits.Bit24 => 24,
                GamePropertiesContext.Bits.Bit8 => 8,
                GamePropertiesContext.Bits.None => 0,
                _ => 32,
            } );
            window = new RenderWindow( mode, Title, Properties.Style, settings );

            window.Closed += ( _, __ ) => window.Close();
            window.GainedFocus += ( _, _ ) => IsFocused = true;
            window.LostFocus += ( _, _ ) => IsFocused = false;

            window.SetVerticalSyncEnabled( Properties.VSyncronization );

            Initialize();

            logger.Write( "Loading content...", this );
            LoadContent();

            logger.Write( "Creating game loop...", this );

            double targetDelta = 1.0 / Math.Max(1, Win32.DisplayHelper.GetWindowRefreshRate(window));
            double accumulatedTime = 0.0;
            var stopwatch = Stopwatch.StartNew();
            double previousTime = stopwatch.Elapsed.TotalSeconds;

            while ( window.IsOpen )
            {
                // Process window events
                window.DispatchEvents();

                // Calculate elapsed time
                double currentTime = stopwatch.Elapsed.TotalSeconds;
                double frameTime = currentTime - previousTime;
                previousTime = currentTime;

                // Clamp large frame gaps (avoid simulation spikes)
                if ( frameTime > 0.25 )
                    frameTime = 0.25;

                // Update GameTime
                gameTime.UnscaledElapsedGameTime = TimeSpan.FromSeconds( frameTime );
                gameTime.TotalUnscaledGameTime += gameTime.ElapsedGameTime;
                gameTime.UnscaledDelta = ( float )frameTime;

                if ( IsFixedTimeStep )
                {
                    accumulatedTime += frameTime;

                    // Fixed update step (logic at 60Hz)
                    while ( accumulatedTime >= targetDelta )
                    {
                        gameTime.UnscaledElapsedGameTime = TimeSpan.FromSeconds( targetDelta );
                        gameTime.UnscaledDelta = ( float )targetDelta;

                        FixedUpdate( gameTime );
                        accumulatedTime -= targetDelta;
                    }

                    // Once per frame
                    Input.InputSystem.Update();
                    Update( gameTime );
                    Draw( gameTime );
                    window.Display();

                    if ( Properties.VSyncronization )
                    {
                        double frameEnd = stopwatch.Elapsed.TotalSeconds;
                        double frameDuration = frameEnd - currentTime;
                        double sleepTime = targetDelta - frameDuration;

                        if ( sleepTime > 0 )
                        { 
                            // Fine-tune with a short spin-wait for high-precision frame cap
                            double targetEnd = currentTime + targetDelta;
                            while ( stopwatch.Elapsed.TotalSeconds < targetEnd )
                            {
                                // Optional: yield CPU very briefly
                                Thread.SpinWait( 10 );
                            }
                        }
                    }
                }
                else
                {
                    // Variable timestep mode
                    Input.InputSystem.Update();
                    Update( gameTime );
                    Draw( gameTime );
                    window.Display();
                }
            }
        }
        catch (Exception ex)
        {
            logger.Write( "RunTime Error: " + ex.Message, EntryType.Fatel, this );
            window.Close();
        }
        logger.Write( "Game loop End. Disposing objects...", this );
        Dispose();

        logger.SaveToFile( Environment.CurrentDirectory + @"\SFML.Game.Framework.log" );
    }

    public void Dispose()
    {
        if(!IsDisposed)
        {
            window.Dispose();
            IsDisposed = true;

            logger.Write( "Objects Disposed", this );
        }
    }

    #endregion
}
