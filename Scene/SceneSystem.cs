/*
 *--------------------------------------------------
 * Auther: Stephen Hale
 * File: SceneSystem.cs
 *-------------------------------------------------
*/

#nullable disable
using SFML.Game.Framework.Animation;
using SFML.Game.Framework.Graphics;

namespace SFML.Game.Framework.Scene;

/// <summary>
/// Manages all scenes in the game, handling updates, drawing, and input.
/// </summary>
public class SceneSystem : IDrawableGameComponent
{
    private readonly List<Scene> _scenes = new();
    private readonly GraphicsDrawer _graphicsBatch;
    private bool _initialized;

    public Game Game { get; }
    public bool IsDisposed { get; private set; }

    public AnimationManager AnimationManager { get; }

    public SceneSystem( Game game )
    {
        Game = game ?? throw new ArgumentNullException( nameof( game ) );
        AnimationManager = new AnimationManager();
        _graphicsBatch = new GraphicsDrawer( game.GraphicsDevice as GraphicsDevice );
        Game.logger.Write( "Scene System Created", this );
    }

    /// <summary>
    /// Initializes all scenes managed by this system.
    /// </summary>
    public void Initialize()
    {
        if ( _initialized ) return;
        Game.logger.Write( "Initializing Scene System...", this );

        foreach ( var scene in _scenes )
            scene.Initialize();

        _initialized = true;
    }

    /// <summary>
    /// Adds a scene to the system.
    /// </summary>
    public void AddScene( Scene scene )
    {
        if ( scene == null ) throw new ArgumentNullException( nameof( scene ) );
        if ( _scenes.Contains( scene ) ) return;

        Game.logger.Write( $"Adding scene '{scene.SceneName}'", this );
        scene.SceneSystem = this;
        _scenes.Add( scene );

        if ( _initialized ) scene.Initialize();
    }

    /// <summary>
    /// Removes a scene from the system.
    /// </summary>
    public void RemoveScene( Scene scene )
    {
        if ( scene == null ) throw new ArgumentNullException( nameof( scene ) );
        if ( !_scenes.Contains( scene ) ) return;

        scene.Unload();
        Game.logger.Write( $"Removing scene '{scene.SceneName}'", this );
        _scenes.Remove( scene );
    }

    /// <summary>
    /// Updates all scenes, input, and animations.
    /// </summary>
    public void Update( GameTime frameTime )
    {
        AnimationManager.Update( frameTime.Delta );

        bool otherScreenHasFocus = !Game.IsActive;
        bool coveredByOtherScreen = false;

        foreach ( var scene in _scenes.AsEnumerable().Reverse() )
        {
            scene.Update( frameTime, otherScreenHasFocus, coveredByOtherScreen );

            if ( !otherScreenHasFocus )
            {
                scene.HandleInput();
                otherScreenHasFocus = true;
            }

            if ( !scene.IsPopup )
                coveredByOtherScreen = true;
        }
    }

    /// <summary>
    /// Draws all scenes and animations.
    /// Popups are drawn on top of underlying scenes.
    /// </summary>
    public void Draw( GameTime frameTime )
    {
        AnimationManager.Draw( _graphicsBatch );

        foreach ( var scene in _scenes )
            scene.Draw( frameTime, _graphicsBatch );
    }

    /// <summary>
    /// Disposes the scene system and all resources.
    /// </summary>
    public void Dispose()
    {
        if ( IsDisposed ) return;

        foreach ( var scene in _scenes )
            scene.Unload();

        _graphicsBatch.Dispose();
        IsDisposed = true;

        Game.logger.Write( "Scene System Disposed", this );
    }

    public void LoadContent() { }
}
