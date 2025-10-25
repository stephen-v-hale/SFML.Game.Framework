using System;
using System.Collections.Generic;
using System.Text;

namespace SFML.Game.Framework.PostProcessing;

public class GlowEffect : PostProcessingEffect
{
    /// <summary>
    /// Initialize a new instance of <see cref="GlowEffect"/>
    /// </summary>
    /// <param name="intensity">The intensity of the glow.</param>
    public GlowEffect(float intensity) : base( Encoding.UTF8.GetBytes( @"
        uniform sampler2D texture;
        uniform float intensity;
        void main()
        {
            vec4 color = texture2D(texture, gl_TexCoord[0].xy);
            vec3 glow = color.rgb * intensity;
            gl_FragColor = vec4(glow, color.a);
        }" ) )
    {
        shader.SetUniform( "intensity", intensity );
    }
}
