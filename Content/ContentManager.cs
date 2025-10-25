using System;
using System.Collections.Generic;
using System.Text;

using SFML.Game.Framework.Content.Tile;

namespace SFML.Game.Framework.Content;
#nullable disable
public class ContentManager
{
    ContentPipelineReader contentPipelineReader;

    /// <summary>
    /// Initialize a new instance of <see cref="ContentManager"/>
    /// </summary>
    /// <param name="assetPackagePath"></param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="FileNotFoundException"></exception>
    public ContentManager(string assetPackagePath)
    {
        if ( string.IsNullOrWhiteSpace( assetPackagePath ) ) throw new ArgumentNullException( nameof( assetPackagePath ) );
        if ( !File.Exists( assetPackagePath ) ) throw new FileNotFoundException( assetPackagePath );

        this.contentPipelineReader = new ContentPipelineReader( assetPackagePath );

        Game.logger.Write( $"ContentManager created from assetpackage '{assetPackagePath}'", this );
        foreach (var asset in contentPipelineReader.GetAllAssets())
        {
            Game.logger.Write( $"Asset '{asset.Key}' loaded with size of '{asset.Value}'", this );
        }

    }

    /// <summary>
    /// Gets a texture.
    /// </summary>
    /// <param name="textureName">The name of the texture that is inside the asset file.</param>
    /// <returns></returns>
    public Texture2D GetTexture( string textureName ) => new Texture2D( contentPipelineReader.GetByName( textureName ) );

    /// <summary>
    /// Gets a font.
    /// </summary>
    /// <param name="fontName">The name of the font that is inside the asset file.</param>
    /// <returns></returns>
    public Font GetFont( string fontName ) => new Font( contentPipelineReader.GetByName( fontName ) , 10 );

    /// <summary>
    /// Gets a <see cref="SoundEffect"/>."/>
    /// </summary>
    /// <param name="soundEffectName">The name of the <see cref="SoundEffect"/> that was given when assets pack was built.</param>
    /// <returns></returns>
    public SoundEffect GetSoundEffect( string soundEffectName ) => new SoundEffect( contentPipelineReader.GetByName( soundEffectName ) );

    /// <summary>
    /// Gets a <see cref="Song"/>
    /// </summary>
    /// <param name="songName">The name of the <see cref="Song"/> that was given when assets pack was built.</param>
    /// <returns></returns>
    public Song GetSong( string songName ) => new Song( contentPipelineReader.GetByName( songName ) );

    /// <summary>
    /// Gets a Vertex shader
    /// </summary>
    /// <param name="shaderName">The name given.</param>
    /// <returns></returns>
    public Shader GetVertexShader( string shaderName ) => new Shader( contentPipelineReader.GetByName( shaderName + "_vertex" ) );

    /// <summary>
    /// Gets a fragment shader.
    /// </summary>
    /// <param name="shaderName">The name given.</param>
    /// <returns></returns>
    public Shader GetFragmentShader( string shaderName ) => new Shader( null, contentPipelineReader.GetByName( shaderName + "_frag" ) );

    /// <summary>
    /// Gets a full shader.
    /// </summary>
    /// <param name="vertexName">The name of the vertex shader.</param>
    /// <param name="fragName">The name of the fragment shader.</param>
    /// <returns></returns>
    public Shader GetShader( string vertexName, string fragName ) => new Shader( contentPipelineReader.GetByName( vertexName + "_vertex" ), contentPipelineReader.GetByName( fragName + "_frag" ) );
   
    /// <summary>
    /// Parses a tile map from a text file.
    /// </summary>
    /// <param name="filePath">Path to the tile map file.</param>
    /// <param name="contentManager">Content manager used to load textures.</param>
    /// <returns>A TileMap instance containing all tiles from the file.</returns>
    public TileMap GetTileMap ( string tilemapName )
    {
        TileMap map = new TileMap();
        string[] lines = Encoding.ASCII.GetString(contentPipelineReader.GetByName(tilemapName)).Split("\n");

        foreach ( string line in lines )
        {
            if ( string.IsNullOrWhiteSpace( line ) || line.StartsWith( "#" ) )
                continue; // Skip empty lines or comments

            string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if ( parts.Length != 6 )
                throw new FormatException( $"Invalid line format: {line}" );

            string textureName = parts[0];
            int x = int.Parse(parts[1]);
            int y = int.Parse(parts[2]);
            int width = int.Parse(parts[3]);
            int height = int.Parse(parts[4]);
            int index = int.Parse(parts[5]);

            Texture2D texture = GetTexture(textureName);
            Tile.Tile tile = new Tile.Tile (texture, new Vector2(x, y), width, height, index);
            map.AddTile( tile );
        }

        return map;
    }
}