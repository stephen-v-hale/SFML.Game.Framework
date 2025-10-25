using System;
using System.Collections.Generic;
using System.Text;

using SFML.Game.Framework.Graphics;
using SFML.Game.Framework.Content;
#nullable disable
namespace SFML.Game.Framework.PostProcessing;
/// <summary>
/// Base class for a post-processing effect using a fragment shader.
/// </summary>
public abstract class PostProcessingEffect : IDisposable
{
    protected Shader shader;
    public bool IsDisposed { get; private set; }

    /// <summary>
    /// Gets the shader, for use on <see cref="GraphicsDrawer.Begin(BlendState, Shader)"/>
    /// </summary>
    public Shader Shader
    {
        get => shader;
    }

    /// <summary>
    /// Creates the effect from a GLSL fragment shader string.
    /// </summary>
    protected PostProcessingEffect( byte[] fragmentShader )
    {
        shader = new Shader( null, fragmentShader );
    }

    public void Dispose()
    {
        if ( !IsDisposed )
        {
            shader.Dispose();
            IsDisposed = true;
        }
    }
}