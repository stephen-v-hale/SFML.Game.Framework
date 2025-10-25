using System;
using System.Collections.Generic;
using System.Text;

namespace SFML.Game.Framework.PostProcessing;

public  class ChromaticAberrationEffect : PostProcessingEffect
{
    /// <summary>
    /// Initialize a new instance of <see cref="ChromaticAberrationEffect"/>
    /// </summary>
    /// <param name="offset">The offset.</param>
    public ChromaticAberrationEffect(float offset) : base( Encoding.UTF8.GetBytes( @"
        uniform sampler2D texture;
        uniform float offset;
        void main()
        {
            vec2 uv = gl_TexCoord[0].xy;
            vec2 texSize = vec2(textureSize(texture, 0));

            float r = texture2D(texture, uv + vec2(offset/texSize.x,0)).r;
            float g = texture2D(texture, uv).g;
            float b = texture2D(texture, uv - vec2(offset/texSize.x,0)).b;

            gl_FragColor = vec4(r,g,b,1.0);
        }" ) )
    {
        shader.SetUniform( "offset", offset );
    }
}
