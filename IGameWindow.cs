using System;
using System.Collections.Generic;
using System.Text;

namespace SFML.Game.Framework;

public interface IGameWindow
{
    /// <summary>
    /// Load the content of this <see cref="IGameWindow"/>
    /// </summary>
    void LoadContent();

    /// <summary>
    /// Draws the content of this <see cref="IGameWindow"/>
    /// </summary>
    /// <param name="gameTime">The game time.</param>
    abstract void Draw( GameTime gameTime );

    /// <summary>
    /// Update the content of <see cref="IGameWindow"/>
    /// </summary>
    /// <param name="gameTime"></param>
    abstract void Update( GameTime gameTime );

    /// <summary>
    /// Called when fixed update comences.
    /// </summary>
    /// <param name="game"></param>
    void FixedUpdate( GameTime game );

    /// <summary>
    /// Initialize this <see cref="IGameWindow"/>
    /// </summary>
    void Initialize();

    /// <summary>
    /// Run this <see cref="IGameWindow"/>
    /// </summary>
    void Run();
}
