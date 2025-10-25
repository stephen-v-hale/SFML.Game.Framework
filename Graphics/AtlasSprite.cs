using System;
using System.Collections.Generic;
using System.Text;

namespace SFML.Game.Framework.Graphics;

/// <summary>
/// Represents a sprite drawn from a texture atlas.
/// </summary>
public class AtlasSprite
{
    private TextureAtlas atlas;
    private string regionName;

    public AtlasSprite( TextureAtlas atlas, string regionName )
    {
        this.atlas = atlas;
        this.regionName = regionName;
    }

    /// <summary>
    /// Draws the sprite at a given position, scale, rotation, and color.
    /// </summary>
    internal void Draw( GraphicsDrawer drawer, Vector2 position, Vector2 scale, float rotation = 0f, Color? color = null )
    {
        Rectangle rect = atlas.GetRegion(regionName);
        drawer.DrawTexture( atlas.Texture, rect, new Vector2( 0, 0 ), color ?? Color.White, rotation, scale );
    }
}