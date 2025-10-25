using System;
using System.Collections.Generic;
using System.Text;

namespace SFML.Game.Framework;

public interface IGameComponent : IDisposable
{
    /// <summary>
    /// Gets a bool value indicating whether this <see cref="IGameComponent"/> has been disposed.
    /// </summary>
    bool IsDisposed { get; }

    /// <summary>
    /// Gets the game.
    /// </summary>
    Game Game { get; }

    /// <summary>
    /// Loads this game components content.
    /// </summary>
    void LoadContent();

    /// <summary>
    /// Initalize this game component.
    /// </summary>
    void Initialize();

    /// <summary>
    /// Update this game component.
    /// </summary>
    /// <param name="gameTime"></param>
    void Update( GameTime gameTime );
}
