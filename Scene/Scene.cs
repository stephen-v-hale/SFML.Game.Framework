/*
 *--------------------------------------------------
 * Auther: Stephen Hale
 * File: Scene.cs
 *-------------------------------------------------
*/
#nullable disable
using SFML.Game.Framework.Graphics;
using SFML.Game.Framework.Scene.UI;

namespace SFML.Game.Framework.Scene;

/// <summary>
/// Represents an abstract scene that manages objects, controls, updates, and drawing.
/// </summary>
public abstract class Scene
{
    private readonly List<object> _objects = new();
    private readonly List<ControlBase> _controls = new();
    private bool _initialized;

    public SceneSystem SceneSystem { get; internal set; }

    /// <summary>
    /// Gets or sets whether this scene is a popup.
    /// Popups don't block underlying scenes from updating/drawing.
    /// </summary>
    public bool IsPopup { get; set; } = false;

    /// <summary>Gets the name of the scene.</summary>
    public string SceneName { get; }

    /// <summary>
    /// Event triggered when the scene is initialized.
    /// </summary>
    public event Action<Scene> OnInitialized;

    /// <summary>
    /// Event triggered when the scene is unloaded.
    /// </summary>
    public event Action<Scene> OnUnloaded;

    protected Scene( string sceneName )
    {
        SceneName = sceneName ?? throw new ArgumentNullException( nameof( sceneName ) );
        Game.logger.Write( $"Scene Created: {sceneName}", this );
    }

    /// <summary>
    /// Initializes the scene and all its controls.
    /// </summary>
    public virtual void Initialize()
    {
        if ( _initialized ) return;

        Game.logger.Write( $"Initializing Scene: {SceneName}", this );
        foreach ( var control in _controls )
        {
            Game.logger.Write( $"Initializing Control: {control}", this );
            control.Initialize();
        }

        _initialized = true;
        OnInitialized?.Invoke( this );
    }

    /// <summary>
    /// Draws the scene and its controls.
    /// </summary>
    public virtual void Draw( GameTime frameTime, GraphicsDrawer batch )
    {
        foreach ( var control in _controls )
            control.Draw( frameTime, batch );
    }

    /// <summary>
    /// Updates the scene logic and controls.
    /// </summary>
    public virtual void Update( GameTime frameTime, bool otherScreenHasFocus, bool coveredByOtherScreen )
    {
        foreach ( var control in _controls )
            control.Update( frameTime );
    }

    /// <summary>
    /// Handles input for all controls in the scene.
    /// </summary>
    public virtual void HandleInput()
    {
        foreach ( var control in _controls )
            control.HandleInput();
    }

    /// <summary>
    /// Unloads the scene and triggers events.
    /// </summary>
    public virtual void Unload()
    {
        _objects.Clear();
        _controls.Clear();
        OnUnloaded?.Invoke( this );
    }

    #region Object Management
    public void AddObject( object obj )
    {
        _objects.Add( obj ?? throw new ArgumentNullException( nameof( obj ) ) );
    }

    public void RemoveObject( object obj )
    {
        _objects.Remove( obj ?? throw new ArgumentNullException( nameof( obj ) ) );
    }

    public void ClearObjects() => _objects.Clear();

    public T GetObject<T>() => _objects.OfType<T>().FirstOrDefault();
    public IEnumerable<T> GetObjects<T>() => _objects.OfType<T>();
    #endregion

    #region Control Management
    public void AddControl( ControlBase control ) => _controls.Add( control ?? throw new ArgumentNullException( nameof( control ) ) );
    public void RemoveControl( ControlBase control ) => _controls.Remove( control ?? throw new ArgumentNullException( nameof( control ) ) );

    public T GetControl<T>() where T : ControlBase => _controls.OfType<T>().FirstOrDefault();
    public IEnumerable<T> GetControls<T>() where T : ControlBase => _controls.OfType<T>();
    #endregion
}