using System;
using System.Collections.Generic;
using System.Text;

namespace SFML.Game.Framework;

public interface IDrawableGameComponent : IGameComponent
{
    /// <summary>
    /// Draw this game component
    /// </summary>
    /// <param name="gameTime"></param>
    void Draw( GameTime gameTime );
}
