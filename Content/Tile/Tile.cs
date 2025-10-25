using System;
using System.Collections.Generic;
using System.Text;

namespace SFML.Game.Framework.Content.Tile;

public class Tile : ITile
{
    public int Width { get; }
    public int Height { get; }
    public Texture2D Texture { get; }
    public int TileIndex { get; }
    public Vector2 Position { get; }

    /// <summary>
    /// Initialize a new instance of <see cref="Tile"/>
    /// </summary>
    /// <param name="texture"></param>
    /// <param name="position"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    public Tile(Texture2D texture, Vector2 position, int width, int height, int index )
    {
        Texture = texture;
        Position = position;
        Width = width;
        Height = height;
        TileIndex = index;
    }
}
