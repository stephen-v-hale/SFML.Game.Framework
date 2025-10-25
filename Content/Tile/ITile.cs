using System;
using System.Collections.Generic;
using System.Text;

namespace SFML.Game.Framework.Content.Tile;

public interface ITile
{
    /// <summary>
    /// Gets the texture of the tile.
    /// </summary>
    Texture2D Texture { get; }

    /// <summary>
    /// Gets the index of the tile.
    /// </summary>
    int TileIndex { get; }

    /// <summary>
    /// Gets the position of the tile.
    /// </summary>
    Vector2 Position { get; }

    /// <summary>
    /// Gets the width of the tile.
    /// </summary>
    int Width { get; }

    /// <summary>
    /// Gets the height of the tile.
    /// </summary>
    int Height { get; }
}
