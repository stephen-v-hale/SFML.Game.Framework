using System;
using System.Collections.Generic;
using System.Text;

using SFML.Game.Framework.Content;

namespace SFML.Game.Framework.Graphics;

/// <summary>
/// Represents a texture atlas containing multiple sprites in a single texture.
/// </summary>
public class TextureAtlas
{
    private Texture2D atlasTexture;
    private Dictionary<string, Rectangle> regions = new();

    /// <summary>
    /// Initializes a new texture atlas.
    /// </summary>
    /// <param name="texture">The full atlas texture.</param>
    public TextureAtlas( Texture2D texture )
    {
        atlasTexture = texture;
    }

    /// <summary>
    /// Adds a named region to the atlas.
    /// </summary>
    public void AddRegion( string name, Rectangle rect )
    {
        regions[name] = rect;
    }

    /// <summary>
    /// Gets the rectangle of a named region.
    /// </summary>
    public Rectangle GetRegion( string name )
    {
        if ( !regions.TryGetValue( name, out var rect ) )
            throw new KeyNotFoundException( $"Region '{name}' not found in atlas." );
        return rect;
    }

    /// <summary>
    /// The underlying texture.
    /// </summary>
    public Texture2D Texture => atlasTexture;
}