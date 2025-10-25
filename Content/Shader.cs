using System;
using System.Collections.Generic;
using System.Text;

using SFML.Graphics;
using SFML.System;

namespace SFML.Game.Framework.Content;

public class Shader
{  
    /// <summary>
    /// The underlying SFML <see cref="Shader"/>.
    /// </summary>
    internal SFML.Graphics.Shader SFMLShader { get; private set; }

    /// <summary>
    /// Indicates whether this <see cref="ShaderWrapper"/> has been disposed.
    /// </summary>
    public bool IsDisposed { get; private set; } = false;

    /// <summary>
    /// Initializes a new instance of <see cref="ShaderWrapper"/> by loading a fragment shader.
    /// </summary>
    /// <param name="fragmentPath">Path to the fragment shader file.</param>
    public Shader( string fragmentPath )
    {
        SFMLShader = new SFML.Graphics.Shader(null, null, fragmentPath );
    }

    /// <summary>
    /// Initializes a new instance of <see cref="ShaderWrapper"/> by loading vertex and fragment shaders.
    /// </summary>
    /// <param name="vertexPath">Path to the vertex shader file.</param>
    /// <param name="fragmentPath">Path to the fragment shader file.</param>
    public Shader( string vertexPath, string fragmentPath )
    {
        SFMLShader = new SFML.Graphics.Shader( vertexPath, null, fragmentPath );
    }

    public Shader( byte[] vertexData )
    {
        SFMLShader = new SFML.Graphics.Shader( new MemoryStream( vertexData ), Stream.Null, Stream.Null );
    }

    public Shader( byte[] vertexData, byte[] fragmentData)
    {
        SFMLShader = new SFML.Graphics.Shader( vertexData == null? Stream.Null: new MemoryStream( vertexData ), new MemoryStream( fragmentData ), Stream.Null );
    }

    // ----------------------------------------------------------
    // Uniform Setters
    // ----------------------------------------------------------

    /// <summary>
    /// Sets a float uniform in the shader.
    /// </summary>
    public void SetUniform( string name, float value )
    {
        SFMLShader.SetUniform( name, value );
    }

    /// <summary>
    /// Sets a 2D vector uniform in the shader.
    /// </summary>
    public void SetUniform( string name, Vector2f value )
    {
        SFMLShader.SetUniform( name, value );
    }

    /// <summary>
    /// Sets a 3D vector uniform in the shader.
    /// </summary>
    public void SetUniform( string name, Vector3f value )
    {
        SFMLShader.SetUniform( name, value );
    }

    /// <summary>
    /// Sets a color uniform in the shader.
    /// </summary>
    public void SetUniform( string name, Color color )
    {
        SFMLShader.SetUniform( name, new SFML.Graphics.Color( ( byte )color.R, ( byte )color.G, ( byte )color.B, ( byte )color.A ) );
    }

    /// <summary>
    /// Sets a texture uniform in the shader.
    /// </summary>
    public void SetUniform( string name, Texture texture )
    {
        SFMLShader.SetUniform( name, texture );
    }

    /// <summary>
    /// Disposes the shader and releases resources.
    /// </summary>
    public void Dispose()
    {
        if ( !IsDisposed )
        {
            SFMLShader.Dispose();
            IsDisposed = true;
        }
    }
}
