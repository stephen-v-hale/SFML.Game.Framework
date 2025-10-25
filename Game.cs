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
    public bool IsFixedTimeStep { get; set; } = false;

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

        // Initialize timing and window
        window = new RenderWindow( new VideoMode( (uint)Properties.PreferedBackbufferWidth, (uint)Properties.PreferedBackBufferHeight, ( uint )Properties.BitPerPixel ), Title, Properties.Style, Properties.Settings );
        window.Closed += ( _, __ ) => window.Close();
        window.GainedFocus += ( _, _ ) => IsFocused = true;
        window.LostFocus += ( _, _ ) => IsFocused = false;

        window.SetVerticalSyncEnabled( Properties.VSyncronization );

        Initialize();

        logger.Write( "Loading content...", this );
        LoadContent();

        logger.Write( "Creating game loop...", this );

        const double targetDelta = 1.0 / 60.0; // 60 FPS fixed timestep
        double accumulatedTime = 0.0;
        var stopwatch = Stopwatch.StartNew();

        double previousTime = stopwatch.Elapsed.TotalSeconds;

        // Main game loop
        while ( window.IsOpen )
        {
            // Process SFML events
            window.DispatchEvents();

            // Calculate elapsed time
            double currentTime = stopwatch.Elapsed.TotalSeconds;
            double frameTime = currentTime - previousTime;
            previousTime = currentTime;

            // Clamp large frame gaps (avoid simulation spikes)
            if ( frameTime > 0.25 )
                frameTime = 0.25;

            // Update GameTime
            gameTime.ElapsedGameTime = TimeSpan.FromSeconds( frameTime );
            gameTime.TotalGameTime += gameTime.ElapsedGameTime;
            gameTime.Delta = ( float )frameTime;

            if ( IsFixedTimeStep )
            {
                accumulatedTime += frameTime;

                // Run FixedUpdate() at consistent intervals
                while ( accumulatedTime >= targetDelta )
                {
                    gameTime.ElapsedGameTime = TimeSpan.FromSeconds( targetDelta );
                    gameTime.Delta = ( float )targetDelta;
                    FixedUpdate( gameTime );
                    accumulatedTime -= targetDelta;
                }

                Input.InputSystem.Update();

                // Once per frame
                Update( gameTime );
            }
            else
            {
                Input.InputSystem.Update();

                // Variable timestep
                Update( gameTime );
            }

            Draw( gameTime );
            window.Display();
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
