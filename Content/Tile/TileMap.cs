using System;
using System.Collections.Generic;
using System.Text;

using SFML.Game.Framework.Graphics;

namespace SFML.Game.Framework.Content.Tile;

#nullable disable

/// <summary>
/// Represents a tile map containing multiple tiles in a grid.
/// </summary>
public class TileMap
{
    /// <summary>
    /// List of all tiles in the map.
    /// </summary>
    private readonly List<Tile> tiles = new();

    /// <summary>
    /// Adds a tile to the map.
    /// </summary>
    /// <param name="tile">The tile to add.</param>
    public void AddTile( Tile tile )
    {
        tiles.Add( tile );
    }

    /// <summary>
    /// Gets a tile by its index.
    /// </summary>
    /// <param name="index">The tile index.</param>
    /// <returns>The tile with the specified index, or null if not found.</returns>
    public Tile GetTileByIndex( int index )
    {
        return tiles.Find( t => t.TileIndex == index );
    }

    /// <summary>
    /// Gets all tiles at a specific position.
    /// </summary>
    /// <param name="position">The world position to check.</param>
    /// <returns>List of tiles at the specified position.</returns>
    public List<Tile> GetTilesAtPosition( Vector2 position )
    {
        List<Tile> found = new List<Tile>();
        foreach ( var tile in tiles )
        {
            if ( tile.Position.X == position.X && tile.Position.Y == position.Y )
                found.Add( tile );
        }
        return found;
    }

    /// <summary>
    /// Draws the tile map using the provided GraphicsDrawer.
    /// </summary>
    /// <param name="drawer">GraphicsDrawer instance.</param>
    public void Draw( GraphicsDrawer drawer )
    {
        foreach ( Tile tile in tiles )
        {
            drawer.DrawTexture( tile.Texture,
                               tile.Position, Color.White);
        }
    }

    /// <summary>
    /// Removes a tile by its index.
    /// </summary>
    /// <param name="index">The tile index to remove.</param>
    public void RemoveTile( int index )
    {
        tiles.RemoveAll( t => t.TileIndex == index );
    }

    /// <summary>
    /// Clears all tiles from the map.
    /// </summary>
    public void Clear()
    {
        tiles.Clear();
    }
}
